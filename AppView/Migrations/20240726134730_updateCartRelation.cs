using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class updateCartRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_GioHangsId",
                table: "gioHangChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_gioHangChiTiets_GioHangsId",
                table: "gioHangChiTiets");

            migrationBuilder.DropColumn(
                name: "GioHangsId",
                table: "gioHangChiTiets");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangChiTiets_IdGH",
                table: "gioHangChiTiets",
                column: "IdGH");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets",
                column: "IdGH",
                principalTable: "gioHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_gioHangChiTiets_IdGH",
                table: "gioHangChiTiets");

            migrationBuilder.AddColumn<Guid>(
                name: "GioHangsId",
                table: "gioHangChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_gioHangChiTiets_GioHangsId",
                table: "gioHangChiTiets",
                column: "GioHangsId");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_GioHangsId",
                table: "gioHangChiTiets",
                column: "GioHangsId",
                principalTable: "gioHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
