using FreshX.Application.Dtos.Auth.Account;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FreshX.Infrastructure.Services;

public class TokenRepository(
    IConfiguration configuration,
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor) : ITokenRepository
{
    public Task<TokenInfo> IssueAccessToken(AppUser app)
    {
        var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key configuration is required.");
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new("App_Id", app.Id),
            new(ClaimTypes.NameIdentifier, app.Id),
            new(ClaimTypes.Name, app.UserName ?? app.Email ?? app.Id),
            new(ClaimTypes.Email, app.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Sub, app.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        return BuildTokenAsync();

        async Task<TokenInfo> BuildTokenAsync()
        {
            var roles = await userManager.GetRolesAsync(app);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expiresAt = DateTime.UtcNow.AddHours(1);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new TokenInfo
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt
            };
        }
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
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key configuration is required.");
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = !string.IsNullOrWhiteSpace(issuer),
                ValidIssuer = issuer,
                ValidateAudience = !string.IsNullOrWhiteSpace(audience),
                ValidAudience = audience,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return principal.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? principal.FindFirstValue("App_Id")
                ?? principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        }
        catch
        {
            return null;
        }
    }

    public string? GetUserIdFromToken()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? httpContextAccessor.HttpContext?.User.FindFirstValue("App_Id");
    }
}
