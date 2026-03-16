namespace Freshx_API.Dtos.Auth.Account
{
    public class UserDto
    {
        public string Email { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool IsActive { get; set; }
        public List<string>? Roles { get; set; } 
    }
}
