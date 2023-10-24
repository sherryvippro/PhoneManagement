using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public partial class TChiTietHdb
    {
        public string SoHdb { get; set; } = null!;
        public string MaSp { get; set; } = null!;
        public int? Slban { get; set; }
        public string? KhuyenMai { get; set; }

        public virtual TSp MaSpNavigation { get; set; } = null!;
        public virtual THoaDonBan SoHdbNavigation { get; set; } = null!;
    }
}
