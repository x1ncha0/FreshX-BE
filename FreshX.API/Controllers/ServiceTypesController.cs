using FreshX.Application.Interfaces.ServiceType;
using FreshX.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceTypesController(IServiceTypeService serviceTypeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? searchKey, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await serviceTypeService.GetAllAsync(searchKey));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await serviceTypeService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServiceTypes serviceType, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await serviceTypeService.AddAsync(serviceType));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ServiceTypes serviceType, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await serviceTypeService.UpdateAsync(id, serviceType);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await serviceTypeService.DeleteAsync(id);
        return NoContent();
    }
}
