using FreshX.Application.Interfaces.Auth;

namespace FreshX.Application.Services;

public class AuthService(ITokenRepository tokenRepository) : IAuthService
{
    public string? GetUserIdFromToken(string accessToken) => tokenRepository.GetUserIdFromToken(accessToken);

    public string? GetCurrentUserId() => tokenRepository.GetUserIdFromToken();
}
