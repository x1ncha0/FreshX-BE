using FreshX.Application.Dtos;
using FreshX.Application.Interfaces.IReception;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceptionController(IReceptionService receptionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await receptionService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await receptionService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReceptionDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await receptionService.AddAsync(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CreateReceptionDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await receptionService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await receptionService.DeleteAsync(id);
        return NoContent();
    }
}
