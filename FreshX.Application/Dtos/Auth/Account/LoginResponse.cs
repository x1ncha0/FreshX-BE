namespace FreshX.Application.Dtos.Auth.Account
{
    public class LoginResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public UserDto User { get; set; } = null!;
        public int LockoutTimeRemaining { get; set; }
        public string AccessToken  { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        
    }
}

