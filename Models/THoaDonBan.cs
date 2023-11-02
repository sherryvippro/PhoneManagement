using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public partial class THoaDonBan
    {
        public THoaDonBan()
        {
            TChiTietHdbs = new HashSet<TChiTietHdb>();
        }

        public string SoHdb { get; set; } = null!;
        public DateTime? NgayBan { get; set; }
        public string? MaNguoiDung { get; set; }
        public decimal? TongHdb { get; set; }

        public virtual Nguoidung? MaKhNavigation { get; set; }
        public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; }

    }
}
