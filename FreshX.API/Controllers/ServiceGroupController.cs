using FreshX.Application.Constants;
using FreshX.Application.Dtos.ServiceGroup;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceGroupController(IServiceGroupService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ServiceGroupDto>>> GetAll(
            [FromQuery] string? searchKeyword,
            [FromQuery] DateTime? createdDate,
            [FromQuery] DateTime? updatedDate,
            [FromQuery] int? status,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, createdDate, updatedDate, status, cancellationToken));
        }

        [HttpGet("detail")]
        public async Task<ActionResult<IReadOnlyList<ServiceGroupDetailDto>>> GetAllDetail(
            [FromQuery] string? searchKeyword,
            [FromQuery] DateTime? createdDate,
            [FromQuery] DateTime? updatedDate,
            [FromQuery] int? status,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetDetailAllAsync(searchKeyword, createdDate, updatedDate, status, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceGroupDetailDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var serviceGroup = await service.GetByIdAsync(id, cancellationToken);
            return serviceGroup is null ? NotFound() : Ok(serviceGroup);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<ServiceGroupDto>> Create([FromBody] ServiceGroupCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            var serviceGroup = await service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = serviceGroup.ServiceGroupId }, serviceGroup);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<ServiceGroupDto>> Update(int id, [FromBody] ServiceGroupCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            var serviceGroup = await service.UpdateAsync(id, dto, cancellationToken);
            return serviceGroup is null ? NotFound() : Ok(serviceGroup);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<ServiceGroupDto>> Delete(int id, CancellationToken cancellationToken)
        {
            var serviceGroup = await service.DeleteAsync(id, cancellationToken);
            return serviceGroup is null ? NotFound() : Ok(serviceGroup);
        }
    }
}
