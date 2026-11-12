using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomwhangShop.Models
{
    [Table("slip_audit_logs")]
    public class SlipAuditLog
    {
        [Key]
        public int LogId { get; set; } // ✅ แก้เป็น LogId ตามฐานข้อมูลจริง
        public int ShopId { get; set; }
        public DateTime AttemptDate { get; set; } = DateTime.Now;
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // Approved, Rejected, Duplicate
        public string Reason { get; set; }
        public string? SlipImageUrl { get; set; } // ✅ เพิ่มตามฐานข้อมูล
    }
}