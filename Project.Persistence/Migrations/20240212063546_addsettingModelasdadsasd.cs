using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Persistence.Migrations
{
    public partial class addsettingModelasdadsasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "UserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "RoleClaims");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserClaims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "RoleClaims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Key", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "SiteName", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "فرشوگاه آنلاین" },
                    { 31, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step2Banner2Link", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 16, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step3Banner1", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp" },
                    { 32, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step3Banner1Link", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 55, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step3Banner2", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp" },
                    { 33, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step3Banner2Link", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 17, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step3Banner3", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp" },
                    { 344, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step3Banner3Link", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 15, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step2Banner2", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp" },
                    { 18, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step3Banner4", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp" },
                    { 19, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step4Banner", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/Images/media-643e9c17da34f.jpg" },
                    { 35, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step4BannerLink", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 20, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Telegram", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "" },
                    { 21, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Instagram", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "" },
                    { 22, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Whatsapp", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "" },
                    { 23, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Email", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "" },
                    { 244, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Telphone", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "" },
                    { 34, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step3Banner4Link", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 24, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "PhoneNumber", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "" },
                    { 30, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step2Banner1Link", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 13, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "AmazingOffersBanner", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/images/1.jpg" },
                    { 2, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "MetaDescription", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم متن ساختگی" },
                    { 3, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "MetaKeyWord", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم متن ساختگی" },
                    { 4, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "ShortcutIcon", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/assets/images/icons/favicon.ico" },
                    { 5, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "HeaderIcon", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/assets/images/demos/demo-3/logo.png" },
                    { 6, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "FooterIcon", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/assets/images/demos/demo-3/logo.png" },
                    { 7, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "TopIcon", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/images/58fb25ef5ef526b9c362c81a839dd931eafb6a46_1707298697.gif" },
                    { 25, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "TopIconLink", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 14, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "Step2Banner1", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/Images/0007575b0b65e5d07cf2abb9e9501880690c664e_1674463984.webp" },
                    { 8, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "TopIconBool", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "True" },
                    { 26, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "BigBannerLink", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 10, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "FirstBanner", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/images/20cfa2fef7747b0fde8ba6875a0788325f5d081d_1706101976.webp" },
                    { 27, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "FirstBannerLink", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 11, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "SecondBanner", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/images/b596a2fd4ae4dd03bfb424ca83bd703af01ca85c_1706351348.webp" },
                    { 28, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "SecondBannerLink", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 12, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "ThirdBanner", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/images/61ed3d9edfbf5ca49898b5a3f7b32293a5c2c26f_1683011290.webp" },
                    { 29, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "ThirdBannerLink", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "#" },
                    { 9, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "BigBanner", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "/images/1690875299-esv04wsL6tPNeV3u.webp" },
                    { 36, new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", true, "FooterText", new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc), "SYSTEM", "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.RenameTable(
                name: "UserTokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
