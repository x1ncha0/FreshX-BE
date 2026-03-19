using FreshX.Application.Dtos.Prescription;
using FreshX.Application.Interfaces.IPrescription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PrescriptionController(
    IPrescriptionService prescriptionService,
    IPrescriptionDetailService prescriptionDetailService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? searchKey, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await prescriptionService.GetAllAsync(searchKey));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await prescriptionService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await prescriptionService.AddAsync(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePrescriptionDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await prescriptionService.UpdateAsync(dto));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await prescriptionService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{prescriptionId:int}/details")]
    public async Task<IActionResult> GetDetails(int prescriptionId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await prescriptionDetailService.GetByPrescriptionIdAsync(prescriptionId));
    }

    [HttpPost("details")]
    public async Task<IActionResult> CreateDetail([FromBody] CreatePrescriptionDetailDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await prescriptionDetailService.AddAsync(dto);
        return NoContent();
    }

    [HttpPut("details")]
    public async Task<IActionResult> UpdateDetail([FromBody] UpdatePrescriptionDetailDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await prescriptionDetailService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("details/{id:int}")]
    public async Task<IActionResult> DeleteDetail(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await prescriptionDetailService.DeleteAsync(id);
        return NoContent();
    }
}
