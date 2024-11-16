using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Workloopz.ViewModels
{
    public class RegisterVM
    {
        public int Id { get; set; }
        [Display(Name ="Họ")]
        [Required(ErrorMessage ="*")]
        [MaxLength(50, ErrorMessage ="Tối đa 50 kí tự.")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "*")]
        [MaxLength(255, ErrorMessage = "Vượt quá số kí tự quy định")]
        public string Tittle { get; set; } = null!;

        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [MaxLength(255, ErrorMessage = "Vượt quá số kí tự quy định")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự.")]
        public string Username { get; set; } = null!;

        public string? Bu { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự.")]
        public string? JobTitle { get; set; } 
    }
}
