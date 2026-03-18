namespace FreshX.Application.Dtos.Auth.Account
{
    public class RegisterResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsActive { get; set; }
    }
}

