using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Tls;
using SomwhangShop.Models; // ✅ เรียกใช้ CartItem จาก Namespace ที่ถูกต้อง
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SomwhangShop.Services // ✅ แก้เป็น Namespace ปัจจุบัน
{
    public class EmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await ExecuteSendEmail(email, subject, htmlMessage);
        }

        // ฟังก์ชันส่งใบยืนยันคำสั่งซื้อ
        public async Task SendOrderConfirmationAsync(string customerEmail, string customerName, string orderId, List<CartItem> items, decimal totalAmount, string shippingAddress)
        {
            var subject = $"ยืนยันคำสั่งซื้อ #{orderId} - SomwhangShop";
            var body = GenerateHtmlTemplate(customerName, orderId, items, totalAmount, shippingAddress);

            await ExecuteSendEmail(customerEmail, subject, body);
        }

        private async Task ExecuteSendEmail(string toEmail, string subject, string body)
        {
            try
            {
                // อ่านค่าจาก appsettings.json (ถ้ายังไม่มี ให้แก้ตรงนี้เป็น email/password ของคุณชั่วคราวได้)
                var senderEmail = _config["EmailSettings:SenderEmail"] ?? "somwhangshop@gmail.com";
                var senderPassword = _config["EmailSettings:SenderPassword"] ?? "your-app-password";
                var senderName = _config["EmailSettings:SenderName"] ?? "SomwhangShop";
                var host = _config["EmailSettings:Host"] ?? "smtp.gmail.com";
                //var host = "smtp.gmail.com";
                var port = int.Parse(_config["EmailSettings:Port"] ?? "587");

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(senderEmail, senderName);
                    //message.From = new MailAddress(senderEmail, "SomwhangShop");
                    message.To.Add(new MailAddress(toEmail));
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    using (var client = new SmtpClient(host, port))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        await client.SendMailAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }

        private string GenerateHtmlTemplate(string customerName, string orderId, List<CartItem> items, decimal totalAmount, string shippingAddress)
        {
            var sb = new StringBuilder();

            sb.Append(@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; background-color: #fff;'>
                    <div style='background-color: #f57224; padding: 20px; text-align: center; color: white;'>
                        <h2 style='margin:0;'>คำสั่งซื้อสำเร็จ!</h2>
                        <p style='margin: 5px 0 0;'>ขอบคุณที่ช้อปกับ SomwhangShop</p>
                    </div>
                    <div style='padding: 20px;'>
                        <p>สวัสดีคุณ <strong>" + customerName + @"</strong>,</p>
                        <p>เราได้รับคำสั่งซื้อ <strong>#" + orderId + @"</strong> ของคุณแล้ว</p>
                        
                        <table style='width: 100%; border-collapse: collapse; margin-top: 20px;'>
                            <tr style='background-color: #f8f9fa; border-bottom: 2px solid #eee;'>
                                <th style='padding: 10px; text-align: left;'>สินค้า</th>
                                <th style='padding: 10px; text-align: right; width: 80px;'>จำนวน</th>
                                <th style='padding: 10px; text-align: right; width: 100px;'>รวม</th>
                            </tr>");

            foreach (var item in items)
            {
                sb.Append($@"
                            <tr>
                                <td style='padding: 10px; border-bottom: 1px solid #eee;'>
                                    <strong>{item.ProductName}</strong>
                                </td>
                                <td style='padding: 10px; text-align: right; border-bottom: 1px solid #eee;'>{item.Quantity}</td>
                                <td style='padding: 10px; text-align: right; border-bottom: 1px solid #eee;'>฿{(item.Price * item.Quantity):N0}</td>
                            </tr>");
            }

            sb.Append($@"
                            <tr>
                                <td colspan='2' style='padding: 10px; text-align: right;'><strong>ราคารวมสุทธิ</strong></td>
                                <td style='padding: 10px; text-align: right; color: #f57224; font-size: 18px;'><strong>฿{totalAmount:N0}</strong></td>
                            </tr>
                        </table>

                        <div style='background-color: #f8f9fa; padding: 15px; margin-top: 20px; border-radius: 5px; font-size: 14px;'>
                            <strong>📦 ที่อยู่จัดส่ง:</strong><br/>
                            {shippingAddress}
                        </div>
                    </div>
                </div>");

            return sb.ToString();
        }
    }
}