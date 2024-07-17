using System.ComponentModel.DataAnnotations;

namespace AppView.Models
{
    public class SanPham
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Ten { get; set; }
        public string? ImgFile { get; set; }
        [Required]
        public decimal Gia { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public int MauSac { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public string MoTa { get; set; }    

        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; }
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }
        public Guid? IdDMSP { get; set; }
        public virtual DanhMucSanPham? DanhMucSanPhams { get; set; }

    }
}
