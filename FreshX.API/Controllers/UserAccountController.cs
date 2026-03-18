using FreshX.Application.Constants;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.UserAccount;
using FreshX.Application.Interfaces.UserAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleNames.Admin)]
    public class UserAccountController(IUserAccountService userAccountService) : ControllerBase
    {
        [HttpPost("Create-NewUser")]
        public async Task<ActionResult<UserResponse>> Create([FromForm] AddingUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userAccountService.CreateAsync(request, cancellationToken);
            return Ok(user);
        }

        [HttpGet("Get-AllUsers")]
        public async Task<ActionResult<CustomPageResponse<IEnumerable<UserResponse?>>>> GetAll([FromQuery] Parameters parameters, CancellationToken cancellationToken)
        {
            return Ok(await userAccountService.GetAsync(parameters, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetById(string id, CancellationToken cancellationToken)
        {
            var user = await userAccountService.GetByIdAsync(id, cancellationToken);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            await userAccountService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> Update(string id, [FromForm] UpdatingUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userAccountService.UpdateAsync(id, request, cancellationToken);
            return user is null ? NotFound() : Ok(user);
        }
    }
}
