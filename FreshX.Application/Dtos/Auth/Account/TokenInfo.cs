namespace FreshX.Application.Dtos.Auth.Account
{
    public class TokenInfo
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}

