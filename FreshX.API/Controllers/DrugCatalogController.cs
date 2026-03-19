using FreshX.Application.Constants;
using FreshX.Application.Dtos.Country;
using FreshX.Application.Dtos.DrugCatalog;
using FreshX.Application.Dtos.Drugs;
using FreshX.Application.Dtos.Supplier;
using FreshX.Application.Dtos.UnitOfMeasure;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugCatalogController(IDrugCatalogService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DrugCatalogDetailDto>>> GetAll([FromQuery] string? searchKeyword, [FromQuery] DateTime? createdDate, [FromQuery] DateTime? updatedDate, [FromQuery] int? status, CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, createdDate, updatedDate, status, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DrugCatalogDetailDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await service.GetByIdAsync(id, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<DrugCatalogDetailDto>> Create([FromBody] DrugCatalogCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            var result = await service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.DrugCatalogId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> Update(int id, [FromBody] DrugCatalogCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            await service.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("drug-type/{drugTypeId:int}")]
        public async Task<ActionResult<DrugTypeDto>> GetDrugTypeById(int drugTypeId, CancellationToken cancellationToken)
        {
            var result = await service.GetDrugTypeByIdAsync(drugTypeId, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("manufacturer/{manufacturerId:int}")]
        public async Task<ActionResult<SupplierDetailDto>> GetManufacturerById(int manufacturerId, CancellationToken cancellationToken)
        {
            var result = await service.GetManufacturerByIdAsync(manufacturerId, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("unit-of-measure/{unitOfMeasureId:int}")]
        public async Task<ActionResult<UnitOfMeasureDetailDto>> GetUnitOfMeasureById(int unitOfMeasureId, CancellationToken cancellationToken)
        {
            var result = await service.GetUnitOfMeasureByIdAsync(unitOfMeasureId, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("country/{countryId:int}")]
        public async Task<ActionResult<CountryDto>> GetCountryById(int countryId, CancellationToken cancellationToken)
        {
            var result = await service.GetCountryByIdAsync(countryId, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
