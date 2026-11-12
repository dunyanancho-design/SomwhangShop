using MySql.Data.MySqlClient;
using SomwhangShop.Models;
using System.Data; // เพิ่ม namespace นี้สำหรับ check column

namespace SomwhangShop.Services
{
    public class ProductService
    {
        private readonly string _connString;

        public ProductService(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection") ?? "";
        }

        // 1. ดึงสินค้าทั้งหมด (หรือกรองตาม ShopId)
        public List<Product> GetProducts(int? shopId = null)
        {
            var list = new List<Product>();
            using var conn = new MySqlConnection(_connString);
            conn.Open();

            string sql = "SELECT * FROM Products ";
            if (shopId.HasValue)
            {
                sql += "WHERE ShopId = @ShopId ";
            }
            sql += "ORDER BY Id DESC"; // สินค้าใหม่มาบนสุด

            using var cmd = new MySqlCommand(sql, conn);
            if (shopId.HasValue) cmd.Parameters.AddWithValue("@ShopId", shopId.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    ShopId = Convert.ToInt32(reader["ShopId"]),
                    Name = reader["Name"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Description = reader["Description"].ToString(),
                    ImageUrl = reader["ImageUrl"].ToString(),
                    Category = reader["Category"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),

                    // อ่านค่า StockQuantity
                    StockQuantity = reader.HasColumn("StockQuantity") ? Convert.ToInt32(reader["StockQuantity"]) : 0,

                    // ✅ เพิ่มส่วนนี้: อ่านค่า Weight จากฐานข้อมูล (ถ้าไม่มีให้ Default 1.0)
                    Weight = reader.HasColumn("Weight") ? Convert.ToDecimal(reader["Weight"]) : 1.0m
                });
            }
            return list;
        }

        // 2. ดึงสินค้าตาม ID
        public Product? GetProductById(int id)
        {
            return GetProducts().FirstOrDefault(p => p.Id == id);
        }

        // 3. บันทึกสินค้า (เพิ่มใหม่ / แก้ไข)
        public void SaveProduct(Product p)
        {
            using var conn = new MySqlConnection(_connString);
            conn.Open();
            string sql;

            if (p.Id == 0) // Insert (เพิ่มสินค้าใหม่)
            {
                // ✅ เพิ่ม Weight ลงในคำสั่ง INSERT
                sql = @"INSERT INTO Products (ShopId, Name, Price, Description, ImageUrl, Category, IsActive, StockQuantity, Weight) 
                        VALUES (@ShopId, @Name, @Price, @Desc, @Img, @Cat, @Active, @Stock, @Weight)";
            }
            else // Update (แก้ไขสินค้าเดิม)
            {
                // ✅ เพิ่ม Weight ลงในคำสั่ง UPDATE
                sql = @"UPDATE Products SET ShopId=@ShopId, Name=@Name, Price=@Price, Description=@Desc, 
                        ImageUrl=@Img, Category=@Cat, IsActive=@Active, StockQuantity=@Stock, Weight=@Weight 
                        WHERE Id=@Id";
            }

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@ShopId", p.ShopId);
            cmd.Parameters.AddWithValue("@Name", p.Name);
            cmd.Parameters.AddWithValue("@Price", p.Price);
            cmd.Parameters.AddWithValue("@Desc", p.Description);
            cmd.Parameters.AddWithValue("@Img", p.ImageUrl);
            cmd.Parameters.AddWithValue("@Cat", p.Category);
            cmd.Parameters.AddWithValue("@Active", p.IsActive);
            cmd.Parameters.AddWithValue("@Stock", p.StockQuantity);

            // ✅ Map ค่า Weight ไปยัง Parameter
            cmd.Parameters.AddWithValue("@Weight", p.Weight);

            cmd.ExecuteNonQuery();
        }

        // 4. ลบสินค้า
        public void DeleteProduct(int id)
        {
            using var conn = new MySqlConnection(_connString);
            conn.Open();
            string sql = "DELETE FROM Products WHERE Id = @Id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }

    // Extension method ช่วยเช็ค Column ป้องกัน Error เก่า
    public static class DataRecordExtensions
    {
        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}