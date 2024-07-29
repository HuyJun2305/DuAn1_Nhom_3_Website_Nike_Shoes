using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class fix_ver_2hd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDonChiTiets_hoaDons_HoaDonsId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_hoaDonChiTiets_sanPhams_SanPhamsId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_hoaDonChiTiets_HoaDonsId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_hoaDonChiTiets_SanPhamsId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropColumn(
                name: "HoaDonsId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropColumn(
                name: "SanPhamsId",
                table: "hoaDonChiTiets");

            migrationBuilder.AlterColumn<int>(
                name: "SoLuong",
                table: "hoaDonChiTiets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSP",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdHD",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Gia",
                table: "hoaDonChiTiets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HoaDonId",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamId",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_HoaDonId",
                table: "hoaDonChiTiets",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_SanPhamId",
                table: "hoaDonChiTiets",
                column: "SanPhamId");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDonChiTiets_hoaDons_HoaDonId",
                table: "hoaDonChiTiets",
                column: "HoaDonId",
                principalTable: "hoaDons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDonChiTiets_sanPhams_SanPhamId",
                table: "hoaDonChiTiets",
                column: "SanPhamId",
                principalTable: "sanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDonChiTiets_hoaDons_HoaDonId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_hoaDonChiTiets_sanPhams_SanPhamId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_hoaDonChiTiets_HoaDonId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_hoaDonChiTiets_SanPhamId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropColumn(
                name: "HoaDonId",
                table: "hoaDonChiTiets");

            migrationBuilder.DropColumn(
                name: "SanPhamId",
                table: "hoaDonChiTiets");

            migrationBuilder.AlterColumn<int>(
                name: "SoLuong",
                table: "hoaDonChiTiets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSP",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdHD",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<double>(
                name: "Gia",
                table: "hoaDonChiTiets",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "HoaDonsId",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamsId",
                table: "hoaDonChiTiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_HoaDonsId",
                table: "hoaDonChiTiets",
                column: "HoaDonsId");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_SanPhamsId",
                table: "hoaDonChiTiets",
                column: "SanPhamsId");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDonChiTiets_hoaDons_HoaDonsId",
                table: "hoaDonChiTiets",
                column: "HoaDonsId",
                principalTable: "hoaDons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDonChiTiets_sanPhams_SanPhamsId",
                table: "hoaDonChiTiets",
                column: "SanPhamsId",
                principalTable: "sanPhams",
                principalColumn: "Id");
        }
    }
}
