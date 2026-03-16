using Freshx_API.Services.CommonServices.ValidationService;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.Auth.Account
{
    public class AddingRegister
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [StrongPassword]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100,MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; }
    }
}
