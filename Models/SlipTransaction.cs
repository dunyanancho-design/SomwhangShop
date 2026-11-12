using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomwhangShop.Models
{
    // ตารางนี้เอาไว้เก็บ "บัญชีรายรับ" แยกออกมาจาก Log
    [Table("sliptransactions")]
    public class SlipTransaction
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string TransRef { get; set; } = "";

        [StringLength(50)]
        public string BankName { get; set; } = "";

        [StringLength(255)]
        public string SenderName { get; set; } = "";

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}