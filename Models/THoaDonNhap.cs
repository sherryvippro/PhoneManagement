using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public partial class THoaDonNhap
    {
        public THoaDonNhap()
        {
            TChiTietHdns = new HashSet<TChiTietHdn>();
        }

        public string SoHdn { get; set; } = null!;
        public DateTime? NgayNhap { get; set; }
        public string? MaNcc { get; set; }
        public decimal? TongHdn { get; set; }

        public virtual TNhaCungCap? MaNccNavigation { get; set; }
        public virtual ICollection<TChiTietHdn> TChiTietHdns { get; set; }
    }
}
