using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Font;
using AppView.Models;

public class PdfService
{
    private readonly ApplicationDbContext _context;

    public PdfService(ApplicationDbContext context)
    {
        _context = context;
    }

    public byte[] CreateInvoicePdf(HoaDon hoaDon)
    {
        using (var ms = new MemoryStream())
        {
            using (var writer = new PdfWriter(ms))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    // Đăng ký font chữ Unicode
                    var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fonts", "arial.ttf");
                    var font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                    // Thay đổi font chữ cho tài liệu
                    document.SetFont(font);

                    // Thêm nội dung PDF
                    document.Add(new Paragraph("Hóa Đơn")
                        .SetFont(font)
                        .SetFontSize(18)
                        .SetBold());

                    document.Add(new Paragraph($"Mã Hóa Đơn: {hoaDon.Id}")
                        .SetFont(font));
                    document.Add(new Paragraph($"Ngày Lập: {hoaDon.NgayTao.ToString("dd/MM/yyyy")}")
                        .SetFont(font));
                    document.Add(new Paragraph($"Trạng Thái: {(hoaDon.TrangThai ? "Đã thanh toán" : "Đã Hủy")}")
                        .SetFont(font));
                    document.Add(new Paragraph($"Tổng Tiền: {hoaDon.HoaDonChiTiets.Sum(x => x.SoLuong * x.Gia).ToString("C")}")
                        .SetFont(font));

                    // Thêm chi tiết hóa đơn
                    document.Add(new Paragraph("Chi Tiết Hóa Đơn:")
                        .SetFont(font));
                    foreach (var chiTiet in hoaDon.HoaDonChiTiets)
                    {
                        var sanPham = _context.sanPhams.FirstOrDefault(sp => sp.Id == chiTiet.IdSP);
                        var tenSanPham = sanPham != null ? sanPham.Ten : "Không xác định";
                        document.Add(new Paragraph($"Sản phẩm: {tenSanPham}, Số lượng: {chiTiet.SoLuong}, Giá: {chiTiet.Gia.ToString("C")}")
                            .SetFont(font));
                    }

                    document.Close();
                }
            }
            return ms.ToArray();
        }
    }
}
