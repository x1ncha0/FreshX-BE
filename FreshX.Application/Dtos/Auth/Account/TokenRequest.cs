using System.ComponentModel.DataAnnotations;

namespace FreshX.Application.Dtos.Auth.Account;

public class TokenRequest
{
    [Required]
    public string AccessToken { get; set; } = string.Empty;
}
