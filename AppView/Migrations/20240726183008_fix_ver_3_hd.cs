using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class fix_ver_3_hd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDonChiTiets_hoaDons_HoaDonId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_hoaDonChiTiets_HoaDonId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropColumn(
                name: "HoaDonId",
                table: "hoaDonChiTiets");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_IdHD",
                table: "hoaDonChiTiets",
                column: "IdHD");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDonChiTiets_hoaDons_IdHD",
                table: "hoaDonChiTiets",
                column: "IdHD",
                principalTable: "hoaDons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDonChiTiets_hoaDons_IdHD",
                table: "hoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_hoaDonChiTiets_IdHD",
                table: "hoaDonChiTiets");

            migrationBuilder.AddColumn<Guid>(
                name: "HoaDonId",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_HoaDonId",
                table: "hoaDonChiTiets",
                column: "HoaDonId");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDonChiTiets_hoaDons_HoaDonId",
                table: "hoaDonChiTiets",
                column: "HoaDonId",
                principalTable: "hoaDons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
