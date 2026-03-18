using FreshX.Application.Constants;
using FreshX.Application.Dtos.Auth.Role;
using FreshX.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleNames.Admin)]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<RoleResponse>> Create([FromBody] AddingRole request, CancellationToken cancellationToken)
        {
            var role = await roleService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<RoleResponse>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await roleService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResponse>> GetById(string id, CancellationToken cancellationToken)
        {
            var role = await roleService.GetByIdAsync(id, cancellationToken);
            return role is null ? NotFound() : Ok(role);
        }
    }
}
