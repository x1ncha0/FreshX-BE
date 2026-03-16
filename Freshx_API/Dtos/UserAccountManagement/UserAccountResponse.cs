using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Dtos.UserAccountManagement
{
    public class UserAccountResponse
    {
        public string? IdentityCardNumber { get; set; } // Số CMND/CCCD ktra trùng lặp
        public string? FullName { get; set; } // Tên người dùng
        public string? Gender { get; set; } // Giới tính người dùng
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; } // Ngày sinh Người dùng
        public string? PhoneNumber { get; set; } // Số điện thoại Người dùng
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int? AvatarId { get; set; }
    }
}
