using FreshX.Application.Dtos.Drugs;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrugTypesController(IDrugTypeService drugTypeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? searchKeyword,
        [FromQuery] DateTime? createtDate,
        [FromQuery] DateTime? updatedDate,
        [FromQuery] int? status,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await drugTypeService.GetDrugTypeAsync(searchKeyword, createtDate, updatedDate, status));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await drugTypeService.GetDrugTypeByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] DrugTypeCreateDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var created = await drugTypeService.CreateDrugTypeAsync(dto);
        return Ok(created);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] DrugTypeUpdateDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var updated = await drugTypeService.UpdateDrugTypeAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}/soft")]
    [Authorize]
    public async Task<IActionResult> SoftDelete(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await drugTypeService.SoftDeleteDrugTypeAsync(id));
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await drugTypeService.DeleteDrugTypeAsync(id));
    }
}
