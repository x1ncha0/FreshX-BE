using FreshX.Application.Dtos;
using FreshX.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LabResultController(ILabResultService labResultService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? searchKey, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await labResultService.GetAllAsync(searchKey));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await labResultService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLabResultDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await labResultService.AddAsync(dto);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLabResultDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await labResultService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await labResultService.DeleteAsync(id);
        return NoContent();
    }
}
