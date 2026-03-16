using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshX.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public bool? IsActive { get; set; }

        public string? RefreshToken { get; set; }   
        
        public DateTime? ExpiredTime { get; set; }

        /// <summary>
        /// Số CMND/CCCD
        /// </summary>
        public string? IdentityCardNumber { get; set; }

        public int? AvatarId { get; set; }

        /// <summary>
        /// ID phường/xã
        /// </summary>
        public string? WardId { get; set; }

        /// <summary>
        /// ID quận/huyện
        /// </summary>
        public string? DistrictId { get; set; }

        /// <summary>
        /// ID tỉnh/thành phố
        /// </summary>
        public string? ProvinceId { get; set; }

        /// <summary>
        /// Địa chỉ chi tiết bệnh nhân
        /// </summary>
        public string? Address { get; set; }

        // Computed property for formatting
        [NotMapped]
        public string? FormattedAddress => string.Join(", ", new[]
            {
                Ward?.FullName,
                District?.FullName,
                Province?.FullName
            }.Where(x => !string.IsNullOrWhiteSpace(x)));

        public string? Gender { get; set; }

        /// <summary>
        /// Đơn vị hành chính phường/xã
        /// </summary>
        public virtual Ward? Ward { get; set; }

        /// <summary>
        /// Đơn vị hành chính huyện-thị trấn
        /// </summary>
        public virtual District? District { get; set; }

        /// <summary>
        /// Đơn vị hành chính tỉnh
        /// </summary>
        public virtual Province? Province { get; set; }

        public virtual Doctor? Doctor { get; set; }

        public virtual Employee? Employee { get; set; }

        public virtual Patient? Patient { get; set; }

        public virtual Technician? Technician { get; set; }
    }
}