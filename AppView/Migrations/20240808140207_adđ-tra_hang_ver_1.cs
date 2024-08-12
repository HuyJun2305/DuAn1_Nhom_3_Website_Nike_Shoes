using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class adđtra_hang_ver_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_traHangs_donHangs_IdDonHang",
                table: "traHangs");

            migrationBuilder.DropIndex(
                name: "IX_traHangs_IdDonHang",
                table: "traHangs");

           
            migrationBuilder.DropColumn(
                name: "IdDonHang",
                table: "traHangs");

            
            migrationBuilder.CreateIndex(
                name: "IX_traHangs_IdDH",
                table: "traHangs",
                column: "IdDH",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_traHangs_donHangs_IdDH",
                table: "traHangs",
                column: "IdDH",
                principalTable: "donHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_traHangs_donHangs_IdDH",
                table: "traHangs");

            migrationBuilder.DropIndex(
                name: "IX_traHangs_IdDH",
                table: "traHangs");

           
            migrationBuilder.AddColumn<Guid>(
                name: "IdDonHang",
                table: "traHangs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            

            migrationBuilder.CreateIndex(
                name: "IX_traHangs_IdDonHang",
                table: "traHangs",
                column: "IdDonHang",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_traHangs_donHangs_IdDonHang",
                table: "traHangs",
                column: "IdDonHang",
                principalTable: "donHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
