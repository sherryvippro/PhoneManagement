using Admin.Models;

namespace Admin.InputModels
{
    public class InputProducts
    {
        public InputProducts()
        {
            TChiTietHdbs = new HashSet<TChiTietHdb>();
            TChiTietHdns = new HashSet<TChiTietHdn>();
        }

        public string MaSp { get; set; } = null!;
        public string? TenSp { get; set; }
        public string? MaTl { get; set; }
        public string? MaHang { get; set; }
        public decimal? DonGiaNhap { get; set; }
        public decimal? DonGiaBan { get; set; }
        public int? SoLuong { get; set; }
        public IFormFile Anh { get; set; }

        public virtual THang? MaHangNavigation { get; set; }
        public virtual TTheLoai? MaTlNavigation { get; set; }
        public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; }
        public virtual ICollection<TChiTietHdn> TChiTietHdns { get; set; }
    }
}
