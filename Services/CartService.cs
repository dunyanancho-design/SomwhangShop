using SomwhangShop.Models;
using Microsoft.Extensions.Logging;

namespace SomwhangShop.Services
{
    public class CartService
    {
        private readonly ILogger<CartService> _logger;

        public CartService(ILogger<CartService> logger)
        {
            _logger = logger;
        }

        public List<CartItem> Items { get; private set; } = new List<CartItem>();
        public event Action? OnChange;

        // ✅ 1. เพิ่มฟังก์ชันนับจำนวนสินค้า (แก้ Error CS1061: GetItemCount)
        public int GetItemCount()
        {
            return Items.Sum(i => i.Quantity);
        }

        // ✅ 2. เพิ่มฟังก์ชัน AddItem ให้เป็น Alias เรียก AddToCart (แก้ Error CS1061: AddItem)
        public void AddItem(Product product)
        {
            AddToCart(product);
        }

        // ฟังก์ชันเดิมที่มีอยู่แล้ว
        public void AddToCart(Product product)
        {
            var existingItem = Items.FirstOrDefault(x => x.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                LogToConsole($"🛒 เพิ่มจำนวน: ลูกค้าหยิบ '{product.Name}' เพิ่ม (รวมเป็น {existingItem.Quantity} ชิ้น)");
            }
            else
            {
                Items.Add(new CartItem { Product = product, Quantity = 1 });
                LogToConsole($"✨ หยิบใส่ตะกร้า: ลูกค้าเลือก '{product.Name}' เป็นชิ้นแรก");
            }
            NotifyStateChanged();
        }

        public void DecreaseItem(Product product)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    LogToConsole($"➖ ลดจำนวน: '{product.Name}' เหลือ {item.Quantity} ชิ้น");
                }
                else
                {
                    Items.Remove(item);
                    LogToConsole($"🗑️ หยิบออก: '{product.Name}' ถูกเอาออกจากตะกร้าเพราะลดจนเหลือ 0");
                }
                NotifyStateChanged();
            }
        }

        public void RemoveFromCart(Product product)
        {
            var item = Items.FirstOrDefault(x => x.Product.Id == product.Id);
            if (item != null)
            {
                Items.Remove(item);
                LogToConsole($"❌ ลบทิ้ง: ลูกค้าเอา '{product.Name}' ออกจากตะกร้า");
                NotifyStateChanged();
            }
        }

        public void ClearCart()
        {
            var count = Items.Count;
            Items.Clear();
            LogToConsole($"🧹 ล้างตะกร้า: เทของออกหมดเกลี้ยง ({count} รายการ)");
            NotifyStateChanged();
        }

        public decimal GetGrandTotal()
        {
            return Items.Sum(x => x.TotalPrice);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        private void LogToConsole(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 🛍️ ตะกร้าสินค้า: {message}");
            Console.ForegroundColor = originalColor;
        }
    }
}