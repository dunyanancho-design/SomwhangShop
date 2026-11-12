using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SomwhangShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTrackingNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ ปิดโค้ดชั่วคราว: เพราะคอลัมน์ TrackingNumber มีอยู่แล้วในฐานข้อมูลจริง
            /* migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "Orders");
        }
    }
}