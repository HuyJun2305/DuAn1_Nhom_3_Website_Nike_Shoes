namespace AppView.ViewModels
{
    public class SanPhamViewModel
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string ImgFile { get; set; } // Đường dẫn hình ảnh
    }

}
