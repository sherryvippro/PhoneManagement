using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class Nguoidung
    {
        public Nguoidung()
        {
            Donhang = new HashSet<THoaDonBan>();
        }

        [Key]
        public int MaNguoiDung { get; set; }
        [Display(Name = "Mã Người dùng")]
        public string Anhdaidien { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Họ tên bắt buộc nhập")]
        public string Hoten { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email bắt buộc nhập")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail sai định dạng")]
        public string Email { get; set; }

        [StringLength(10)]
        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Điện thoại bắt buộc nhập")]
        public string Dienthoai { get; set; }

        [Required(ErrorMessage = "Mật khẩu bắt buộc nhập")]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "Độ dài mật khẩu từ 8-20 kí tự")]
        [Display(Name = "Mật khẩu")]
        public string Matkhau { get; set; }

        public int? IDQuyen { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        public string Diachi { get; set; }

        public virtual ICollection<THoaDonBan> Donhang { get; set; }

        public virtual PhanQuyen PhanQuyen { get; set; }
        public object THoaDonBan { get; internal set; }
    }
}
