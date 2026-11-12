using SomwhangShop.Components;
using SomwhangShop.Services;
using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using SomwhangShop.Data;
using Microsoft.AspNetCore.Components.Server.Circuits;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddSignalR(hubOptions => { hubOptions.MaximumReceiveMessageSize = 100 * 1024 * 1024; });

builder.Services.AddSingleton<ProductService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddScoped<ShopService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<EmailSender>();
builder.Services.AddScoped<FileUploadService>();
builder.Services.AddHttpClient<TelegramService>();
builder.Services.AddSingleton<ShippingService>();
builder.Services.AddScoped<AdminStateService>();
builder.Services.AddScoped<StatsService>();
builder.Services.AddHttpClient<SlipVerificationService>();
builder.Services.AddScoped<BackupService>();

// ✅ เพิ่ม Service สำหรับควบคุม Maintenance Mode
builder.Services.AddSingleton<SiteStateService>();

// ระบบ Monitor คนใช้งาน
builder.Services.AddSingleton<UserTrackerService>();
builder.Services.AddScoped<CircuitHandler, TrackingCircuitHandler>();

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<SomwhangShopContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

// ============================================================
// ✨ Dashboard สรุปสถานะ
// ============================================================
void ShowDashboard()
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var tracker = services.GetService<UserTrackerService>();
        var siteState = services.GetService<SiteStateService>(); // ✅ ดึง SiteState
        var userCount = tracker?.GetCount() ?? 0;

        int activeFbBots = 0;
        int activeLineBots = 0;
        bool isSlipSystemReady = false;
        bool isCartSystemReady = false;
        int productCount = 0;
        bool isMaintenance = siteState?.IsMaintenanceMode ?? false; // ✅ เช็คสถานะ

        try
        {
            var dbFactory = services.GetRequiredService<IDbContextFactory<SomwhangShopContext>>();
            using var db = dbFactory.CreateDbContext();

            activeFbBots = db.FacebookPageConfigs.Count(p => p.IsActive);
            activeLineBots = db.LineBotConfigs.Count(p => p.IsActive);

            var slipConfig = db.SlipConfigs.FirstOrDefault();
            isSlipSystemReady = slipConfig != null && !string.IsNullOrEmpty(slipConfig.ApiToken);

            if (db.Database.CanConnect())
            {
                productCount = db.Products.Count();
                isCartSystemReady = true;
            }
        }
        catch { }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║               🚀 SOMWHANG SHOP - LIVE MONITOR                  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");

        Console.Write("  👥  User Online      : ");
        if (userCount <= 1) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"{userCount} คน (✅ จำนวนผู้ใช้)"); }
        else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"{userCount} คน (⚠️ มีลูกค้า!)"); }
        Console.ResetColor();

        // ✅ เพิ่มบรรทัดสถานะร้าน
        Console.Write("  🏪  Shop Status      : ");
        if (isMaintenance) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("⛔ ปิดปรับปรุง (Maintenance Mode)"); }
        else { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("✅ เปิดให้บริการ (Online)"); }
        Console.ResetColor();

        Console.Write("  🤖  Facebook Bot     : ");
        if (activeFbBots > 0) { Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"ทำงานอยู่ {activeFbBots} เพจ"); }
        else { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("ปิดการทำงาน (Offline)"); }
        Console.ResetColor();

        Console.Write("  💬  LINE OA Bot      : ");
        if (activeLineBots > 0) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"ทำงานอยู่ {activeLineBots} บัญชี"); }
        else { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("ปิดการทำงาน (Offline)"); }
        Console.ResetColor();

        Console.Write("  💸  Slip Check       : ");
        if (isSlipSystemReady) { Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("พร้อมใช้งาน (Configured)"); }
        else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("❌ ยังไม่ตั้งค่า Token"); }
        Console.ResetColor();

        Console.Write("  🛒  Cart System      : ");
        if (isCartSystemReady) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"พร้อมขาย ({productCount} สินค้า)"); }
        else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("❌ ระบบมีปัญหา / Database หลุด"); }
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("  [S] เช็คสถานะ  |  [C] เคลียร์จอ  |  [Ctrl+C] ปิดโปรแกรม");
        Console.ResetColor();
        Console.WriteLine();
    }
}

var serverTask = app.RunAsync();
Thread.Sleep(1000);
Console.Clear();
ShowDashboard();

while (!serverTask.IsCompleted)
{
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.S) ShowDashboard();
        else if (key == ConsoleKey.C) { Console.Clear(); ShowDashboard(); }
    }
    Thread.Sleep(100);
}

await serverTask;