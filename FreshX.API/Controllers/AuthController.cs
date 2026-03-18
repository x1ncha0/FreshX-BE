using FreshX.Application.Dtos.Auth.Account;
using FreshX.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("get-user-id")]
    [AllowAnonymous]
    public IActionResult GetUserIdFromToken([FromBody] TokenRequest request)
    {
        var userId = authService.GetUserIdFromToken(request.AccessToken);
        return string.IsNullOrWhiteSpace(userId) ? Unauthorized(new { Message = "Token is invalid or expired." }) : Ok(new { UserId = userId });
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUserId()
    {
        var userId = authService.GetCurrentUserId();
        return string.IsNullOrWhiteSpace(userId) ? Unauthorized(new { Message = "Token is invalid or expired." }) : Ok(new { UserId = userId });
    }
}
