using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class up_11_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietTraHang_traHangs_IdTraHang",
                table: "ChiTietTraHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietTraHang",
                table: "ChiTietTraHang");

            

            migrationBuilder.RenameTable(
                name: "ChiTietTraHang",
                newName: "chiTietTraHangs");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietTraHang_IdTraHang",
                table: "chiTietTraHangs",
                newName: "IX_chiTietTraHangs_IdTraHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chiTietTraHangs",
                table: "chiTietTraHangs",
                column: "Id");

            

            migrationBuilder.AddForeignKey(
                name: "FK_chiTietTraHangs_traHangs_IdTraHang",
                table: "chiTietTraHangs",
                column: "IdTraHang",
                principalTable: "traHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chiTietTraHangs_traHangs_IdTraHang",
                table: "chiTietTraHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chiTietTraHangs",
                table: "chiTietTraHangs");

            

            migrationBuilder.RenameTable(
                name: "chiTietTraHangs",
                newName: "ChiTietTraHang");

            migrationBuilder.RenameIndex(
                name: "IX_chiTietTraHangs_IdTraHang",
                table: "ChiTietTraHang",
                newName: "IX_ChiTietTraHang_IdTraHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietTraHang",
                table: "ChiTietTraHang",
                column: "Id");

            

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietTraHang_traHangs_IdTraHang",
                table: "ChiTietTraHang",
                column: "IdTraHang",
                principalTable: "traHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
