namespace FreshX.Application.Dtos.Auth.Account
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool IsActive { get; set; }
        public List<string>? Roles { get; set; } 
    }
}

