using SomwhangShop.Models;
using SomwhangShop.Data;
using Microsoft.EntityFrameworkCore;

namespace SomwhangShop.Services
{
    public class ShippingService
    {
        private readonly IDbContextFactory<SomwhangShopContext> _dbFactory;

        public ShippingService(IDbContextFactory<SomwhangShopContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<decimal> CalculateCostAsync(List<CartItem> items, string provider)
        {
            // 1. ถ้าไม่มีของในตะกร้า ค่าส่งเป็น 0
            if (items == null || !items.Any()) return 0;

            var shopId = items.First().Product.ShopId;

            using var context = _dbFactory.CreateDbContext();

            // ดึงข้อมูลร้านค้า และ Config ขนส่ง (ถ้ามี)
            var shop = await context.Shops.FirstOrDefaultAsync(s => s.Id == shopId);
            var config = await context.ShippingConfigs.FirstOrDefaultAsync(c => c.ShopId == shopId && c.CourierName == provider);

            if (shop == null) return 50; // Fallback ปลอดภัยถ้าไม่เจอร้านค้า

            // 2. คำนวณเกณฑ์ (น้ำหนัก หรือ จำนวนชิ้น)
            decimal criteriaValue = 0;
            if (shop.CalcType == "Quantity")
            {
                // นับจำนวนชิ้น
                criteriaValue = items.Sum(i => (decimal)i.Quantity);
            }
            else
            {
                // คำนวณน้ำหนักรวม
                criteriaValue = items.Sum(i => (decimal)i.Product.Weight * i.Quantity);
            }

            // 3. เช็คส่งฟรีตาม "เกณฑ์ร้านค้า" (เช่น ครบ 3 ขวด หรือ 3 กก. ส่งฟรี) [Priority สูงสุด]
            if (shop.FreeThreshold > 0 && criteriaValue >= shop.FreeThreshold)
            {
                return 0;
            }

            // 4. เช็คส่งฟรีตาม "ยอดเงิน" (เฉพาะกรณีที่มี Config พิเศษ)
            if (config != null && config.IsActive)
            {
                decimal totalOrderPrice = items.Sum(i => i.Product.Price * i.Quantity);
                if (totalOrderPrice >= config.FreeShippingThreshold) return 0;
            }

            // 5. ดึงราคาจากตาราง ShippingTiers (ทำเสมอ แม้ไม่มี Config)
            var matchedTier = await context.ShippingTiers
                .Where(t => t.ShopId == shopId && t.CourierName == provider)
                .OrderBy(t => t.MinWeight)
                .FirstOrDefaultAsync(t => criteriaValue >= t.MinWeight && criteriaValue < t.MaxWeight);

            if (matchedTier != null)
            {
                return matchedTier.Price; // เจอตารางเรท ใช้ราคานั้นเลย
            }

            // 6. ถ้าไม่เจอเรท: ให้ลองใช้ BaseRate จาก Config (ถ้ามี)
            if (config != null && config.IsActive)
            {
                return config.BaseRate;
            }

            // ✅ แก้ไข: ลบ "?? 50" ออก เพราะ BaseShippingFee เป็น decimal (ไม่ Null)
            return shop.BaseShippingFee;
        }
    }
}