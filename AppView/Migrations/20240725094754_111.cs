using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class _111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_SanPhamsId",
                table: "gioHangChiTiets");

            migrationBuilder.AlterColumn<Guid>(
                name: "SanPhamsId",
                table: "gioHangChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSP",
                table: "gioHangChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdGH",
                table: "gioHangChiTiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_SanPhamsId",
                table: "gioHangChiTiets",
                column: "SanPhamsId",
                principalTable: "sanPhams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_SanPhamsId",
                table: "gioHangChiTiets");

            migrationBuilder.AlterColumn<Guid>(
                name: "SanPhamsId",
                table: "gioHangChiTiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSP",
                table: "gioHangChiTiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdGH",
                table: "gioHangChiTiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_gioHangChiTiets_sanPhams_SanPhamsId",
                table: "gioHangChiTiets",
                column: "SanPhamsId",
                principalTable: "sanPhams",
                principalColumn: "Id");
        }
    }
}
