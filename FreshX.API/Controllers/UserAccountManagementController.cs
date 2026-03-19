using FreshX.Application.Dtos;
using FreshX.Application.Dtos.UserAccountManagement;
using FreshX.Application.Interfaces.Auth;
using FreshX.Application.Interfaces.UserAccountManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAccountManagementController(
        IAuthService authService,
        IUserAccountManagementService userAccountManagementService) : ControllerBase
    {
        [HttpGet("Infomation-Account")]
        public async Task<ActionResult<UserAccountResponse>> GetInformationAccount(CancellationToken cancellationToken)
        {
            var id = authService.GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(id))
            {
                return Unauthorized();
            }

            var account = await userAccountManagementService.GetByIdAsync(id, cancellationToken);
            return account is null ? NotFound() : Ok(account);
        }

        [HttpPut("Update-Account")]
        public async Task<ActionResult<UserAccountResponse>> UpdateInformationAccount([FromForm] UserAccountRequest request, CancellationToken cancellationToken)
        {
            var id = authService.GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(id))
            {
                return Unauthorized();
            }

            var account = await userAccountManagementService.UpdateAsync(id, request, cancellationToken);
            return account is null ? NotFound() : Ok(account);
        }
    }
}
