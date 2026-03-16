namespace Freshx_API.Dtos.Auth.Account
{
    public class TokenInfo
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
