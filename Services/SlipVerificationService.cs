using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SomwhangShop.Data;
using SomwhangShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace SomwhangShop.Services
{
    public class SlipVerificationResult
    {
        public bool IsValid { get; set; }
        public string BankName { get; set; } = "";
        public string SenderName { get; set; } = "";
        public decimal Amount { get; set; }
        public string TransRef { get; set; } = "";
        public string Message { get; set; } = "";
        public string SlipImageUrl { get; set; } = "";
    }

    public class SlipVerificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IDbContextFactory<SomwhangShopContext> _dbFactory;
        private readonly ILogger<SlipVerificationService> _logger;
        private readonly IWebHostEnvironment _env;

        public SlipVerificationService(HttpClient httpClient, IDbContextFactory<SomwhangShopContext> dbFactory, ILogger<SlipVerificationService> logger, IWebHostEnvironment env)
        {
            _httpClient = httpClient;
            _dbFactory = dbFactory;
            _logger = logger;
            _env = env;
        }

        public async Task<SlipVerificationResult> VerifySlipAsync(string base64Image, decimal expectedAmount, string customerName, string phoneNumber, int shopId)
        {
            using var context = _dbFactory.CreateDbContext();

            // 1. ดึง Config ร้านค้าก่อน เพื่อเอาชื่อร้านมาแสดงใน Log
            var config = await context.SlipConfigs
                .FirstOrDefaultAsync(c => EF.Property<int>(c, "ShopId") == shopId);

            // เตรียมชื่อร้านสำหรับ Log (ถ้าไม่มี Config ให้โชว์ ID)
            string shopLogName = config != null && !string.IsNullOrEmpty(config.ShopNameKey)
                ? config.ShopNameKey.Split(',', StringSplitOptions.RemoveEmptyEntries)[0].Trim() // เอาชื่อแรก เช่น "บจก. สมหวัง"
                : $"Shop ID {shopId}";

            LogToConsole($"เริ่มตรวจสอบสลิป: {shopLogName} | ลูกค้า: {customerName} | ยอด: {expectedAmount}");

            if (config == null || string.IsNullOrEmpty(config.ApiToken))
            {
                LogToConsole($"Error: {shopLogName} ยังไม่ได้ตั้งค่าระบบตรวจสลิป", true);
                return new SlipVerificationResult { IsValid = false, Message = "ระบบตรวจสอบสลิปของร้านนี้ยังไม่ได้ตั้งค่า" };
            }

            try
            {
                var cleanBase64 = base64Image.Contains(",") ? base64Image.Split(',')[1] : base64Image;
                var payload = new { image = cleanBase64 };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.ApiToken);

                var response = await _httpClient.PostAsJsonAsync(config.ApiUrl, payload);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    LogToConsole($"API Error: {response.StatusCode}", true);
                    string errorPath = await SaveAuditLog(context, "Rejected", $"API Error: {response.StatusCode}", 0, customerName, phoneNumber, cleanBase64);
                    return new SlipVerificationResult { IsValid = false, Message = "ติดต่อระบบตรวจสอบสลิปไม่ได้", SlipImageUrl = errorPath };
                }

                var apiResult = JsonSerializer.Deserialize<ApiResponseRoot>(responseString);
                var data = apiResult?.data;

                if (data == null)
                {
                    string noDataPath = await SaveAuditLog(context, "Rejected", "สลิปไม่ถูกต้อง", 0, customerName, phoneNumber, cleanBase64);
                    return new SlipVerificationResult { IsValid = false, Message = "ไม่พบข้อมูลในสลิป", SlipImageUrl = noDataPath };
                }

                decimal slipAmount = data.amount?.amount ?? 0;
                string transRef = data.transRef ?? "";
                string receiverNameTh = data.receiver?.account?.name?.th ?? "";
                string receiverNameEn = data.receiver?.account?.name?.en ?? "";

                // ---------------------------------------------------------
                // กฎข้อที่ 1: ตรวจสอบยอดเงิน (อนุญาตให้ต่างกันได้ไม่เกิน 0.01 บาท)
                // ---------------------------------------------------------
                if (Math.Abs(slipAmount - expectedAmount) > 0.01m)
                {
                    string reason = $"ยอดเงินไม่ตรง (สลิป: {slipAmount} / ต้องโอน: {expectedAmount})";
                    string path = await SaveAuditLog(context, "Rejected", reason, slipAmount, customerName, phoneNumber, cleanBase64);
                    return new SlipVerificationResult { IsValid = false, Message = reason, SlipImageUrl = path };
                }

                // ---------------------------------------------------------
                // กฎข้อที่ 2: ตรวจสอบชื่อร้าน (Normalize ตัดช่องว่างทิ้งทั้งหมด)
                // ---------------------------------------------------------
                if (!string.IsNullOrEmpty(config.ShopNameKey))
                {
                    var keywords = config.ShopNameKey.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    // Helper: ลบ Space และอักขระพิเศษที่มองไม่เห็น (\u00A0)
                    string NormalizeName(string input) => input?.Replace(" ", "").Replace("\u00A0", "").Trim() ?? "";

                    string cleanReceiverTh = NormalizeName(receiverNameTh);
                    string cleanReceiverEn = NormalizeName(receiverNameEn);

                    bool isNameMatch = keywords.Any(k =>
                    {
                        string cleanKey = NormalizeName(k);
                        // เทียบแบบ IgnoreCase และไม่มีช่องว่าง
                        return cleanReceiverTh.Contains(cleanKey, StringComparison.OrdinalIgnoreCase) ||
                               cleanReceiverEn.Contains(cleanKey, StringComparison.OrdinalIgnoreCase);
                    });

                    if (!isNameMatch)
                    {
                        // Log ค่าจริงที่ได้ เพื่อให้เราตรวจสอบได้ว่าธนาคารส่งชื่ออะไรมา
                        string debugMsg = $"ชื่อไม่ตรง (Config: {string.Join("|", keywords)} vs Slip: {receiverNameTh}/{receiverNameEn})";
                        LogToConsole(debugMsg, true);

                        string reason = $"ชื่อผู้รับไม่ตรง (ในสลิป: {receiverNameTh})";
                        string path = await SaveAuditLog(context, "Rejected", reason, slipAmount, customerName, phoneNumber, cleanBase64);
                        return new SlipVerificationResult { IsValid = false, Message = reason, SlipImageUrl = path };
                    }
                }

                // ---------------------------------------------------------
                // กฎข้อที่ 3: ตรวจสอบสลิปซ้ำ (Duplicate Check)
                // ---------------------------------------------------------
                // เช็คทั้งใน AuditLog และ SlipTransactions (ถ้ามีข้อมูล) เพื่อความชัวร์
                bool isDuplicateLog = await context.SlipAuditLogs.AnyAsync(l => l.Reason.Contains(transRef) && l.Status == "Approved");
                bool isDuplicateTrans = await context.SlipTransactions.AnyAsync(t => t.TransRef == transRef);

                if (isDuplicateLog || isDuplicateTrans)
                {
                    string reason = $"สลิปซ้ำ (Ref: {transRef})";
                    string path = await SaveAuditLog(context, "Duplicate", reason, slipAmount, customerName, phoneNumber, cleanBase64);
                    return new SlipVerificationResult { IsValid = false, Message = reason, SlipImageUrl = path };
                }

                // =========================================================
                // ✅ ผ่านการตรวจสอบ: เตรียมบันทึกข้อมูล
                // =========================================================

                // 1. เพิ่มลงตาราง SlipTransactions (บัญชีรายรับ)
                try
                {
                    var newTransaction = new SlipTransaction
                    {
                        TransRef = transRef,
                        BankName = data.sender?.bank?.name ?? "Unknown Bank",
                        // ถ้าชื่อคนโอนไม่มี ให้ใช้ชื่อลูกค้าที่กรอกมา
                        SenderName = string.IsNullOrEmpty(data.sender?.account?.name?.th) ? customerName : data.sender.account.name.th,
                        Amount = slipAmount,
                        CreatedDate = DateTime.Now
                    };
                    context.SlipTransactions.Add(newTransaction);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error preparing SlipTransaction record");
                    // ไม่ return error เพราะถือว่าลูกค้าโอนจริงแล้ว แค่บันทึก transaction พลาด
                }

                // 2. บันทึกรูปและ AuditLog (คำสั่ง SaveChangesAsync ในนี้จะบันทึก Transaction ด้านบนด้วย)
                string successPath = await SaveAuditLog(context, "Approved", $"ผ่าน (Ref: {transRef})", slipAmount, customerName, phoneNumber, cleanBase64);

                LogToConsole($"✅ ผ่าน! Ref: {transRef} | ยอด: {slipAmount}");

                return new SlipVerificationResult
                {
                    IsValid = true,
                    Amount = slipAmount,
                    TransRef = transRef,
                    SlipImageUrl = successPath,
                    BankName = data.receiver?.bank?.name ?? "",
                    SenderName = data.sender?.account?.name?.th ?? ""
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Slip Verification Exception");
                return new SlipVerificationResult { IsValid = false, Message = "เกิดข้อผิดพลาดภายในระบบ" };
            }
        }

        // ฟังก์ชันช่วยบันทึก Log และรูปภาพ
        private async Task<string> SaveAuditLog(SomwhangShopContext context, string status, string reason, decimal amount, string customerName, string phoneNumber, string base64)
        {
            string savedImagePath = "";
            try
            {
                string fileName = $"slip_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}.jpg";
                string folderPath = Path.Combine(_env.WebRootPath, "images", "slips");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, fileName);
                await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(base64));
                savedImagePath = $"/images/slips/{fileName}";
            }
            catch (Exception ex) { _logger.LogError(ex, "Error saving slip image"); }

            context.SlipAuditLogs.Add(new SlipAuditLog
            {
                CustomerName = customerName,
                PhoneNumber = phoneNumber,
                Amount = amount,
                Status = status,
                Reason = reason,
                SlipImageUrl = savedImagePath,
                AttemptDate = DateTime.Now
            });

            // ✅ บันทึกทุกอย่างลง Database (ทั้ง AuditLog และ SlipTransactions ที่ Add ค้างไว้)
            await context.SaveChangesAsync();

            return savedImagePath;
        }

        private void LogToConsole(string message, bool isError = false)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = isError ? ConsoleColor.Red : ConsoleColor.Magenta;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 💸 SlipCheck: {message}");
            Console.ForegroundColor = original;
        }

        // --- API Response Models ---
        private class ApiResponseRoot { public ApiData? data { get; set; } }
        private class ApiData
        {
            public ApiAmountObj? amount { get; set; }
            public ApiParty? sender { get; set; }
            public ApiParty? receiver { get; set; }
            public string? transRef { get; set; }
        }
        private class ApiAmountObj { [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)] public decimal amount { get; set; } }
        private class ApiParty { public ApiBank? bank { get; set; } public ApiAccount? account { get; set; } }
        private class ApiBank { public string? name { get; set; } }
        private class ApiAccount { public ApiName? name { get; set; } }
        private class ApiName { public string? th { get; set; } public string? en { get; set; } }
    }
}