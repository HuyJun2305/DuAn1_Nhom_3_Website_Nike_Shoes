namespace AppView.ViewModels
{
    public class DanhMucSanPhamViewModel
    {
        public Guid Id { get; set; }
        public string TenDM { get; set; }
        public string ImgUrl { get; set; }
        public bool TrangThai { get; set; }
        public int TongSoLuongSanPham { get; set; } // Tổng số lượng sản phẩm
    }

}
