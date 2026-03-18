using FreshX.Application.Dtos.ExamineDtos;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExamineController(IExamineService examineService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await examineService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await examineService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExamDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await examineService.AddAsync(dto));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ExamineRequestDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await examineService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await examineService.DeleteAsync(id);
        return NoContent();
    }
}
