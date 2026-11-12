using Microsoft.EntityFrameworkCore;
using SomwhangShop.Models;

namespace SomwhangShop.Data
{
    public class SomwhangShopContext : DbContext
    {
        public SomwhangShopContext(DbContextOptions<SomwhangShopContext> options)
            : base(options)
        {
        }

        // --- 🛒 ส่วนร้านค้า (Core Shop) ---
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // --- 🚚 ส่วนระบบจัดส่ง (Shipping System) ✅ เพิ่มใหม่ตรงนี้ ---
        public DbSet<ShippingConfig> ShippingConfigs { get; set; }
        public DbSet<ShippingTier> ShippingTiers { get; set; }
        public DbSet<SlipTransaction> SlipTransactions { get; set; }

        // --- 🤖 ส่วน Facebook Bot ---
        public DbSet<FacebookPageConfig> FacebookPageConfigs { get; set; }
        public DbSet<AutoReplyItem> AutoReplyItems { get; set; }

        // --- 💬 ส่วน LINE OA Bot ---
        public DbSet<LineBotConfig> LineBotConfigs { get; set; }
        public DbSet<LineAutoReplyItem> LineAutoReplyItems { get; set; }

        // --- 💸 ส่วนระบบตรวจสลิป (Slip Verification) ---
        public DbSet<SlipConfig> SlipConfigs { get; set; }
        public DbSet<SlipAuditLog> SlipAuditLogs { get; set; }

        // ✅ เพิ่ม Table สำหรับเก็บค่า Config ระบบ (เช่น โหมดปิดปรับปรุง)
        public DbSet<AppSetting> AppSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ตั้งค่า Facebook Bot
            modelBuilder.Entity<AutoReplyItem>()
                .HasOne(r => r.FacebookPageConfig)
                .WithMany(p => p.AutoReplyItems)
                .HasForeignKey(r => r.FacebookPageConfigId)
                .OnDelete(DeleteBehavior.Cascade);

            // ตั้งค่า LINE Bot
            modelBuilder.Entity<LineAutoReplyItem>()
                .HasOne(r => r.LineBotConfig)
                .WithMany(p => p.AutoReplyItems)
                .HasForeignKey(r => r.LineBotConfigId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ แนะนำเพิ่มเติม: ระบุชื่อตารางให้ชัดเจนเพื่อป้องกัน EF เติม s ผิดที่
            modelBuilder.Entity<ShippingConfig>().ToTable("ShippingConfigs");
            modelBuilder.Entity<ShippingTier>().ToTable("ShippingTiers");
        }
    }
}