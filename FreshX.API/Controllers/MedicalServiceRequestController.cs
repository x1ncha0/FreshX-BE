using FreshX.Application.Dtos;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalServiceRequestController(IMedicalServiceRequestService medicalServiceRequestService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await medicalServiceRequestService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await medicalServiceRequestService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicalServiceRequestDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await medicalServiceRequestService.AddAsync(dto));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicalServiceRequestDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await medicalServiceRequestService.UpdateAsync(id, dto));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await medicalServiceRequestService.DeleteAsync(id);
        return NoContent();
    }
}
