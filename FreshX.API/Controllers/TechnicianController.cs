using FreshX.Application.Constants;
using FreshX.Application.Dtos;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Technician;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TechnicianController(ITechnicianService service) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<TechnicianDto>> Create([FromForm] TechnicianRequest request, CancellationToken cancellationToken)
        {
            var technician = await service.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = technician.TechnicianId }, technician);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TechnicianDto>>> Get([FromQuery] Parameters parameters, CancellationToken cancellationToken)
        {
            return Ok(await service.GetAsync(parameters, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TechnicianDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var technician = await service.GetByIdAsync(id, cancellationToken);
            return technician is null ? NotFound() : Ok(technician);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<TechnicianDto>> Update(int id, [FromForm] TechnicianRequest request, CancellationToken cancellationToken)
        {
            var technician = await service.UpdateAsync(id, request, cancellationToken);
            return technician is null ? NotFound() : Ok(technician);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<TechnicianDto>> Delete(int id, CancellationToken cancellationToken)
        {
            var technician = await service.DeleteAsync(id, cancellationToken);
            return technician is null ? NotFound() : Ok(technician);
        }
    }
}
