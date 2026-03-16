using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.Auth.Account
{
    public class AddingAccountRequest
    {
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? RoleId { get; set; }

        public bool ? IsActive {  get; set; }
    }
}
