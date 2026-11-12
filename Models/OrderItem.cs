using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomwhangShop.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}