using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SomwhangShop.Data;
using SomwhangShop.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

[ApiController]
[Route("api/line/webhook")]
public class LineWebhookController : ControllerBase
{
    private readonly IDbContextFactory<SomwhangShopContext> _dbFactory;
    private readonly HttpClient _httpClient;

    public LineWebhookController(IDbContextFactory<SomwhangShopContext> dbFactory, IHttpClientFactory httpClientFactory)
    {
        _dbFactory = dbFactory;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        try
        {
            // 1. อ่าน Header Signature
            if (!Request.Headers.TryGetValue("x-line-signature", out var signature))
            {
                return BadRequest("Missing Signature");
            }

            // 2. อ่าน Body (ต้องอ่านเป็น string เพื่อนำมา Hash ตรวจสอบ)
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            // 3. ดึง Config จาก DB (เพื่อเอา ChannelSecret มาตรวจ)
            using var context = _dbFactory.CreateDbContext();
            var botConfig = await context.LineBotConfigs
                                         .Include(b => b.AutoReplyItems)
                                         .FirstOrDefaultAsync(b => b.IsActive);

            if (botConfig == null) return Ok(); // ไม่มีบอทที่เปิดใช้งาน ก็จบงานเงียบๆ

            // ⚠️ 4. ตรวจสอบ Signature (Security Check)
            if (!VerifySignature(signature.ToString(), body, botConfig.ChannelSecret))
            {
                Console.WriteLine($"⛔ [LINE Security] Signature validation failed! IP: {HttpContext.Connection.RemoteIpAddress}");
                return BadRequest("Invalid Signature"); // ปฏิเสธ Request ปลอม
            }

            // 5. แปลง JSON เป็น Object
            var jsonDoc = JsonDocument.Parse(body);
            // ตรวจสอบว่ามี property "events" หรือไม่ (ป้องกัน Error กรณี Webhook Test)
            if (!jsonDoc.RootElement.TryGetProperty("events", out var events))
            {
                return Ok();
            }

            foreach (var ev in events.EnumerateArray())
            {
                var type = ev.GetProperty("type").GetString();

                // ตรวจสอบว่ามี replyToken หรือไม่ (บาง event อาจไม่มี)
                string? replyToken = null;
                if (ev.TryGetProperty("replyToken", out var rt))
                {
                    replyToken = rt.GetString();
                }

                if (type == "message" && replyToken != null)
                {
                    var messageType = ev.GetProperty("message").GetProperty("type").GetString();
                    if (messageType == "text")
                    {
                        var userMessage = ev.GetProperty("message").GetProperty("text").GetString();

                        // กรองคำตอบ (Logic เดิมของคุณ)
                        var matchedRule = botConfig.AutoReplyItems
                            .FirstOrDefault(r => r.IsActive && r.Keywords.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Any(k => userMessage.Contains(k.Trim())));

                        if (matchedRule != null)
                        {
                            await ReplyMessage(replyToken, matchedRule.ReplyMessage, botConfig.ChannelAccessToken);
                        }
                        else
                        {
                            Console.WriteLine($"[LINE] No match for: {userMessage}");
                        }
                    }
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"🔥 [LINE Error] {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    // ✅ ฟังก์ชันตรวจสอบ Signature (HMACSHA256)
    private bool VerifySignature(string signature, string body, string channelSecret)
    {
        if (string.IsNullOrEmpty(channelSecret)) return true; // ถ้าไม่ได้ตั้ง Secret ไว้ ให้ปล่อยผ่าน

        try
        {
            var key = Encoding.UTF8.GetBytes(channelSecret);
            using var hmac = new HMACSHA256(key);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
            var computedSignature = Convert.ToBase64String(hash);

            return signature == computedSignature;
        }
        catch
        {
            return false;
        }
    }

    private async Task ReplyMessage(string replyToken, string message, string accessToken)
    {
        var requestBody = new
        {
            replyToken = replyToken,
            messages = new[] { new { type = "text", text = message } }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.line.me/v2/bot/message/reply");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Content = content;

        await _httpClient.SendAsync(request);
    }
}