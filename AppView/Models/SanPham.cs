using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool TrangThai { get; set; }

        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; }
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }


        [ForeignKey("DanhMucSanPham")]
        public Guid IdDMSP { get; set; }
        public virtual DanhMucSanPham DanhMucSanPham { get; set; }

    }
}
