using FreshX.Application.Dtos.Auth.Account;
using FreshX.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] AddingRegister request, CancellationToken cancellationToken)
    {
        var account = await accountService.RegisterAsync(request, cancellationToken);
        return Ok(account);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await accountService.LoginAsync(request, cancellationToken);
        return response.Succeeded
            ? Ok(response)
            : response.IsLockedOut
                ? StatusCode(StatusCodes.Status423Locked, response)
                : response.IsNotAllowed
                    ? StatusCode(StatusCodes.Status403Forbidden, response)
                    : BadRequest(response);
    }

    [HttpPost("RefreshToken")]
    [AllowAnonymous]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        return Ok(await accountService.RefreshTokenAsync(request, cancellationToken));
    }

    [HttpPost("Forgot-Password")]
    [AllowAnonymous]
    public async Task<ActionResult<ForgotPasswordResponse>> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        return Ok(await accountService.ForgotPasswordAsync(request, cancellationToken));
    }

    [HttpPost("verify-reset-otp")]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest request, CancellationToken cancellationToken)
    {
        await accountService.VerifyResetCodeAsync(request, cancellationToken);
        return NoContent();
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, [FromQuery] string code, CancellationToken cancellationToken)
    {
        await accountService.ResetPasswordAsync(code, request, cancellationToken);
        return NoContent();
    }

    [HttpPost("set-password")]
    [AllowAnonymous]
    public async Task<IActionResult> SetPassword([FromBody] SetPasswordRequest request, CancellationToken cancellationToken)
    {
        await accountService.SetPasswordAsync(request, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<AccountDto>> GetById(string id, CancellationToken cancellationToken)
    {
        var account = await accountService.GetByIdAsync(id, cancellationToken);
        return account is null ? NotFound() : Ok(account);
    }

    [HttpGet("Get-InformatonAccounts")]
    [Authorize]
    public async Task<ActionResult<IReadOnlyList<AccountDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await accountService.GetAllAsync(cancellationToken));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<AccountDto>> Update(string id, [FromBody] UpdatingAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await accountService.UpdateAsync(id, request, cancellationToken);
        return account is null ? NotFound() : Ok(account);
    }
}
