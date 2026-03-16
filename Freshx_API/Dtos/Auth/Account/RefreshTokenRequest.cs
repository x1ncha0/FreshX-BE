using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.Auth.Account
{
    public class RefreshTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
