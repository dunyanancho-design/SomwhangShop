using MySqlConnector;
using SomwhangShop.Models;
using Microsoft.Extensions.Configuration;

namespace SomwhangShop.Services
{
    public class ShopService
    {
        private readonly string _connString;

        public ShopService(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection");
        }

        public List<Shop> GetShops(bool activeOnly = true)
        {
            var list = new List<Shop>();
            using var conn = new MySqlConnection(_connString);
            conn.Open();

            string sql = "SELECT * FROM Shops";
            if (activeOnly) sql += " WHERE IsActive = 1";
            sql += " ORDER BY DisplayOrder";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Shop
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    ImageUrl = reader["ImageUrl"].ToString(),
                    LinkUrl = reader["LinkUrl"].ToString(),
                    PromptPayId = reader["PromptPayId"]?.ToString(),
                    PromptPayName = reader["PromptPayName"]?.ToString(),
                    QrCodeUrl = reader["QrCodeUrl"]?.ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]),

                    // อ่านค่า Config
                    CalcType = reader["CalcType"] != DBNull.Value ? reader["CalcType"].ToString() : "Weight",
                    BaseShippingFee = reader["BaseShippingFee"] != DBNull.Value ? Convert.ToDecimal(reader["BaseShippingFee"]) : 50,
                    FreeThreshold = reader["FreeThreshold"] != DBNull.Value ? Convert.ToDecimal(reader["FreeThreshold"]) : 0
                });
            }
            return list;
        }

        public Shop GetShopById(int id)
        {
            return GetShops(false).FirstOrDefault(s => s.Id == id);
        }

        public void SaveShop(Shop shop)
        {
            using var conn = new MySqlConnection(_connString);
            conn.Open();

            if (shop.Id == 0)
            {
                // INSERT
                string sql = @"INSERT INTO Shops 
                               (Name, Description, ImageUrl, LinkUrl, PromptPayId, PromptPayName, QrCodeUrl, IsActive, CalcType, BaseShippingFee, FreeThreshold) 
                               VALUES 
                               (@Name, @Desc, @Img, @Link, @PayId, @PayName, @Qr, @Active, @CalcType, @BaseFee, @FreeThreshold)";

                using var cmd = new MySqlCommand(sql, conn);
                AddParams(cmd, shop);
                cmd.ExecuteNonQuery();
            }
            else
            {
                // UPDATE
                string sql = @"UPDATE Shops SET 
                               Name=@Name, 
                               Description=@Desc, 
                               ImageUrl=@Img, 
                               LinkUrl=@Link, 
                               PromptPayId=@PayId, 
                               PromptPayName=@PayName, 
                               QrCodeUrl=@Qr, 
                               IsActive=@Active,
                               CalcType=@CalcType,           
                               BaseShippingFee=@BaseFee,
                               FreeThreshold=@FreeThreshold
                               WHERE Id=@Id";

                using var cmd = new MySqlCommand(sql, conn);
                AddParams(cmd, shop);
                cmd.Parameters.AddWithValue("@Id", shop.Id);
                cmd.ExecuteNonQuery();
            }
        }

        private void AddParams(MySqlCommand cmd, Shop shop)
        {
            cmd.Parameters.AddWithValue("@Name", shop.Name);
            cmd.Parameters.AddWithValue("@Desc", shop.Description);
            cmd.Parameters.AddWithValue("@Img", shop.ImageUrl);
            cmd.Parameters.AddWithValue("@Link", shop.LinkUrl);
            cmd.Parameters.AddWithValue("@PayId", shop.PromptPayId);
            cmd.Parameters.AddWithValue("@PayName", shop.PromptPayName);
            cmd.Parameters.AddWithValue("@Qr", shop.QrCodeUrl);
            cmd.Parameters.AddWithValue("@Active", shop.IsActive);

            // ✅ แก้ไข: ลบ ?? 50 และ ?? 0 ออก เพราะตัวแปรเป็น decimal ไม่ใช่ decimal?
            cmd.Parameters.AddWithValue("@CalcType", shop.CalcType ?? "Weight");
            cmd.Parameters.AddWithValue("@BaseFee", shop.BaseShippingFee);
            cmd.Parameters.AddWithValue("@FreeThreshold", shop.FreeThreshold);
        }

        public void DeleteShop(int id)
        {
            using var conn = new MySqlConnection(_connString);
            conn.Open();
            string sql = "DELETE FROM Shops WHERE Id = @Id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}