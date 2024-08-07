using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class updaterelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_AspNetUsers_KhachHangId",
                table: "hoaDons");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_KhachHangId",
                table: "hoaDons");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1944909d-33ac-4475-afc9-5c52d92d2d51"), new Guid("2b91fc29-55ee-4276-81e2-3e4f4510b836") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("85e45cb5-93ca-4616-855e-5a9bd01cda4e"), new Guid("5c10ba59-084b-493b-b033-875ae5c44e59") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1944909d-33ac-4475-afc9-5c52d92d2d51"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("85e45cb5-93ca-4616-855e-5a9bd01cda4e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2b91fc29-55ee-4276-81e2-3e4f4510b836"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5c10ba59-084b-493b-b033-875ae5c44e59"));

            migrationBuilder.DropColumn(
                name: "KhachHangId",
                table: "hoaDons");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1252f4b5-36b4-4cb9-8bb0-f5969d264f9d"), "416bca2a-5e28-43e2-abb9-924b0fe3d853", "User", "USER" },
                    { new Guid("1b335466-7dd1-48a3-b946-8122d8cc108c"), "cd46fc13-05c9-47bd-b4eb-d07de360ef06", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImgUrl", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SDT", "SecurityStamp", "Ten", "TrangThai", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("01bebec1-f678-47d6-bd13-a904e21f2583"), 0, "8cdf1d3d-24dc-45ed-ba92-428644fdc976", "user@example.com", false, null, true, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJJJGsHXOkNrBf3qywfmGUo2i45uFMpqZJ2yUwFoqffiPmASbCVL9I2NoFYRLwwWdA==", null, false, "0987654321", "eacf72c7-bc94-4c52-9345-1094f8a0a543", "Regular User", null, false, "user@example.com" },
                    { new Guid("8cc5290c-b06b-4f1a-b789-4b4a6bde56be"), 0, "075668f3-12c4-4943-b934-07bab791f1b9", "admin@example.com", false, null, true, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ6Z1z4cn4oO6/LP5NEvAfXTq27yBfX/BJEH6j9GZdLT/1Mot9u1h7W516zl/s5CqA==", null, false, "0123456789", "4c3d33a5-7e60-46e1-b5df-9a7440fb9d63", "Admin User", null, false, "admin@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1252f4b5-36b4-4cb9-8bb0-f5969d264f9d"), new Guid("01bebec1-f678-47d6-bd13-a904e21f2583") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1b335466-7dd1-48a3-b946-8122d8cc108c"), new Guid("8cc5290c-b06b-4f1a-b789-4b4a6bde56be") });

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_IdKH",
                table: "hoaDons",
                column: "IdKH");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDons_AspNetUsers_IdKH",
                table: "hoaDons",
                column: "IdKH",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_AspNetUsers_IdKH",
                table: "hoaDons");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_IdKH",
                table: "hoaDons");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1252f4b5-36b4-4cb9-8bb0-f5969d264f9d"), new Guid("01bebec1-f678-47d6-bd13-a904e21f2583") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1b335466-7dd1-48a3-b946-8122d8cc108c"), new Guid("8cc5290c-b06b-4f1a-b789-4b4a6bde56be") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1252f4b5-36b4-4cb9-8bb0-f5969d264f9d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1b335466-7dd1-48a3-b946-8122d8cc108c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("01bebec1-f678-47d6-bd13-a904e21f2583"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8cc5290c-b06b-4f1a-b789-4b4a6bde56be"));

            migrationBuilder.AddColumn<Guid>(
                name: "KhachHangId",
                table: "hoaDons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1944909d-33ac-4475-afc9-5c52d92d2d51"), "09f0e7f3-6f95-47fa-830e-67b9d9697712", "Admin", "ADMIN" },
                    { new Guid("85e45cb5-93ca-4616-855e-5a9bd01cda4e"), "cd532f2b-c275-4595-b71b-fa040575f223", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImgUrl", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SDT", "SecurityStamp", "Ten", "TrangThai", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2b91fc29-55ee-4276-81e2-3e4f4510b836"), 0, "c246ca76-7286-4527-a74c-b5a022029c12", "admin@example.com", false, null, true, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAECzvK6V6SxrbL3zd9ObwhQllgqHwL3yacnnI8qcPF50PLSLMfmvFApJxggDO8iWTqg==", null, false, "0123456789", "f356d582-0fce-4354-92ee-9ff4299ada9a", "Admin User", null, false, "admin@example.com" },
                    { new Guid("5c10ba59-084b-493b-b033-875ae5c44e59"), 0, "e985e2ac-3d4b-4987-bfcb-4dafe7dce589", "user@example.com", false, null, true, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAEAACcQAAAAEEugpiG5mc4Ka01oW7IMaKan489lIfBbPWLNgnPufqFZNnRawikLgGwZQHH2gJqN9g==", null, false, "0987654321", "dec5b787-a6e0-4cd4-98dc-89c6bff0f23c", "Regular User", null, false, "user@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1944909d-33ac-4475-afc9-5c52d92d2d51"), new Guid("2b91fc29-55ee-4276-81e2-3e4f4510b836") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("85e45cb5-93ca-4616-855e-5a9bd01cda4e"), new Guid("5c10ba59-084b-493b-b033-875ae5c44e59") });

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_KhachHangId",
                table: "hoaDons",
                column: "KhachHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDons_AspNetUsers_KhachHangId",
                table: "hoaDons",
                column: "KhachHangId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
