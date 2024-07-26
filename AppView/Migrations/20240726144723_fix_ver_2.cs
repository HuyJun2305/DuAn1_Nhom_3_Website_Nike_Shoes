using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class fix_ver_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_SanPhamsId",
                table: "gioHangChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_gioHangChiTiets_SanPhamsId",
                table: "gioHangChiTiets");

            migrationBuilder.DropColumn(
                name: "SanPhamsId",
                table: "gioHangChiTiets");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangChiTiets_IdSP",
                table: "gioHangChiTiets",
                column: "IdSP");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets",
                column: "IdGH",
                principalTable: "gioHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_IdSP",
                table: "gioHangChiTiets",
                column: "IdSP",
                principalTable: "sanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_IdSP",
                table: "gioHangChiTiets");

            migrationBuilder.DropIndex(
                name: "IX_gioHangChiTiets_IdSP",
                table: "gioHangChiTiets");

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamsId",
                table: "gioHangChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_gioHangChiTiets_SanPhamsId",
                table: "gioHangChiTiets",
                column: "SanPhamsId");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets",
                column: "IdGH",
                principalTable: "gioHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_SanPhamsId",
                table: "gioHangChiTiets",
                column: "SanPhamsId",
                principalTable: "sanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
