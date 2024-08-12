using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class up_11_8_ver_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_donHangs_hoaDons_IdHD",
                table: "donHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_donHangs_IdDH",
                table: "hoaDons");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_IdDH",
                table: "hoaDons");

            migrationBuilder.DropIndex(
                name: "IX_donHangs_IdHD",
                table: "donHangs");

            

            migrationBuilder.CreateIndex(
                name: "IX_donHangs_IdHD",
                table: "donHangs",
                column: "IdHD",
                unique: true,
                filter: "[IdHD] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_donHangs_hoaDons_IdHD",
                table: "donHangs",
                column: "IdHD",
                principalTable: "hoaDons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_donHangs_hoaDons_IdHD",
                table: "donHangs");

            migrationBuilder.DropIndex(
                name: "IX_donHangs_IdHD",
                table: "donHangs");

            

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_IdDH",
                table: "hoaDons",
                column: "IdDH");

            migrationBuilder.CreateIndex(
                name: "IX_donHangs_IdHD",
                table: "donHangs",
                column: "IdHD");

            migrationBuilder.AddForeignKey(
                name: "FK_donHangs_hoaDons_IdHD",
                table: "donHangs",
                column: "IdHD",
                principalTable: "hoaDons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDons_donHangs_IdDH",
                table: "hoaDons",
                column: "IdDH",
                principalTable: "donHangs",
                principalColumn: "Id");
        }
    }
}
