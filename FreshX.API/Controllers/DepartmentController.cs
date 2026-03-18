using FreshX.Application.Constants;
using FreshX.Application.Dtos.DepartmentDtos;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentController(IDepartmentService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetAll(
            [FromQuery] string? searchKeyword,
            [FromQuery] DateTime? createdDate,
            [FromQuery] DateTime? updatedDate,
            [FromQuery] int? status,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, createdDate, updatedDate, status, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentDetailDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var department = await service.GetByIdAsync(id, cancellationToken);
            return department is null ? NotFound() : Ok(department);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<DepartmentDto>> Create([FromBody] DepartmentCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            var department = await service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = department.DepartmentId }, department);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<DepartmentDto>> Update(int id, [FromBody] DepartmentCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            var department = await service.UpdateAsync(id, dto, cancellationToken);
            return department is null ? NotFound() : Ok(department);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<DepartmentDto>> Delete(int id, CancellationToken cancellationToken)
        {
            var department = await service.DeleteAsync(id, cancellationToken);
            return department is null ? NotFound() : Ok(department);
        }
    }
}
