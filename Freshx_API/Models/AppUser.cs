using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; } //tạo tài khoản
        public DateTime? UpdatedAt { get; set; }
        public string? RefreshToken { get; set; }           
        public DateTime? ExpiredTime { get; set; }
    /*    public int? DoctorId { get; set; }
        public int? EmployeeId { get; set; }
        public int? PatientId { get; set; }*/
        public string? IdentityCardNumber { get; set; } // Số CMND/CCCD
        public int? AvatarId { get; set; }
        public string? WardId { get; set; } // ID phường/xã
        public string? DistrictId { get; set; } // ID quận/huyện
        public string? ProvinceId { get; set; } // ID tỉnh/thành phố
        public string? Address { get; set; }
        // Địa chỉ chi tiết bệnh nhân
        // Computed property for formatting
        [NotMapped]
        public string? FormattedAddress => string.Join(", ", new[]
            {
                Ward?.FullName,
                District?.FullName,
                Province?.FullName
            }.Where(x => !string.IsNullOrWhiteSpace(x)));
        public string? Gender { get; set; }
        public virtual Ward? Ward { get; set; } // Đơn vị hành chính phường/xã
        public virtual District? District { get; set; } // Đơn vị hành chính huyện-thị trấn
        public virtual Province? Province { get; set; } // Đơn vị hành chính tỉnh
        public virtual Doctor? Doctor { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual Technician? Technician { get; set; }
        
    }
}
