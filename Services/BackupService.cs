using MySql.Data.MySqlClient;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace SomwhangShop.Services
{
    public class BackupService
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public BackupService(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public async Task<string> CreateFullBackupAsync()
        {
            // 1. ตั้งค่า Path และชื่อไฟล์
            string backupFolder = Path.Combine(_env.WebRootPath, "backups");
            if (!Directory.Exists(backupFolder)) Directory.CreateDirectory(backupFolder);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string tempFolder = Path.Combine(backupFolder, $"temp_{timestamp}");
            string zipFileName = $"FullBackup_{timestamp}.zip";
            string zipFilePath = Path.Combine(backupFolder, zipFileName);

            Directory.CreateDirectory(tempFolder);

            try
            {
                // ---------------------------------------------------------
                // 🟢 STEP 1: Backup Database (.sql)
                // ---------------------------------------------------------
                string constring = _config.GetConnectionString("DefaultConnection");
                string sqlFilePath = Path.Combine(tempFolder, "database.sql");

                using (MySqlConnection conn = new MySqlConnection(constring))
                using (MySqlCommand cmd = new MySqlCommand())
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    mb.ExportToFile(sqlFilePath);
                    conn.Close();
                }

                // ---------------------------------------------------------
                // 🟢 STEP 2: Copy รูปภาพ (images folder)
                // ---------------------------------------------------------
                string sourceImagesPath = Path.Combine(_env.WebRootPath, "images");
                string destImagesPath = Path.Combine(tempFolder, "images");

                // ฟังก์ชัน Copy Directory แบบ Recursive
                CopyDirectory(sourceImagesPath, destImagesPath);

                // ---------------------------------------------------------
                // 🟢 STEP 3: Zip รวมกัน
                // ---------------------------------------------------------
                ZipFile.CreateFromDirectory(tempFolder, zipFilePath);

                // ---------------------------------------------------------
                // 🟢 STEP 4: ลบโฟลเดอร์ Temp ทิ้ง
                // ---------------------------------------------------------
                Directory.Delete(tempFolder, true);

                // ---------------------------------------------------------
                // 🟢 STEP 5: Rotation (ลบไฟล์เก่าเกิน 15 วัน)
                // ---------------------------------------------------------
                CleanUpOldBackups(backupFolder, 15);

                return zipFileName; // ส่งชื่อไฟล์กลับไป
            }
            catch (Exception ex)
            {
                // ถ้าพัง ให้ลบ temp ทิ้ง
                if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder, true);
                throw new Exception($"Backup Failed: {ex.Message}");
            }
        }

        // ฟังก์ชันลบไฟล์เก่า
        private void CleanUpOldBackups(string folderPath, int daysToKeep)
        {
            var directory = new DirectoryInfo(folderPath);
            var files = directory.GetFiles("FullBackup_*.zip");

            foreach (var file in files)
            {
                if (file.CreationTime < DateTime.Now.AddDays(-daysToKeep))
                {
                    try { file.Delete(); } catch { }
                }
            }
        }

        // ฟังก์ชัน Copy โฟลเดอร์ (รวม Sub-folders)
        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists) return;

            Directory.CreateDirectory(destinationDir);

            foreach (var file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            foreach (var subDir in dir.GetDirectories())
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir);
            }
        }
    }
}