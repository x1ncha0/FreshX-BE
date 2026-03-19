namespace FreshX.Application.Dtos.Auth.Account
{
    public class AccountDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public string? RoleName { get; set; }
    }
}

