using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class updb_123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("34134e56-fd39-4f9e-84fc-5e7bcbfc045f"), new Guid("23102f5b-d1bf-440c-bdcc-791339f88049") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b95ce05c-7938-4b77-a72f-77164f629090"), new Guid("c5ecabff-813d-4900-a130-0d9a32447e60") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("34134e56-fd39-4f9e-84fc-5e7bcbfc045f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b95ce05c-7938-4b77-a72f-77164f629090"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("23102f5b-d1bf-440c-bdcc-791339f88049"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5ecabff-813d-4900-a130-0d9a32447e60"));

            migrationBuilder.AlterColumn<Guid>(
                name: "IdHD",
                table: "donHangs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<Guid>(
                name: "IdHD",
                table: "donHangs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("34134e56-fd39-4f9e-84fc-5e7bcbfc045f"), "6f1ff480-5b27-40d2-9eea-73f684ccde79", "Admin", "ADMIN" },
                    { new Guid("b95ce05c-7938-4b77-a72f-77164f629090"), "eb33ceb7-91d0-4dcd-8383-1a9c0bcea004", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImgUrl", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SDT", "SecurityStamp", "Ten", "TrangThai", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("23102f5b-d1bf-440c-bdcc-791339f88049"), 0, "1b7a44b5-0e3f-4b86-b0f1-71970620c686", "admin@example.com", false, null, true, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAEOgq5Ojp8GgjVPS2ESAIrToVYAn9U3GGeKtmwXb9Qx4rVtkyIb81Fv2v/4TeC8QGNA==", null, false, "0123456789", "7254ba6d-3c68-4f06-8aeb-30af28edd24f", "Admin User", null, false, "admin@example.com" },
                    { new Guid("c5ecabff-813d-4900-a130-0d9a32447e60"), 0, "ed59cb40-d802-4389-8d97-6d9618e739d8", "user@example.com", false, null, true, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAEAACcQAAAAEH9xUPHYrSrUUqBijTQZGGJSaUC/u7pzIMTxQ4mBHIzaBhULas0vyfR7hnaMJEV3Yg==", null, false, "0987654321", "d0877ab8-7e0f-4f4d-a0ab-bd4a038fc85d", "Regular User", null, false, "user@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("34134e56-fd39-4f9e-84fc-5e7bcbfc045f"), new Guid("23102f5b-d1bf-440c-bdcc-791339f88049") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("b95ce05c-7938-4b77-a72f-77164f629090"), new Guid("c5ecabff-813d-4900-a130-0d9a32447e60") });
        }
    }
}
