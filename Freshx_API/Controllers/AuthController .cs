using Freshx_API.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;

        public AuthController(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        [HttpPost("get-user-id")]
        [AllowAnonymous]
        public IActionResult GetUserIdFromToken([FromBody] TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.AccessToken))
            {
                return BadRequest(new { Message = "Token không được để trống" });
            }

            var userId = _tokenRepository.GetUserIdFromToken(request.AccessToken);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Token không hợp lệ hoặc đã hết hạn" });
            }

            return Ok(new { UserId = userId });
        }

        [HttpGet("me")]
        public IActionResult GetUserId()
        {
            var userId = _tokenRepository.GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized("Token không hợp lệ hoặc đã hết hạn.");
            }

            return Ok(new { UserId = userId });
        }
    }

    public class TokenRequest
    {
        public string AccessToken { get; set; }
    }
}
