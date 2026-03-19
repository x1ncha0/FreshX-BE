using FreshX.Application.Constants;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.DepartmenTypeDtos;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentTypeController(IFixDepartmentTypeService service) : ControllerBase
    {
        [HttpPost("Create-DepartType")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<DepartmentTypeDto>> CreateNewDepartmentType([FromBody] DepartmentTypeCreateUpdateDto request, CancellationToken cancellationToken)
        {
            var data = await service.CreateAsync(request, cancellationToken);
            return Ok(data);
        }

        [HttpGet("Get-DepartmentTypes")]
        public async Task<ActionResult<IReadOnlyList<DepartmentTypeDto>>> GetAllDepartmentType([FromQuery] Parameters parameters, CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(parameters, cancellationToken));
        }

        [HttpGet("Get-DepartmentDetail/{id:int}")]
        public async Task<ActionResult<DepartmentTypeDto>> GetDepartmentTypeById(int id, CancellationToken cancellationToken)
        {
            var data = await service.GetByIdAsync(id, cancellationToken);
            return data is null ? NotFound() : Ok(data);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<DepartmentTypeDto>> DeleteDepartmentTypeById(int id, CancellationToken cancellationToken)
        {
            return Ok(await service.DeleteAsync(id, cancellationToken));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<DepartmentTypeDto>> UpdateDepartmentTypeById(int id, [FromBody] DepartmentTypeCreateUpdateDto request, CancellationToken cancellationToken)
        {
            return Ok(await service.UpdateAsync(id, request, cancellationToken));
        }
    }
}
