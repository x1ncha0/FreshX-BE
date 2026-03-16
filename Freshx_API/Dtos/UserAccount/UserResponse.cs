using Freshx_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.UserAccount
{
    public class UserResponse
    {
        public string? FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }   
        public string? Gender { get; set; }
        public string? IdentityCardNumber { get; set; } // Số CMND/CCCD
        public string? Email { get; set; }        
        public string? PhoneNumber { get; set; }
        public int? AvatarId { get; set; }
        public int? Age { get; set; }
        public Ward? Ward { get; set; }
        public District? District { get; set; }
        public Province? Province { get; set; }
        public string? Address
        {
            get
            {
                var addressParts = new List<string>();

                if (!string.IsNullOrWhiteSpace(Ward?.FullName))
                {
                    addressParts.Add(Ward.FullName);
                }
                if (!string.IsNullOrWhiteSpace(District?.FullName))
                {
                    addressParts.Add(District.FullName);
                }
                if (!string.IsNullOrWhiteSpace(Province?.FullName))
                {
                    addressParts.Add(Province.FullName);
                }

                return addressParts.Any() ? string.Join(", ", addressParts) : null;
            }
        }

    }
}
