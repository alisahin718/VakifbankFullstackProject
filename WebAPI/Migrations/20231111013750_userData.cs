using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class userData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "ImageUrl", "IsActive", "Name", "Password" },
                values: new object[] { 1, new DateTime(2023, 11, 11, 1, 37, 50, 87, DateTimeKind.Utc).AddTicks(6369), "sahin@sahin.com", "", true, "Ali", "Ali.1234" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
