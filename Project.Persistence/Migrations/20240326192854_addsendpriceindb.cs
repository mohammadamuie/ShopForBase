using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Persistence.Migrations
{
    public partial class addsendpriceindb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 8,
                column: "Value",
                value: "on");

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Key", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[] { 333, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "SendPrice", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "20000" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 8,
                column: "Value",
                value: "True");
        }
    }
}
