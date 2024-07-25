namespace AppView.Models
{
    public class GioHangChiTiet
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int SoLuong { get; set; }
        public Guid IdSP { get; set; }
        public virtual SanPham SanPhams { get; set; }
        public Guid IdGH { get; set; }
        public virtual GioHang GioHangs { get; set; }

    }
}
