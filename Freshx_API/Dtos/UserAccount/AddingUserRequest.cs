using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.UserAccount
{
    public class AddingUserRequest
    {
        public string? FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? IdentityCardNumber { get; set; } // Số CMND/CCCD
        public string? WardId { get; set; } // ID phường/xã
        public string? DistrictId { get; set; } // ID quận/huyện
        public string? ProvinceId { get; set; } // ID tỉnh/thành phố
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phonenumber has 10 digits")]
        [Phone(ErrorMessage = "Phone number invalid")]
        public string? PhoneNumber { get; set; }
        public IFormFile? AvatarFile { get; set; }
    }
}
