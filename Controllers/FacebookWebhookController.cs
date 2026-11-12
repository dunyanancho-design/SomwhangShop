using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SomwhangShop.Data;
using Microsoft.EntityFrameworkCore;
using SomwhangShop.Models;
using System.Text.Json.Serialization;

namespace SomwhangShop.Controllers
{
    [Route("api/webhook")]
    [ApiController]
    public class FacebookWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDbContextFactory<SomwhangShopContext> _dbFactory;

        public FacebookWebhookController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IDbContextFactory<SomwhangShopContext> dbFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _dbFactory = dbFactory;
        }

        [HttpGet]
        public IActionResult VerifyWebhook([FromQuery(Name = "hub.mode")] string mode,
                                         [FromQuery(Name = "hub.verify_token")] string token,
                                         [FromQuery(Name = "hub.challenge")] string challenge)
        {
            var expectedToken = _configuration["Facebook:VerifyToken"] ?? "SomwhangSecret";
            if (mode == "subscribe" && token == expectedToken)
            {
                LogInfo($"🔍 ยืนยัน Webhook สำเร็จ!");
                return Ok(challenge);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook([FromBody] JsonElement payload)
        {
            try
            {
                var data = JsonSerializer.Deserialize<FacebookPayload>(payload.GetRawText());
                if (data?.Entry == null) return Ok();

                foreach (var entry in data.Entry)
                {
                    var pageId = entry.Id;

                    // 1. จัดการ "ทักแชท Inbox"
                    if (entry.Messaging != null)
                    {
                        foreach (var msg in entry.Messaging)
                        {
                            if (!string.IsNullOrEmpty(msg.Message?.Text))
                            {
                                var senderId = msg.Sender?.Id;
                                if (senderId == pageId) continue;
                                // ยังไม่รู้ชื่อเพจตรงนี้ รอไป Log ใน ProcessAutoReply
                                _ = Task.Run(() => ProcessAutoReply(pageId, senderId, msg.Message.Text, isChat: true));
                            }
                        }
                    }

                    // 2. จัดการ "คอมเมนต์"
                    if (entry.Changes != null)
                    {
                        foreach (var change in entry.Changes)
                        {
                            if (change.Field == "feed" && change.Value?.Item == "comment" && change.Value?.Verb == "add")
                            {
                                var val = change.Value;
                                if (val.From?.Id == pageId) continue;
                                _ = Task.Run(() => ProcessAutoReply(pageId, val.CommentId, val.Message, isChat: false));
                            }
                        }
                    }
                }
                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex) { LogError($"❌ Webhook Error: {ex.Message}"); return Ok(); }
        }

        private async Task ProcessAutoReply(string pageId, string targetId, string msg, bool isChat)
        {
            using var db = _dbFactory.CreateDbContext();

            // ✅ ดึงชื่อเพจ (AdminNote) มาด้วย
            var config = await db.FacebookPageConfigs
                .FirstOrDefaultAsync(p => p.PageId == pageId && p.IsActive);

            if (config == null) return;

            string pageName = config.AdminNote ?? "Unknown Page"; // ชื่อเพจ เช่น "น้ำปลาร้าสมหวัง"

            // แสดง Log ว่าได้รับข้อความ (ย้ายมาตรงนี้เพื่อให้รู้ชื่อเพจ)
            string type = isChat ? "Inbox" : "Comment";
            LogInfo($"[{pageName}] 📩 รับ {type}: \"{msg}\"");

            var rule = await db.AutoReplyItems.Where(r => r.FacebookPageConfigId == config.Id && r.IsActive).ToListAsync();
            var match = rule.FirstOrDefault(r => r.Keywords.Split(',').Any(k => msg.Contains(k.Trim(), StringComparison.OrdinalIgnoreCase)));

            if (match != null)
            {
                LogInfo($"[{pageName}] ✅ ตอบกลับคีย์เวิร์ด: '{match.Keywords}'");
                string url = isChat
                    ? $"https://graph.facebook.com/v24.0/me/messages"
                    : $"https://graph.facebook.com/v24.0/{targetId}/private_replies";

                var payload = isChat
                    ? (object)new { recipient = new { id = targetId }, message = new { text = match.ReplyMessage } }
                    : (object)new { message = match.ReplyMessage };

                await SendToFb(url, payload, config.PageAccessToken, pageName);
            }
        }

        private async Task SendToFb(string url, object payload, string token, string pageName)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var res = await client.PostAsJsonAsync(url, payload);

            if (!res.IsSuccessStatusCode)
                LogError($"[{pageName}] 💀 ส่งไม่ผ่าน: {await res.Content.ReadAsStringAsync()}");
            else
                LogInfo($"[{pageName}] 🚀 ส่งสำเร็จ!");
        }

        private void LogInfo(string m) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 🤖 FB Bot: {m}"); Console.ResetColor(); }
        private void LogError(string m) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 🤖 FB Bot: {m}"); Console.ResetColor(); }
    }

    // Models (เหมือนเดิม)
    public class FacebookPayload { [JsonPropertyName("entry")] public List<Entry>? Entry { get; set; } }
    public class Entry { [JsonPropertyName("id")] public string Id { get; set; } [JsonPropertyName("changes")] public List<Change>? Changes { get; set; } [JsonPropertyName("messaging")] public List<Messaging>? Messaging { get; set; } }
    public class Messaging { [JsonPropertyName("sender")] public Sender? Sender { get; set; } [JsonPropertyName("message")] public MessageData? Message { get; set; } }
    public class MessageData { [JsonPropertyName("text")] public string Text { get; set; } }
    public class Change { [JsonPropertyName("field")] public string Field { get; set; } [JsonPropertyName("value")] public ChangeValue Value { get; set; } }
    public class ChangeValue { [JsonPropertyName("item")] public string Item { get; set; } [JsonPropertyName("verb")] public string Verb { get; set; } [JsonPropertyName("comment_id")] public string CommentId { get; set; } [JsonPropertyName("message")] public string Message { get; set; } [JsonPropertyName("from")] public Sender? From { get; set; } }
    public class Sender { [JsonPropertyName("id")] public string Id { get; set; } [JsonPropertyName("name")] public string Name { get; set; } }
}