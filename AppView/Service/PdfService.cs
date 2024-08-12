using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Font;
using System.IO;
using System.Linq;
using AppView.Models;

public class PdfService
{
    private readonly ApplicationDbContext _context;

    public PdfService(ApplicationDbContext context)
    {
        _context = context;
    }

    //public byte[] CreateInvoicePdf(HoaDon hoaDon)
    //{
        //using (var ms = new MemoryStream())
        //{
        //    using (var writer = new PdfWriter(ms))
        //    {
        //        using (var pdf = new PdfDocument(writer))
        //        {
        //            var document = new Document(pdf);

        //            // Đăng ký font chữ Unicode
        //            var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fonts", "arial.ttf");
        //            var font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

        //            // Thay đổi font chữ cho tài liệu
        //            document.SetFont(font);

        //            // Thêm tiêu đề
        //            document.Add(new Paragraph("Chi Tiết Hóa Đơn")
        //                .SetFont(font)
        //                .SetFontSize(18)
        //                .SetBold());

        //            // Thêm thông tin hóa đơn
        //            document.Add(new Paragraph($"ID Hóa Đơn: {hoaDon.Id}")
        //                .SetFont(font));
        //            document.Add(new Paragraph($"Ngày Lập: {hoaDon.NgayTao.ToString("dd/MM/yyyy")}")
        //                .SetFont(font));
        //            document.Add(new Paragraph($"Trạng Thái: {(hoaDon.TrangThai ? "Đã thanh toán" : "Đã hủy")}")
        //                .SetFont(font));

        //            // Thêm thông tin khách hàng
        //            document.Add(new Paragraph($"Tên Khách Hàng: {hoaDon.KhachHang.UserName}")
        //                .SetFont(font));
        //            document.Add(new Paragraph($"Số Điện Thoại: {hoaDon.KhachHang.Email}")
        //                .SetFont(font));

        //            // Thêm tiêu đề bảng
        //            document.Add(new Paragraph("Chi Tiết Sản Phẩm:")
        //                .SetFont(font)
        //                .SetBold());

        //            var table = new Table(4); // 4 cột: Tên sản phẩm, Số lượng, Đơn giá, Thành tiền

        //            // Thêm tiêu đề bảng
        //            table.AddHeaderCell("Tên Sản Phẩm");
        //            table.AddHeaderCell("Số Lượng");
        //            table.AddHeaderCell("Giá");
        //            table.AddHeaderCell("Thành Tiền");

        //            foreach (var donHang in hoaDon.DonHangs)
        //            {
        //                foreach (var chiTiet in donHang.ChiTietDonHangs)
        //                {
        //                    var sanPham = _context.sanPhams.FirstOrDefault(sp => sp.Id == chiTiet.IdSP);
        //                    var tenSanPham = sanPham != null ? sanPham.Ten : "Không xác định";

        //                    // Thêm hàng dữ liệu
        //                    table.AddCell(tenSanPham);
        //                    table.AddCell(chiTiet.SoLuong.ToString());
        //                    table.AddCell(chiTiet.Gia.ToString("N0") + "₫");
        //                    table.AddCell((chiTiet.Gia * chiTiet.SoLuong).ToString("N0") + "₫");
        //                }
        //            }

        //            // Thêm bảng vào tài liệu
        //            document.Add(table);

        //            // Thêm tổng tiền
        //            var tongTien = hoaDon.DonHangs.SelectMany(dh => dh.ChiTietDonHangs).Sum(ct => ct.SoLuong * ct.Gia);
        //            document.Add(new Paragraph($"Tổng Tiền: {tongTien.ToString("N0")}₫")
        //                .SetFont(font)
        //                .SetBold());

        //            document.Close();
        //        }
        //    }
        //    return ms.ToArray();
        }

