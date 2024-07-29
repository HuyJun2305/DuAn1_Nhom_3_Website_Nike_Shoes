using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class con_sp_hd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDonChiTiets_sanPhams_SanPhamId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_hoaDonChiTiets_SanPhamId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropColumn(
                name: "SanPhamId",
                table: "hoaDonChiTiets");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_IdSP",
                table: "hoaDonChiTiets",
                column: "IdSP");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDonChiTiets_sanPhams_IdSP",
                table: "hoaDonChiTiets",
                column: "IdSP",
                principalTable: "sanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDonChiTiets_sanPhams_IdSP",
                table: "hoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_hoaDonChiTiets_IdSP",
                table: "hoaDonChiTiets");

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamId",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_SanPhamId",
                table: "hoaDonChiTiets",
                column: "SanPhamId");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDonChiTiets_sanPhams_SanPhamId",
                table: "hoaDonChiTiets",
                column: "SanPhamId",
                principalTable: "sanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
