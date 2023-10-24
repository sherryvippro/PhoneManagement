using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public partial class TAdmin
    {
        public string MaAdmin { get; set; } = null!;
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public int? Role { get; set; }
    }
}
