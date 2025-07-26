using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Persistence.Migrations
{
    public partial class addDescriptionInDiscountCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DiscountCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "DiscountCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "DiscountCodes");
        }
    }
}
