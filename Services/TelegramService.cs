using Microsoft.Extensions.Configuration;
using SomwhangShop.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SomwhangShop.Services
{
    public class TelegramService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public TelegramService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendOrderNotificationAsync(Order order)
        {
            try
            {
                var token = _config["TelegramSettings:BotToken"];
                var chatId = _config["TelegramSettings:ChatId"];

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(chatId)) return;

                // ✅ แก้ไข: ใช้การต่อข้อความแบบ + แทน เพื่อป้องกัน Error เรื่องบรรทัด
                string message = $"🔔 *มีออเดอร์ใหม่!* (Somwhang Shop)\n" +
                                 $"---------------------------------\n" +
                                 $"📦 *รหัส:* `{order.OrderId}`\n" +
                                 $"👤 *ลูกค้า:* {order.CustomerName}\n" +
                                 $"💰 *ยอดเงิน:* {order.TotalAmount:N2} บาท\n" +
                                 $"🚚 *ที่อยู่:* {order.ShippingAddress}\n" +
                                 $"📞 *โทร:* {order.PhoneNumber}\n" +
                                 $"---------------------------------\n" +
                                 $"🔗 กรุณาตรวจสอบสลิปในระบบหลังบ้าน";

                // เรียก API Telegram
                var url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}&parse_mode=Markdown";

                await _httpClient.GetAsync(url);
            }
            catch (Exception ex)
            {
                // ถ้าส่งไม่ผ่าน (เช่น เน็ตหลุด) ให้ปล่อยผ่านไป ไม่ต้องให้ระบบล่ม
                Console.WriteLine($"Telegram Error: {ex.Message}");
            }
        }
    }
}