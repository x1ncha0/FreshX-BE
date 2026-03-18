using FreshX.Application.Constants;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Employee;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController(IEmployeeService service) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<EmployeeDto>> Create([FromForm] EmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = await service.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = employee.EmployeeId }, employee);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> Get([FromQuery] Parameters parameters, CancellationToken cancellationToken)
        {
            return Ok(await service.GetAsync(parameters, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var employee = await service.GetByIdAsync(id, cancellationToken);
            return employee is null ? NotFound() : Ok(employee);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<EmployeeDto>> Update(int id, [FromForm] EmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = await service.UpdateAsync(id, request, cancellationToken);
            return employee is null ? NotFound() : Ok(employee);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<EmployeeDto>> Delete(int id, CancellationToken cancellationToken)
        {
            var employee = await service.DeleteAsync(id, cancellationToken);
            return employee is null ? NotFound() : Ok(employee);
        }
    }
}
