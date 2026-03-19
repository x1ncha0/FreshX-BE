using FreshX.Application.Constants;
using FreshX.Application.Dtos.UnitOfMeasure;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitOfMeasureController(IUnitOfMeasureService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UnitOfMeasureDetailDto>>> GetAllUnitOfMeasures(
            [FromQuery] string? searchKeyword,
            [FromQuery] DateTime? createdDate,
            [FromQuery] DateTime? updatedDate,
            [FromQuery] int? isSuspended,
            [FromQuery] int? isDeleted,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, isDeleted, cancellationToken));
        }

        [HttpGet("id/{id:int}")]
        public async Task<ActionResult<UnitOfMeasureDetailDto>> GetUnitOfMeasureById(int id, CancellationToken cancellationToken)
        {
            var result = await service.GetByIdAsync(id, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("code/{code}")]
        public async Task<ActionResult<UnitOfMeasureDetailDto>> GetByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var result = await service.GetByCodeAsync(code, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<UnitOfMeasureDetailDto>> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var result = await service.GetByNameAsync(name, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<UnitOfMeasureDetailDto>> CreateAsync([FromBody] UnitOfMeasureCreateUpdateDto unitOfMeasureDto, CancellationToken cancellationToken)
        {
            var result = await service.CreateAsync(unitOfMeasureDto, cancellationToken);
            return CreatedAtAction(nameof(GetUnitOfMeasureById), new { id = result.UnitOfMeasureId }, result);
        }

        [HttpPut("id/{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> UpdateUnitOfMeasureById(int id, [FromBody] UnitOfMeasureCreateUpdateDto unitOfMeasureDto, CancellationToken cancellationToken)
        {
            await service.UpdateByIdAsync(id, unitOfMeasureDto, cancellationToken);
            return NoContent();
        }

        [HttpPut("code/{code}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> UpdateUnitOfMeasureByCode(string code, [FromBody] UnitOfMeasureCreateUpdateDto unitOfMeasureDto, CancellationToken cancellationToken)
        {
            await service.UpdateByCodeAsync(code, unitOfMeasureDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("id/{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> DeleteUnitOfMeasure(int id, CancellationToken cancellationToken)
        {
            await service.DeleteByIdAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpDelete("code/{code}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> DeleteAsyncCode(string code, CancellationToken cancellationToken)
        {
            await service.DeleteByCodeAsync(code, cancellationToken);
            return NoContent();
        }
    }
}
