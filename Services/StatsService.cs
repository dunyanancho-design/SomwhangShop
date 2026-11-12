using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using SomwhangShop.Data; // ✅ เพิ่ม namespace นี้
using SomwhangShop.Models; // ✅ เพิ่ม namespace นี้
using Microsoft.EntityFrameworkCore; // ✅ เพิ่ม namespace นี้

namespace SomwhangShop.Services
{
    public class StatsService
    {
        private readonly string _connString;
        private readonly SomwhangShopContext _context; // ✅ เพิ่ม Context

        // ✅ อัปเดต Constructor ให้รับ Context เข้ามา
        public StatsService(IConfiguration config, SomwhangShopContext context)
        {
            _connString = config.GetConnectionString("DefaultConnection") ?? "";
            _context = context;
        }

        // ... (Method เดิม: RecordVisit, RecordProductView, ฯลฯ ปล่อยไว้เหมือนเดิม) ...

        // =========================================================
        // ✅ 1. ส่วนบันทึกข้อมูล (Write Methods) - คงเดิม
        // =========================================================
        public void RecordVisit(string? refId = null)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = @"INSERT INTO SiteVisits (VisitDate, VisitCount) 
                               VALUES (@Date, 1) 
                               ON DUPLICATE KEY UPDATE VisitCount = VisitCount + 1;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                    cmd.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(refId)) RecordAffiliateVisit(refId);
                CleanupOldData();
            }
            catch { }
        }

        public void RecordProductView(int productId)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = @"INSERT INTO ProductViews (ProductId, ViewCount) 
                               VALUES (@Pid, 1) 
                               ON DUPLICATE KEY UPDATE ViewCount = ViewCount + 1;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Pid", productId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        public void RecordAffiliateVisit(string affiliateId)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = @"INSERT INTO AffiliateVisits (AffiliateId, VisitDate, VisitCount) 
                               VALUES (@RefId, @Date, 1) 
                               ON DUPLICATE KEY UPDATE VisitCount = VisitCount + 1;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RefId", affiliateId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        public void CleanupOldData()
        {
            try
            {
                if (new Random().Next(1, 10) != 1) return;
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = "DELETE FROM SiteVisits WHERE VisitDate < DATE_SUB(NOW(), INTERVAL 90 DAY);";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        // ... (Method อ่านค่า Dashboard คงเดิม) ...
        public decimal GetTotalSales() => ExecuteScalar<decimal>("SELECT COALESCE(SUM(TotalAmount), 0) FROM Orders WHERE Status != 'ยกเลิก' AND Status != 'TEST'");
        public int GetTotalOrders() => ExecuteScalar<int>("SELECT COUNT(*) FROM Orders WHERE Status != 'TEST'");
        public int GetPendingSlipsCount() => ExecuteScalar<int>("SELECT COUNT(*) FROM Orders WHERE Status = 'รอตรวจสอบยอดเงิน'");

        public int GetTodayVisits()
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = "SELECT VisitCount FROM SiteVisits WHERE VisitDate = @Date";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Date", DateTime.Today);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch { return 0; }
        }

        public IEnumerable<dynamic> GetDailyVisits()
        {
            var result = new List<dynamic>();
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = "SELECT VisitDate, VisitCount FROM SiteVisits ORDER BY VisitDate DESC LIMIT 7";
                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new { VisitDate = Convert.ToDateTime(reader["VisitDate"]), VisitCount = Convert.ToInt32(reader["VisitCount"]) });
                }
            }
            catch { }
            result.Reverse();
            return result;
        }

        public IEnumerable<dynamic> GetTopProducts()
        {
            var result = new List<dynamic>();
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = @"
                    SELECT p.Name, COALESCE(v.ViewCount, 0) as ViewCount 
                    FROM Products p 
                    LEFT JOIN ProductViews v ON p.Id = v.ProductId 
                    WHERE p.IsActive = 1
                    ORDER BY ViewCount DESC, p.Id DESC 
                    LIMIT 10;";

                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new { Name = reader["Name"].ToString(), ViewCount = Convert.ToInt32(reader["ViewCount"]) });
                }
            }
            catch { }
            return result;
        }

        public IEnumerable<dynamic> GetProductSalesStats()
        {
            var result = new List<dynamic>();
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = @"
                    SELECT ProductName, SUM(Quantity) as TotalSold 
                    FROM OrderItems 
                    GROUP BY ProductId, ProductName 
                    ORDER BY TotalSold DESC 
                    LIMIT 10;";
                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new { Name = reader["ProductName"].ToString(), TotalSold = Convert.ToInt32(reader["TotalSold"]) });
                }
            }
            catch { }
            return result;
        }

        public IEnumerable<dynamic> GetTopAffiliates()
        {
            var result = new List<dynamic>();
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                string sql = @"SELECT AffiliateId, SUM(VisitCount) as TotalVisits FROM AffiliateVisits GROUP BY AffiliateId ORDER BY TotalVisits DESC LIMIT 10;";
                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new { AffiliateId = reader["AffiliateId"].ToString(), TotalVisits = Convert.ToInt32(reader["TotalVisits"]) });
                }
            }
            catch { }
            return result;
        }

        public IEnumerable<dynamic> GetAffiliateMonthlySales()
        {
            var result = new List<dynamic>();
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();

                string sql = @"
                    SELECT 
                        DATE_FORMAT(OrderDate, '%Y-%m') as SaleMonth, 
                        AffiliateId, 
                        COUNT(*) as TotalOrders, 
                        SUM(TotalAmount) as TotalSales 
                    FROM Orders 
                    WHERE Status != 'ยกเลิก' 
                      AND Status != 'TEST' 
                      AND AffiliateId IS NOT NULL 
                      AND AffiliateId != '' 
                    GROUP BY SaleMonth, AffiliateId 
                    ORDER BY SaleMonth DESC, TotalSales DESC;";

                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new
                    {
                        SaleMonth = reader["SaleMonth"].ToString(),
                        AffiliateId = reader["AffiliateId"].ToString(),
                        TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                        TotalSales = Convert.ToDecimal(reader["TotalSales"])
                    });
                }
            }
            catch { }
            return result;
        }

        // =========================================================
        // ✅ 5. เพิ่ม Method ดึงรายการออเดอร์สำหรับ Popup (ใช้ EF Core)
        // =========================================================
        public List<Order> GetOrdersByAffiliateAndMonth(string affiliateId, string monthYear)
        {
            try
            {
                // แปลงค่า yyyy-MM (เช่น 2024-05) เป็นวันที่เริ่มต้นและสิ้นสุดเดือน
                if (!DateTime.TryParseExact(monthYear + "-01", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime startDate))
                {
                    return new List<Order>();
                }

                var endDate = startDate.AddMonths(1);

                // ใช้ EF Core ดึงข้อมูลเพื่อความสะดวกและปลอดภัย
                return _context.Orders
                    .AsNoTracking() // เพิ่มประสิทธิภาพเพราะเราแค่อ่านมาโชว์
                    .Where(o => o.AffiliateId == affiliateId
                             && o.OrderDate >= startDate
                             && o.OrderDate < endDate
                             && o.Status != "ยกเลิก"
                             && o.Status != "TEST")
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();
            }
            catch
            {
                return new List<Order>();
            }
        }

        private T ExecuteScalar<T>(string sql)
        {
            try
            {
                using var conn = new MySqlConnection(_connString);
                conn.Open();
                using var cmd = new MySqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value) return default(T);
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch { return default(T); }
        }
    }
}