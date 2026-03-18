using FreshX.Application.Constants;
using FreshX.Application.Dtos.Country;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController(ICountryService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CountryDto>>> GetAllCountries(
            [FromQuery] string? searchKeyword,
            [FromQuery] DateTime? createdDate,
            [FromQuery] DateTime? updatedDate,
            [FromQuery] bool? isSuspended,
            [FromQuery] int? isDeleted,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, isDeleted, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CountryDto>> GetCountryById(int id, CancellationToken cancellationToken)
        {
            var result = await service.GetByIdAsync(id, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<CountryDto>> CreateCountry([FromBody] CountryCreateUpdateDto countryDto, CancellationToken cancellationToken)
        {
            var result = await service.CreateAsync(countryDto, cancellationToken);
            return CreatedAtAction(nameof(GetCountryById), new { id = result.CountryId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryCreateUpdateDto countryDto, CancellationToken cancellationToken)
        {
            await service.UpdateAsync(id, countryDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> DeleteCountry(int id, CancellationToken cancellationToken)
        {
            await service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
