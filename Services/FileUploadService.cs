using Microsoft.AspNetCore.Components.Forms;

namespace SomwhangShop.Services
{
    public class FileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        // ✅ กำหนดนามสกุลไฟล์ที่อนุญาต (Whitelist)
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        public FileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(IBrowserFile file, string subFolder)
        {
            try
            {
                if (file == null) return "";

                // ---------------------------------------------------------------------
                // 🔒 1. ตรวจสอบความปลอดภัย (Security Check)
                // ---------------------------------------------------------------------

                // 1.1 ตรวจสอบนามสกุลไฟล์
                var extension = Path.GetExtension(file.Name).ToLowerInvariant();
                if (!_allowedExtensions.Contains(extension))
                {
                    throw new Exception($"ไม่รองรับไฟล์สกุล {extension} (อนุญาตเฉพาะรูปภาพเท่านั้น)");
                }

                // 1.2 ตรวจสอบ Content-Type (MIME Type) เบื้องต้น
                if (!file.ContentType.StartsWith("image/"))
                {
                    throw new Exception("ไฟล์ที่อัปโหลดไม่ใช่รูปภาพที่ถูกต้อง");
                }

                // 1.3 กำหนดขนาดไฟล์สูงสุด (5 MB)
                long maxFileSize = 1024 * 1024 * 5;
                if (file.Size > maxFileSize)
                {
                    throw new Exception("ไฟล์มีขนาดใหญ่เกินไป (สูงสุด 5MB)");
                }

                // ---------------------------------------------------------------------
                // 📂 2. กระบวนการบันทึกไฟล์ (Saving Process)
                // ---------------------------------------------------------------------

                // กำหนดโฟลเดอร์ปลายทาง
                string folderPath = Path.Combine(_environment.WebRootPath, "images", subFolder);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // สร้างชื่อไฟล์ใหม่แบบสุ่ม (Randomize Filename) เพื่อความปลอดภัยและกันชื่อซ้ำ
                string newFileName = $"{subFolder}_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString().Substring(0, 5)}{extension}";
                string fullPath = Path.Combine(folderPath, newFileName);

                // บันทึกไฟล์ลงเครื่อง
                using var stream = file.OpenReadStream(maxFileSize);
                using var fs = new FileStream(fullPath, FileMode.Create);
                await stream.CopyToAsync(fs);

                // คืนค่า URL สำหรับเก็บใน Database
                return $"/images/{subFolder}/{newFileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Upload Error: {ex.Message}");
                // คืนค่าว่าง หรือจะ throw error ต่อก็ได้ แล้วแต่การจัดการหน้าบ้าน
                // ในที่นี้เลือก throw เพื่อให้หน้าบ้านแจ้งเตือน user ได้
                throw;
            }
        }
    }
}