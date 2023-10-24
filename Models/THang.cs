using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public partial class THang
    {
        public THang()
        {
            TSp = new HashSet<TSp>();
        }

        public string MaHang { get; set; } = null!;
        public string? TenHang { get; set; }

        public virtual ICollection<TSp> TSp { get; set; }
    }
}
