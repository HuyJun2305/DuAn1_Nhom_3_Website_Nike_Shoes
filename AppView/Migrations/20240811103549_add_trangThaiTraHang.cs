using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class add_trangThaiTraHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_traHangs_donHangs_IdDH",
                table: "traHangs");

            

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiTraHang",
                table: "traHangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            

            migrationBuilder.AddForeignKey(
                name: "FK_traHangs_donHangs_IdDH",
                table: "traHangs",
                column: "IdDH",
                principalTable: "donHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_traHangs_donHangs_IdDH",
                table: "traHangs");           

            migrationBuilder.DropColumn(
                name: "TrangThaiTraHang",
                table: "traHangs");

         

            migrationBuilder.AddForeignKey(
                name: "FK_traHangs_donHangs_IdDH",
                table: "traHangs",
                column: "IdDH",
                principalTable: "donHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
