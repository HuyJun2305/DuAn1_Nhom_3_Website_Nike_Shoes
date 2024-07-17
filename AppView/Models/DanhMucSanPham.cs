using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace AppView.Models
{
    public class DanhMucSanPham
    {
        
        public Guid Id { get; set; }
        [StringLength(20,ErrorMessage = "Tên Danh mục quá dài")]
        public string TenDM { get; set; }
        public string ImgUrl { get; set; }
        public bool TrangThai { get; set; }
    }
}
