namespace SomwhangShop.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string LinkUrl { get; set; } = "";

        // ข้อมูลการเงินเดิม
        public string PromptPayId { get; set; } = "";
        public string PromptPayName { get; set; } = "";

        // 🔴 เพิ่มตัวนี้ (เก็บ URL รูป QR Code)
        public string QrCodeUrl { get; set; } = "";

        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        public string CalcType { get; set; } = "Weight";
        public decimal FreeThreshold { get; set; } = 3.0m;
        public decimal BaseShippingFee { get; set; } = 50.0m;
    }
}