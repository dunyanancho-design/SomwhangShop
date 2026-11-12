using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // อย่าลืม using นี้

namespace SomwhangShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int ShopId { get; set; }

        [ForeignKey("ShopId")]
        public virtual Shop? Shop { get; set; }

        [Required]
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string Category { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public int StockQuantity { get; set; } = 100;
        public decimal Weight { get; set; } = 1.0m;
    }
}