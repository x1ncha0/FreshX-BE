using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.Auth.Account
{
    public class UpdatingAccountRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string? RoleId { get; set; }
        public bool? IsActive {  get; set; }
    }
}
