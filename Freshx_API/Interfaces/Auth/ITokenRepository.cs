using Freshx_API.Dtos.Auth.Account;
using Freshx_API.Models;

namespace Freshx_API.Interfaces.Auth
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
