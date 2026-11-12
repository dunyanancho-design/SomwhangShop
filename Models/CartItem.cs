namespace SomwhangShop.Models
{
    public class CartItem
    {
        public Product Product { get; set; } = new();
        public int Quantity { get; set; } = 1;
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }

        // คำนวณราคารวมของสินค้านี้ (ราคา x จำนวน)
        public decimal TotalPrice => Product.Price * Quantity;
    }
}