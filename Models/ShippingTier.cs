using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomwhangShop.Models
{
    [Table("ShippingTiers")] // ✅ ระบุชื่อตารางให้ตรงกับ SQL (เติม s)
    public class ShippingTier
    {
        [Key] // ✅ ระบุว่าเป็น Primary Key
        public int Id { get; set; }

        public int ShopId { get; set; }

        [Required]
        public string CourierName { get; set; } = "Flash";

        // ✅ ใช้ชื่อ MinWeight และ MaxWeight ตามที่แก้ไขใน SQL แล้ว
        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; }
        public decimal Price { get; set; }
    }
}