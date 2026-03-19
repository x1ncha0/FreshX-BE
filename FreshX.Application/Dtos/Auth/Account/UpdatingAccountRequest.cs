using System.ComponentModel.DataAnnotations;

namespace FreshX.Application.Dtos.Auth.Account
{
    public class UpdatingAccountRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? RoleId { get; set; }
        public bool? IsActive {  get; set; }
    }
}

