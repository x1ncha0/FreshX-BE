namespace FreshX.Application.Dtos.Auth.Account
{
    public class RegisterResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsActive { get; set; }
    }
}

