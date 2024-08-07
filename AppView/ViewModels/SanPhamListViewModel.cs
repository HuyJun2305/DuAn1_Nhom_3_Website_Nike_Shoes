using AppView.Models;

namespace AppView.ViewModels
{
    public class SanPhamListViewModel
    {
        // Danh sách sản phẩm hiện tại trên trang
        public IEnumerable<SanPhamViewModel> Products { get; set; }

        // Danh sách danh mục sản phẩm để hiển thị bên cạnh
        public IEnumerable<DanhMucSanPhamViewModel> Categories { get; set; }

        // Trang hiện tại
        public int CurrentPage { get; set; }

        // Tổng số trang
        public int TotalPages { get; set; }
    }

}
