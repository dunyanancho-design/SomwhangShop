using System.ComponentModel.DataAnnotations;

namespace SomwhangShop.Models
{
    public class LineBotConfig
    {
        [Key]
        public int Id { get; set; }
        public string AdminNote { get; set; } = "My Line OA"; // ชื่อเรียกในระบบหลังบ้าน
        public string ChannelAccessToken { get; set; } // คีย์ยาวๆ
        public string ChannelSecret { get; set; } // คีย์สั้นๆ ไว้ตรวจ Signature
        public bool IsActive { get; set; } = true;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public List<LineAutoReplyItem> AutoReplyItems { get; set; } = new();
    }

    public class LineAutoReplyItem
    {
        [Key]
        public int Id { get; set; }
        public int LineBotConfigId { get; set; }
        public string Keywords { get; set; } // คำที่ลูกค้าพิมพ์ (คั่นด้วย ,)
        public string ReplyMessage { get; set; } // ข้อความที่บอทจะตอบ
        public bool IsActive { get; set; } = true;
        public LineBotConfig LineBotConfig { get; set; }
    }
}