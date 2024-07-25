using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class _123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_IdSP",
                table: "gioHangChiTiets");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets",
                column: "IdGH",
                principalTable: "gioHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_IdSP",
                table: "gioHangChiTiets",
                column: "IdSP",
                principalTable: "sanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_gioHangs_IdGH",
                table: "gioHangChiTiets");

            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_IdSP",
                table: "gioHangChiTiets");

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
    }
}
