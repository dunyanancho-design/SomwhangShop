using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomwhangShop.Models
{
    // ตารางลูก: เก็บกฎการตอบกลับ (1 เพจ มีหลายข้อ)
    [Table("auto_reply_items")]
    public class AutoReplyItem
    {
        [Key]
        public int Id { get; set; }

        // FK เชื่อมกลับไปหาเพจแม่
        public int FacebookPageConfigId { get; set; }

        [ForeignKey("FacebookPageConfigId")]
        public virtual FacebookPageConfig? FacebookPageConfig { get; set; }

        [Required]
        [Display(Name = "คีย์เวิร์ด")]
        public string Keywords { get; set; } // เช่น "ราคา, price"

        [Required]
        [Column(TypeName = "text")]
        [Display(Name = "ข้อความตอบกลับ")]
        public string ReplyMessage { get; set; }

        [Display(Name = "ประเภทคำตอบ")]
        public string ReplyType { get; set; } = "text"; // text, image

        public bool IsActive { get; set; } = true;
    }
}