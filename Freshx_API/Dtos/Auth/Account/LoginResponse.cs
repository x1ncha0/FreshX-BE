namespace Freshx_API.Dtos.Auth.Account
{
    public class LoginResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public UserDto User { get; set; }
        public int LockoutTimeRemaining { get; set; }
        public string AccessToken  { get; set; }
        public DateTime ExpireAt { get; set; }
        public string RefreshToken { get; set; }
        
    }
}
