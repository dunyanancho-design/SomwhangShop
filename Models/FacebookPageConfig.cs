using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomwhangShop.Models
{
    [Table("facebook_page_configs")]
    public class FacebookPageConfig
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ชื่อเพจ (สำหรับจำ)")]
        public string AdminNote { get; set; }

        [Required]
        [StringLength(100)]
        public string PageId { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string PageAccessToken { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // เชื่อมโยงไปหาตารางลูก (เพิ่มบรรทัดนี้)
        public virtual ICollection<AutoReplyItem> AutoReplyItems { get; set; } = new List<AutoReplyItem>();
    }
}