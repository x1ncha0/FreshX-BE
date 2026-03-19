using FreshX.Application.Constants;
using FreshX.Application.Dtos.Supplier;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController(ISupplierService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SupplierDetailDto>>> GetAllSuppliers(
            [FromQuery] string? searchKeyword,
            [FromQuery] DateTime? createdDate,
            [FromQuery] DateTime? updatedDate,
            [FromQuery] bool? isSuspended,
            [FromQuery] bool? isForeign,
            [FromQuery] bool? isStateOwned,
            [FromQuery] int? isDeleted,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, isForeign, isStateOwned, isDeleted, cancellationToken));
        }

        [HttpGet("id/{id:int}")]
        public async Task<ActionResult<SupplierDetailDto>> GetSupplierById(int id, CancellationToken cancellationToken)
        {
            var result = await service.GetByIdAsync(id, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("code/{code}")]
        public async Task<ActionResult<SupplierDetailDto>> GetSupplierByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var result = await service.GetByCodeAsync(code, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<SupplierDetailDto>> CreateSupplier([FromBody] SupplierCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetSupplierById), new { id = result.SupplierId }, result);
        }

        [HttpPut("code/{code}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> UpdateSupplierByCode(string code, [FromBody] SupplierUpdateDto dto, CancellationToken cancellationToken)
        {
            await service.UpdateByCodeAsync(code, dto, cancellationToken);
            return NoContent();
        }

        [HttpPut("id/{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> UpdateSupplierById(int id, [FromBody] SupplierUpdateDto dto, CancellationToken cancellationToken)
        {
            await service.UpdateByIdAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("id/{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> DeleteSupplier(int id, CancellationToken cancellationToken)
        {
            await service.DeleteByIdAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpDelete("code/{code}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> DeleteSupplierCode(string code, CancellationToken cancellationToken)
        {
            await service.DeleteByCodeAsync(code, cancellationToken);
            return NoContent();
        }
    }
}
