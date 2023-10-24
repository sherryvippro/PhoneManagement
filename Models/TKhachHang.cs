using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public partial class TKhachHang
    {
        public TKhachHang()
        {
            THoaDonBans = new HashSet<THoaDonBan>();
        }

        public string MaKh { get; set; } = null!;
        public string? TenKh { get; set; }
        public bool? GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public string? DienThoai { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<THoaDonBan> THoaDonBans { get; set; }
    }
}
