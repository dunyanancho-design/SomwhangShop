using System.Text.Json;

namespace SomwhangShop.Services
{
    public class SiteStateService
    {
        // เก็บสถานะว่าเปิดหรือปิดร้าน
        public bool IsMaintenanceMode { get; private set; } = false;

        public event Action? OnChange;

        // ชื่อไฟล์ที่จะใช้บันทึกสถานะ (ระบบจะสร้างให้อัตโนมัติที่โฟลเดอร์รากของโปรเจกต์)
        private readonly string _filePath = "maintenance_config.json";

        public SiteStateService()
        {
            // 🟢 เมื่อเปิดโปรแกรม: ให้โหลดค่าล่าสุดจากไฟล์ทันที
            LoadState();
        }

        public async Task SetMaintenanceModeAsync(bool isMaintenance)
        {
            IsMaintenanceMode = isMaintenance;

            // 🔴 เมื่อมีการเปลี่ยนแปลง: ให้บันทึกลงไฟล์ทันที
            await SaveStateAsync();

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        // --- ฟังก์ชัน Load/Save ---

        private void LoadState()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    // ถ้าไฟล์อ่านได้ ให้ใช้ค่านั้น ถ้าไม่ได้ให้เป็น false (เปิดร้าน)
                    IsMaintenanceMode = JsonSerializer.Deserialize<bool>(json);
                }
            }
            catch
            {
                // ถ้ามีปัญหาตอนโหลด ให้ถือว่าเปิดร้านไปก่อน
                IsMaintenanceMode = false;
            }
        }

        private async Task SaveStateAsync()
        {
            try
            {
                string json = JsonSerializer.Serialize(IsMaintenanceMode);
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving maintenance state: {ex.Message}");
            }
        }
    }
}