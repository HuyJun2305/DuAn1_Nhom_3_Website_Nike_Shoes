using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class fix_hoadon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_khachHangs_KhachHangsId",
                table: "hoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_sanPhams_danhMucSanPhams_DanhMucSanPhamsId",
                table: "sanPhams");

            migrationBuilder.DropIndex(
                name: "IX_sanPhams_DanhMucSanPhamsId",
                table: "sanPhams");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_KhachHangsId",
                table: "hoaDons");

            migrationBuilder.DropColumn(
                name: "DanhMucSanPhamsId",
                table: "sanPhams");

            migrationBuilder.DropColumn(
                name: "KhachHangsId",
                table: "hoaDons");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdDMSP",
                table: "sanPhams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdKH",
                table: "hoaDons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KhachHangId",
                table: "hoaDons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_IdDMSP",
                table: "sanPhams",
                column: "IdDMSP");

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

            migrationBuilder.AddForeignKey(
                name: "FK_sanPhams_danhMucSanPhams_IdDMSP",
                table: "sanPhams",
                column: "IdDMSP",
                principalTable: "danhMucSanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoaDons_khachHangs_KhachHangId",
                table: "hoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_sanPhams_danhMucSanPhams_IdDMSP",
                table: "sanPhams");

            migrationBuilder.DropIndex(
                name: "IX_sanPhams_IdDMSP",
                table: "sanPhams");

            migrationBuilder.DropIndex(
                name: "IX_hoaDons_KhachHangId",
                table: "hoaDons");

            migrationBuilder.DropColumn(
                name: "KhachHangId",
                table: "hoaDons");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdDMSP",
                table: "sanPhams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DanhMucSanPhamsId",
                table: "sanPhams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdKH",
                table: "hoaDons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "KhachHangsId",
                table: "hoaDons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_DanhMucSanPhamsId",
                table: "sanPhams",
                column: "DanhMucSanPhamsId");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_KhachHangsId",
                table: "hoaDons",
                column: "KhachHangsId");

            migrationBuilder.AddForeignKey(
                name: "FK_hoaDons_khachHangs_KhachHangsId",
                table: "hoaDons",
                column: "KhachHangsId",
                principalTable: "khachHangs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sanPhams_danhMucSanPhams_DanhMucSanPhamsId",
                table: "sanPhams",
                column: "DanhMucSanPhamsId",
                principalTable: "danhMucSanPhams",
                principalColumn: "Id");
        }
    }
}
