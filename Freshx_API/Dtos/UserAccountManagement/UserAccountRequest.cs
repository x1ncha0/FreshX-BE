using Freshx_API.Services.CommonServices.ValidationService;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos
{
    public class UserAccountRequest
    {

        [Required(ErrorMessage = "CMND/CCCD là bắt buộc")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "CCCD phải từ 12 ký tự")]
        [RegularExpression(@"^[0-9]{12}$", ErrorMessage = "CCCD chỉ được chứa số")]
        public string? IdentityCardNumber { get; set; } // Số CMND/CCCD ktra trùng lặp

        [Required(ErrorMessage = "Tên người dùng là bắt buộc")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên phải từ 2-100 ký tự")]
        [RegularExpression(@"^[a-zA-ZÀ-ỹ\s]*$", ErrorMessage = "Tên chỉ được chứa chữ cái và khoảng trắng")]
        public string? FullName { get; set; } // Tên người dùng

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        [RegularExpression("^(Nam|Nữ|Khác)$", ErrorMessage = "Giới tính không hợp lệ")]
        public string? Gender { get; set; } // Giới tính người dùng
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [AgeValidation(MinAge = 0, MaxAge = 150, ErrorMessage = "Tuổi phải từ 0-150")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; } // Ngày sinh người dùng
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải đúng 10 chữ số")]
        [RegularExpression(@"^(03|05|07|08|09)[0-9]{8}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? PhoneNumber { get; set; } // Số điện thoại bệnh nhân

        [Required(ErrorMessage = "Phường/Xã là bắt buộc")]
        public string? WardId { get; set; } // ID phường/xã

        [Required(ErrorMessage = "Quận/Huyện là bắt buộc")]
        public string? DistrictId { get; set; } // ID quận/huyện

        [Required(ErrorMessage = "Tỉnh/Thành phố là bắt buộc")]
        public string? ProvinceId { get; set; } // ID tỉnh/thành phố
        [AvatarValidation(MaxSizeInMb = 5,
        AllowedExtensions = new[] { ".jpg", ".jpeg", ".png" },
        ErrorMessage = "File ảnh không hợp lệ")]
        public IFormFile? AvatarFile { get; set; } //Hình ảnh của bệnh nhân 
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
