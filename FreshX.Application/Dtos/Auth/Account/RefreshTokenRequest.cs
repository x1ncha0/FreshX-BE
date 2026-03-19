using System.ComponentModel.DataAnnotations;

namespace FreshX.Application.Dtos.Auth.Account
{
    public class RefreshTokenRequest
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}

