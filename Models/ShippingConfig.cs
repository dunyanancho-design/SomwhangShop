using System.ComponentModel.DataAnnotations;

namespace SomwhangShop.Models
{
    public class ShippingConfig
    {
        [Key]
        public int Id { get; set; }
        public int ShopId { get; set; } // แยกตามร้านค้า
        public string CourierName { get; set; } = "Flash"; // Flash, SPX
        public decimal BaseRate { get; set; } = 40; // ค่าส่งเริ่มต้น
        public decimal FreeShippingThreshold { get; set; } = 500; // ยอดขั้นต่ำส่งฟรี
        public bool IsActive { get; set; } = true;
    }
}