using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomwhangShop.Models
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        public string CustomerName { get; set; } = "";
        public string ShippingAddress { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string? SlipImageUrl { get; set; } = "";

        public string Email { get; set; } = "";
        public string TransRef { get; set; } = "";
        public string? TrackingNumber { get; set; }

        // ✅ เพิ่มฟิลด์สำหรับระบบขนส่ง
        public decimal ShippingCost { get; set; } = 0;
        public string CourierName { get; set; } = "";

        // ✅ อันนี้ใช้สำหรับหน้า Cart (ไม่ต้องยุ่งกับมัน)
        [NotMapped]
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // ✅ อันนี้สำหรับหน้า Admin (ให้ EF Core ดึงข้อมูลสินค้าในออเดอร์มาแสดง)
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public string Status { get; set; } = "รอตรวจสอบยอดเงิน";
        public string AffiliateId { get; set; } = "";
    }
}