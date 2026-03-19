namespace FreshX.Application.Interfaces.Auth;

public interface IAuthService
{
    string? GetUserIdFromToken(string accessToken);
    string? GetCurrentUserId();
}
