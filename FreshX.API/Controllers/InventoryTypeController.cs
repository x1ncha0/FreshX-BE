using FreshX.Application.Constants;
using FreshX.Application.Dtos.InventoryType;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryTypeController(IInventoryTypeService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InventoryTypeDto>>> GetAll([FromQuery] string? searchKeyword, CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, cancellationToken));
        }

        [HttpGet("id/{id:int}")]
        public async Task<ActionResult<InventoryTypeDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await service.GetByIdAsync(id, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("code/{code}")]
        public async Task<ActionResult<InventoryTypeDto>> GetByCode(string code, CancellationToken cancellationToken)
        {
            var result = await service.GetByCodeAsync(code, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<InventoryTypeDto>> GetByName(string name, CancellationToken cancellationToken)
        {
            var result = await service.GetByNameAsync(name, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<InventoryTypeDto>> Create([FromBody] InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            var result = await service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.InventoryTypeId }, result);
        }

        [HttpPut("id/{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> UpdateId(int id, [FromBody] InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            await service.UpdateByIdAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpPut("code/{code}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> UpdateCode(string code, [FromBody] InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            await service.UpdateByCodeAsync(code, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("id/{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await service.DeleteByIdAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpDelete("code/{code}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> DeleteCode(string code, CancellationToken cancellationToken)
        {
            await service.DeleteByCodeAsync(code, cancellationToken);
            return NoContent();
        }
    }
}
