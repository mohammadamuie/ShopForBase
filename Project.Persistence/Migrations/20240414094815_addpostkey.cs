using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Persistence.Migrations
{
    public partial class addpostkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostKey",
                table: "PurchaseRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostKey",
                table: "PurchaseRequests");
        }
    }
}
