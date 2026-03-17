using FreshX.Application.Dtos.Auth.Account;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces.Auth
{
    public interface ITokenRepository
    {
        public Task<TokenInfo> IssueAccessToken(AppUser app);
        public string IssueRefreshToken();
        public Task<bool> SaveRefreshToken(string userName, string refreshToken);
        public Task<string?> RetrieveUsernameByRefreshToken(string refreshToken);
        public Task<AppUser?>  RetrieveUserByRefreshToken(string refreshToken);

        // Phương thức mới: Lấy ID người dùng từ token
        public string? GetUserIdFromToken(string token);
        public string? GetUserIdFromToken();

    }
}

