using System.Text.Json.Serialization; // 👈 1. ต้องเพิ่มบรรทัดนี้

namespace SomwhangShop.Models
{
    public class ThunderResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public ThunderData data { get; set; }
    }

    public class ThunderData
    {
        public string sender_name { get; set; }
        public string receiver_name { get; set; }

        // 🔴 2. แก้ตรงนี้: เปลี่ยนกลับเป็น decimal แต่ใส่ Option พิเศษ
        // Option นี้บอกว่า "รับค่าได้ทั้งแบบตัวเลข (100) และแบบตัวหนังสือ ("100")"
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal amount { get; set; }

        public string bank_sender { get; set; }
        public string trans_date { get; set; }
        public string trans_time { get; set; }
        public string qrcode_payload { get; set; }
    }
}