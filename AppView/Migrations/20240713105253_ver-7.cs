using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppView.Migrations
{
    public partial class ver7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "danhMucSanPhams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDM = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danhMucSanPhams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gioHangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhachHangsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gioHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gioHangs_khachHangs_KhachHangsId",
                        column: x => x.KhachHangsId,
                        principalTable: "khachHangs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "hoaDons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KhachHangsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hoaDons_khachHangs_KhachHangsId",
                        column: x => x.KhachHangsId,
                        principalTable: "khachHangs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhuongThuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sanPhams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    MauSac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDMSP = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DanhMucSanPhamsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sanPhams_danhMucSanPhams_DanhMucSanPhamsId",
                        column: x => x.DanhMucSanPhamsId,
                        principalTable: "danhMucSanPhams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "gioHangChiTiets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    IdSP = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SanPhamsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdGH = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GioHangsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gioHangChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gioHangChiTiets_gioHangs_GioHangsId",
                        column: x => x.GioHangsId,
                        principalTable: "gioHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gioHangChiTiets_sanPhams_SanPhamsId",
                        column: x => x.SanPhamsId,
                        principalTable: "sanPhams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "hoaDonChiTiets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    IdSP = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SanPhamsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdHD = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HoaDonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdTT = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThanhToansId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDonChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hoaDonChiTiets_hoaDons_HoaDonsId",
                        column: x => x.HoaDonsId,
                        principalTable: "hoaDons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hoaDonChiTiets_sanPhams_SanPhamsId",
                        column: x => x.SanPhamsId,
                        principalTable: "sanPhams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hoaDonChiTiets_ThanhToan_ThanhToansId",
                        column: x => x.ThanhToansId,
                        principalTable: "ThanhToan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_gioHangChiTiets_GioHangsId",
                table: "gioHangChiTiets",
                column: "GioHangsId");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangChiTiets_SanPhamsId",
                table: "gioHangChiTiets",
                column: "SanPhamsId");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangs_KhachHangsId",
                table: "gioHangs",
                column: "KhachHangsId");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_HoaDonsId",
                table: "hoaDonChiTiets",
                column: "HoaDonsId");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_SanPhamsId",
                table: "hoaDonChiTiets",
                column: "SanPhamsId");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonChiTiets_ThanhToansId",
                table: "hoaDonChiTiets",
                column: "ThanhToansId");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_KhachHangsId",
                table: "hoaDons",
                column: "KhachHangsId");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_DanhMucSanPhamsId",
                table: "sanPhams",
                column: "DanhMucSanPhamsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gioHangChiTiets");

            migrationBuilder.DropTable(
                name: "hoaDonChiTiets");

            migrationBuilder.DropTable(
                name: "gioHangs");

            migrationBuilder.DropTable(
                name: "hoaDons");

            migrationBuilder.DropTable(
                name: "sanPhams");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "danhMucSanPhams");
        }
    }
}
