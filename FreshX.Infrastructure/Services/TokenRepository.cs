using FreshX.Application.Dtos.Auth.Account;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FreshX.Infrastructure.Services;

public class TokenRepository(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor) : ITokenRepository
{
    public Task<TokenInfo> IssueAccessToken(AppUser app)
    {
        throw new NotSupportedException("JWT issuance has not been migrated into FreshX.API yet.");
    }

    public string IssueRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<bool> SaveRefreshToken(string userName, string refreshToken)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return false;
        }

        user.RefreshToken = refreshToken;
        user.ExpiredTime = DateTime.UtcNow.AddDays(7);
        var result = await userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<string?> RetrieveUsernameByRefreshToken(string refreshToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.ExpiredTime > DateTime.UtcNow);
        return user?.UserName;
    }

    public async Task<AppUser?> RetrieveUserByRefreshToken(string refreshToken)
    {
        return await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.ExpiredTime > DateTime.UtcNow);
    }

    public string? GetUserIdFromToken(string token)
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? httpContextAccessor.HttpContext?.User.FindFirstValue("App_Id");
    }

    public string? GetUserIdFromToken()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? httpContextAccessor.HttpContext?.User.FindFirstValue("App_Id");
    }
}