namespace Freshx_API.Dtos.Auth.Account
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public string? RoleName { get; set; }
    }
}
