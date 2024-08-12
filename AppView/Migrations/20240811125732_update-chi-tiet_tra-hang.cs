using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class updatechitiet_trahang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "NguoiXuLy",
                table: "traHangs");

            migrationBuilder.CreateTable(
                name: "ChiTietTraHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTraHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NguoiXuLy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietTraHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietTraHang_traHangs_IdTraHang",
                        column: x => x.IdTraHang,
                        principalTable: "traHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietTraHang_IdTraHang",
                table: "ChiTietTraHang",
                column: "IdTraHang");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietTraHang");

            

            migrationBuilder.AddColumn<string>(
                name: "NguoiXuLy",
                table: "traHangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            
        }
    }
}
