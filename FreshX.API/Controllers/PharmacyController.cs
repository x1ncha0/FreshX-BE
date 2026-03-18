using FreshX.Application.Constants;
using FreshX.Application.Dtos.Pharmacy;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PharmacyController(IPharmacyService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PharmacyDto>>> GetAll([FromQuery] string? searchKeyword, [FromQuery] DateTime? createdDate, [FromQuery] DateTime? updatedDate, [FromQuery] bool? isSuspended, [FromQuery] int? inventoryTypeId, [FromQuery] int? specialtyId, CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, inventoryTypeId, specialtyId, cancellationToken));
        }

        [HttpGet("detail")]
        public async Task<ActionResult<IReadOnlyList<PharmacyDetailDto>>> GetDetailAll([FromQuery] string? searchKeyword, [FromQuery] DateTime? createdDate, [FromQuery] DateTime? updatedDate, [FromQuery] bool? isSuspended, [FromQuery] int? inventoryTypeId, [FromQuery] int? specialtyId, CancellationToken cancellationToken)
        {
            return Ok(await service.GetDetailAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, inventoryTypeId, specialtyId, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PharmacyDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await service.GetByIdAsync(id, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<PharmacyDto>> Create([FromBody] PharmacyCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.PharmacyId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> Update(int id, [FromBody] PharmacyUpdateDto dto, CancellationToken cancellationToken)
        {
            await service.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
