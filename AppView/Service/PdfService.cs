using AppView.Models;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

public class PdfService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PdfService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
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

                    // Lấy userId từ session và chuyển đổi sang Guid
                    var userIdString = _httpContextAccessor.HttpContext.Session.GetString("userId");
                    if (Guid.TryParse(userIdString, out Guid userId))
                    {
                        // Tìm khách hàng trong cơ sở dữ liệu dựa vào userId
                        var khachHang = _context.Users.FirstOrDefault(u => u.Id == userId);

                        if (khachHang == null)
                        {
                            throw new Exception("Không tìm thấy khách hàng.");
                        }

                        // Thêm tiêu đề
                        document.Add(new Paragraph("Chi Tiết Hóa Đơn")
                            .SetFont(font)
                            .SetFontSize(18)
                            .SetBold());

                        // Thêm thông tin hóa đơn
                        document.Add(new Paragraph($"ID Hóa Đơn: {hoaDon.Id}")
                            .SetFont(font));
                        document.Add(new Paragraph($"Ngày Lập: {hoaDon.NgayTao.ToString("dd/MM/yyyy")}")
                            .SetFont(font));
                        document.Add(new Paragraph($"Trạng Thái: {(hoaDon.TrangThai ? "Đã thanh toán" : "Đã hủy")}")
                            .SetFont(font));

                        // Thêm thông tin khách hàng
                        if (khachHang != null)
                        {
                            document.Add(new Paragraph($"Tên Khách Hàng: {khachHang.UserName}")
                                .SetFont(font));
                            document.Add(new Paragraph($"Email: {khachHang.Email}")
                                .SetFont(font));
                        }
                        else
                        {
                            document.Add(new Paragraph("Thông tin khách hàng không có sẵn.")
                                .SetFont(font));
                        }

                        // Thêm thông tin người bán (Seller)
                        if (!string.IsNullOrEmpty(hoaDon.NguoiBan))
                        {
                            document.Add(new Paragraph($"Người Bán: {hoaDon.NguoiBan}")
                                .SetFont(font));
                        }
                        else
                        {
                            document.Add(new Paragraph("Thông tin người bán không có sẵn.")
                                .SetFont(font));
                        }

                        // Lấy thông tin đơn hàng liên kết
                        var donHang = hoaDon.DonHang;  // Quan hệ 1-1

                        if (donHang != null)
                        {
                            // Thêm tiêu đề bảng chi tiết sản phẩm
                            document.Add(new Paragraph("Chi Tiết Sản Phẩm:")
                                .SetFont(font)
                                .SetBold());

                            var table = new Table(4); // 4 cột: Tên sản phẩm, Số lượng, Đơn giá, Thành tiền

                            // Thêm tiêu đề bảng
                            table.AddHeaderCell("Tên Sản Phẩm");
                            table.AddHeaderCell("Số Lượng");
                            table.AddHeaderCell("Giá");
                            table.AddHeaderCell("Thành Tiền");

                            // Kiểm tra chi tiết đơn hàng
                            if (donHang.ChiTietDonHangs != null && donHang.ChiTietDonHangs.Any())
                            {
                                foreach (var chiTiet in donHang.ChiTietDonHangs)
                                {
                                    var sanPham = _context.sanPhams.FirstOrDefault(sp => sp.Id == chiTiet.IdSP);
                                    var tenSanPham = sanPham != null ? sanPham.Ten : "Không xác định";

                                    // Thêm hàng dữ liệu vào bảng
                                    table.AddCell(tenSanPham);
                                    table.AddCell(chiTiet.SoLuong.ToString());
                                    table.AddCell(chiTiet.Gia.ToString("N0") + "₫");
                                    table.AddCell((chiTiet.Gia * chiTiet.SoLuong).ToString("N0") + "₫");
                                }

                                // Thêm bảng vào tài liệu
                                document.Add(table);

                                // Tính tổng tiền
                                var tongTien = donHang.ChiTietDonHangs.Sum(ct => ct.SoLuong * ct.Gia);
                                document.Add(new Paragraph($"Tổng Tiền: {tongTien.ToString("N0")}₫")
                                    .SetFont(font)
                                    .SetBold());
                            }
                            else
                            {
                                document.Add(new Paragraph("Không có chi tiết sản phẩm.")
                                    .SetFont(font));
                            }
                        }
                        else
                        {
                            document.Add(new Paragraph("Thông tin đơn hàng không có sẵn.")
                                .SetFont(font));
                        }

                        // Đóng tài liệu
                        document.Close();
                    }
                    else
                    {
                        throw new Exception("UserId trong session không hợp lệ.");
                    }
                }
            }
            return ms.ToArray();
        }
    }
}
