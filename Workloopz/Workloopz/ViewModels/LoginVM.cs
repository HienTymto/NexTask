using System.ComponentModel.DataAnnotations;

namespace Workloopz.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [MaxLength(255, ErrorMessage = "Vượt quá số kí tự quy định")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự.")]
        public string Username { get; set; } = null!;
    }
}
