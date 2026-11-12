using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomwhangShop.Models
{
    [Table("SlipConfigs")]
    public class SlipConfig
    {
        [Key]
        public int Id { get; set; }

        // ✅ เพิ่มเพื่อระบุว่าการตั้งค่านี้เป็นของร้านค้าใด (เช่น 1 = ลูกชิ้น, 3 = น้ำปลาร้า)
        [Required]
        public int ShopId { get; set; }

        [Required]
        public string ApiUrl { get; set; } = "https://api.thunder.in.th/v1/verify";

        [Required]
        public string ApiToken { get; set; } = "";

        // ✅ รองรับการระบุหลายคีย์เวิร์ดโดยแยกด้วยจุลภาค (,) ในหน้า UI
        [Required]
        public string ShopNameKey { get; set; } = "สมหวัง ลูกชิ้นเนื้อแท้";

        public string? ShopPromptPay { get; set; } = "";

        // ✅ เพิ่ม IsActive เพื่อเปิด-ปิดการใช้งานระบบตรวจสลิปรายร้าน
        public bool IsActive { get; set; } = true;

        public DateTime? LastUpdated { get; set; } = DateTime.Now;
    }
}