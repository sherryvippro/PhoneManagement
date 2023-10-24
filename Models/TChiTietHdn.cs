using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public partial class TChiTietHdn
    {
        public string SoHdn { get; set; } = null!;
        public string MaSp { get; set; } = null!;
        public int? Slnhap { get; set; }
        public string? KhuyenMai { get; set; }

        public virtual TSp MaSpNavigation { get; set; } = null!;
        public virtual THoaDonNhap SoHdnNavigation { get; set; } = null!;
    }
}
