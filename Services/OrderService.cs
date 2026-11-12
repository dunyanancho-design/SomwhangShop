using System.Data;
using MySql.Data.MySqlClient;
using SomwhangShop.Models;
using Microsoft.Extensions.Logging;

namespace SomwhangShop.Services
{
    public class OrderService
    {
        private readonly string _connString;
        private readonly ShippingService _shippingService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IConfiguration config, ShippingService shippingService, ILogger<OrderService> logger)
        {
            _connString = config.GetConnectionString("DefaultConnection") ?? "";
            _shippingService = shippingService;
            _logger = logger;
        }

        // =================================================================================
        // 1. ฟังก์ชันสั่งซื้อ (PlaceOrder) - ปรับปรุงใหม่
        // หน้าที่: ตรวจสอบราคาสินค้าจริง -> คำนวณค่าส่ง -> บันทึกออเดอร์ -> ตัดสต็อก (อย่างปลอดภัย)
        // =================================================================================
        public async Task PlaceOrder(Order order)
        {
            // สร้าง OrderId ถ้ายังไม่มี
            if (string.IsNullOrEmpty(order.OrderId))
                order.OrderId = "ORD-" + DateTime.Now.ToString("yyyyMMddHHmmss");

            if (order.OrderDate == DateTime.MinValue)
                order.OrderDate = DateTime.Now;

            string affiliateInfo = string.IsNullOrEmpty(order.AffiliateId) ? "ลูกค้าทั่วไป" : $"นายหน้า: {order.AffiliateId}";
            LogToConsole($"📦 เริ่มสั่งซื้อ: ID {order.OrderId} ({affiliateInfo}) | ลูกค้า: {order.CustomerName}");

            using var conn = new MySqlConnection(_connString);
            await conn.OpenAsync();

            // ✅ เริ่ม Transaction (ถ้ามีขั้นตอนไหนพัง ให้ยกเลิกทั้งหมด)
            using var transaction = await conn.BeginTransactionAsync();

            try
            {
                decimal verifiedTotalProductPrice = 0;
                LogToConsole("🔍 กำลังตรวจสอบราคาและสต็อกสินค้าล่าสุดจากฐานข้อมูล...");

                // 1.1 ตรวจสอบราคาสินค้าจริงจาก DB (ป้องกันคนแอบแก้ราคาหน้าเว็บ)
                foreach (var item in order.Items)
                {
                    if (item.Product.Id > 0)
                    {
                        string sqlGetProduct = "SELECT Price, Name FROM Products WHERE Id = @Pid";
                        using (var cmdProd = new MySqlCommand(sqlGetProduct, conn, transaction))
                        {
                            cmdProd.Parameters.AddWithValue("@Pid", item.Product.Id);
                            using (var reader = await cmdProd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    // อัปเดตราคาและชื่อให้ตรงกับ DB ปัจจุบัน
                                    item.Product.Price = Convert.ToDecimal(reader["Price"]);
                                    item.Product.Name = reader["Name"].ToString();
                                }
                                else
                                {
                                    throw new Exception($"ไม่พบสินค้า ID {item.Product.Id} ในระบบ");
                                }
                            }
                        }
                        // อัปเดตราคาในรายการ
                        item.Price = item.Product.Price;
                        verifiedTotalProductPrice += item.TotalPrice;
                    }
                }

                // 1.2 คำนวณค่าส่งใหม่ (Server-side Calculation)
                string courierName = string.IsNullOrEmpty(order.CourierName) ? "Flash" : order.CourierName;
                decimal verifiedShippingCost = await _shippingService.CalculateCostAsync(order.Items, courierName);
                LogToConsole($"🚚 คำนวณค่าส่งใหม่: {verifiedShippingCost} บาท (ขนส่ง: {courierName})");

                // 1.3 สรุปยอดเงินสุทธิที่ถูกต้อง
                order.ShippingCost = verifiedShippingCost;
                order.TotalAmount = verifiedTotalProductPrice + verifiedShippingCost;
                order.CourierName = courierName;

                // 1.4 บันทึกข้อมูลลงตาราง Orders
                string sqlOrder = @"INSERT INTO Orders 
                                    (OrderId, CustomerName, PhoneNumber, Email, ShippingAddress, SlipImageUrl, TotalAmount, Status, OrderDate, TransRef, AffiliateId, ShippingCost, CourierName) 
                                    VALUES 
                                    (@Id, @Name, @Phone, @Email, @Addr, @Slip, @Total, @Status, @Date, @Ref, @AffiliateId, @ShipCost, @Courier)";

                using (var cmd = new MySqlCommand(sqlOrder, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@Id", order.OrderId);
                    cmd.Parameters.AddWithValue("@Name", order.CustomerName);
                    cmd.Parameters.AddWithValue("@Phone", order.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", order.Email);
                    cmd.Parameters.AddWithValue("@Addr", order.ShippingAddress);
                    cmd.Parameters.AddWithValue("@Slip", order.SlipImageUrl ?? "");
                    cmd.Parameters.AddWithValue("@Total", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@Status", order.Status);
                    cmd.Parameters.AddWithValue("@Date", order.OrderDate);
                    cmd.Parameters.AddWithValue("@Ref", order.TransRef);
                    cmd.Parameters.AddWithValue("@AffiliateId", order.AffiliateId);
                    cmd.Parameters.AddWithValue("@ShipCost", order.ShippingCost);
                    cmd.Parameters.AddWithValue("@Courier", order.CourierName);
                    await cmd.ExecuteNonQueryAsync();
                }

                // 1.5 บันทึกรายการสินค้า (OrderItems) และตัดสต็อก
                foreach (var item in order.Items)
                {
                    // Insert รายการสินค้า
                    string sqlItem = @"INSERT INTO OrderItems (OrderId, ProductId, ProductName, Price, Quantity, TotalPrice) 
                                       VALUES (@OrderId, @ProductId, @PName, @Price, @Qty, @Total)";
                    using (var cmdItem = new MySqlCommand(sqlItem, conn, transaction))
                    {
                        cmdItem.Parameters.AddWithValue("@OrderId", order.OrderId);
                        cmdItem.Parameters.AddWithValue("@ProductId", item.Product.Id);
                        cmdItem.Parameters.AddWithValue("@PName", item.Product.Name);
                        cmdItem.Parameters.AddWithValue("@Price", item.Product.Price);
                        cmdItem.Parameters.AddWithValue("@Qty", item.Quantity);
                        cmdItem.Parameters.AddWithValue("@Total", item.TotalPrice);
                        await cmdItem.ExecuteNonQueryAsync();
                    }

                    // ✅ ตัดสต็อกสินค้า (Atomic Stock Update)
                    // เพิ่มเงื่อนไข AND StockQuantity >= @Qty เพื่อป้องกันสต็อกติดลบ
                    string sqlUpdateStock = "UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE Id = @Id AND StockQuantity >= @Qty";
                    using (var cmdStock = new MySqlCommand(sqlUpdateStock, conn, transaction))
                    {
                        cmdStock.Parameters.AddWithValue("@Qty", item.Quantity);
                        cmdStock.Parameters.AddWithValue("@Id", item.Product.Id);
                        int affected = await cmdStock.ExecuteNonQueryAsync();

                        // ถ้า affected = 0 แสดงว่าของหมด หรือ ID ผิด (Database ไม่ยอมให้ตัด)
                        if (affected == 0) throw new Exception($"สินค้า '{item.Product.Name}' มีจำนวนไม่พอสำหรับการสั่งซื้อนี้");
                    }
                }

                // ยืนยัน Transaction (Save จริง)
                await transaction.CommitAsync();
                LogToConsole($"✅ ออเดอร์ {order.OrderId} บันทึกสำเร็จ!");
            }
            catch (Exception ex)
            {
                // ถ้ามีอะไรผิดพลาด ให้ย้อนกลับข้อมูลทั้งหมด
                await transaction.RollbackAsync();
                LogToConsole($"❌ เกิดข้อผิดพลาด: {ex.Message}", isError: true);
                throw; // โยน Error กลับไปให้หน้าบ้านรู้
            }
        }

        // =================================================================================
        // 2. ฟังก์ชันดึงออเดอร์ตาม ID (คงเดิม ไม่เปลี่ยนแปลง)
        // =================================================================================
        public async Task<Order?> GetOrderByIdAsync(string orderId)
        {
            using var conn = new MySqlConnection(_connString);
            await conn.OpenAsync();

            string sqlOrder = "SELECT * FROM Orders WHERE OrderId = @Id";
            using var cmd = new MySqlCommand(sqlOrder, conn);
            cmd.Parameters.AddWithValue("@Id", orderId);

            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            var order = new Order
            {
                OrderId = reader["OrderId"].ToString() ?? "",
                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                ShippingCost = Convert.ToDecimal(reader["ShippingCost"]),
                CourierName = reader["CourierName"].ToString() ?? "",
                CustomerName = reader["CustomerName"].ToString() ?? "",
                ShippingAddress = reader["ShippingAddress"].ToString() ?? "",
                PhoneNumber = reader["PhoneNumber"].ToString() ?? "",
                Status = reader["Status"].ToString() ?? "",
                SlipImageUrl = reader["SlipImageUrl"].ToString() ?? ""
            };
            await reader.CloseAsync();

            string sqlItems = "SELECT * FROM OrderItems WHERE OrderId = @Id";
            using var cmdItems = new MySqlCommand(sqlItems, conn);
            cmdItems.Parameters.AddWithValue("@Id", orderId);

            using var readerItems = await cmdItems.ExecuteReaderAsync();
            while (await readerItems.ReadAsync())
            {
                var productName = readerItems["ProductName"].ToString() ?? "";
                var price = Convert.ToDecimal(readerItems["Price"]);
                var quantity = Convert.ToInt32(readerItems["Quantity"]);
                var productId = Convert.ToInt32(readerItems["ProductId"]);

                order.Items.Add(new CartItem
                {
                    ProductName = productName,
                    Price = price,
                    Quantity = quantity,
                    Product = new Product
                    {
                        Id = productId,
                        Name = productName,
                        Price = price
                    }
                });
            }

            return order;
        }

        private void LogToConsole(string message, bool isError = false)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = isError ? ConsoleColor.Red : ConsoleColor.Cyan;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 🧾 ระบบออเดอร์: {message}");
            Console.ForegroundColor = originalColor;
        }
    }
}