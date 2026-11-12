using System.ComponentModel.DataAnnotations;

namespace SomwhangShop.Models
{
    public class AppSetting
    {
        [Key]
        public string SettingKey { get; set; } // เช่น "MaintenanceMode"
        public string SettingValue { get; set; } // "true" หรือ "false"
    }
}