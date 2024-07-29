using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class connect_Kh_HD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_khachHangs_KhachHangId",
                table: "hoaDons");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_KhachHangId",
                table: "hoaDons");

            migrationBuilder.DropColumn(
                name: "KhachHangId",
                table: "hoaDons");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_IdKH",
                table: "hoaDons",
                column: "IdKH");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDons_khachHangs_IdKH",
                table: "hoaDons",
                column: "IdKH",
                principalTable: "khachHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_khachHangs_IdKH",
                table: "hoaDons");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_IdKH",
                table: "hoaDons");

            migrationBuilder.AddColumn<Guid>(
                name: "KhachHangId",
                table: "hoaDons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_KhachHangId",
                table: "hoaDons",
                column: "KhachHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDons_khachHangs_KhachHangId",
                table: "hoaDons",
                column: "KhachHangId",
                principalTable: "khachHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
