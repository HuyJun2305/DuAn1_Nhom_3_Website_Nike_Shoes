using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class adđtra_hang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "traHangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhiHoanTra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayTraHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_traHangs_donHangs_IdDonHang",
                        column: x => x.IdDonHang,
                        principalTable: "donHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_traHangs_IdDonHang",
                table: "traHangs",
                column: "IdDonHang",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "traHangs");

           
        }
    }
}
