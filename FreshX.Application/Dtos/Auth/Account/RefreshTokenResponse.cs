namespace FreshX.Application.Dtos.Auth.Account;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
}
