using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class PhanQuyen
    {
        public PhanQuyen()
        {
            Nguoidung = new HashSet<Nguoidung>();
        }

        [Key]
        public int IDQuyen { get; set; }

        [StringLength(20)]
        [Display(Name = "Tên quyền")]

        public string TenQuyen { get; set; }

        public virtual ICollection<Nguoidung> Nguoidung { get; set; }
    }
}
