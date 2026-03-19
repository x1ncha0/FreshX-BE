using FreshX.Application.Constants;
using FreshX.Application.Dtos.ServiceCatalog;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceCatalogController(IServiceCatalogService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ServiceCatalogDto>>> GetAll(
            [FromQuery] string? searchKeyword,
            [FromQuery] DateTime? createdDate,
            [FromQuery] DateTime? updatedDate,
            [FromQuery] int? status,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetAllAsync(searchKeyword, createdDate, updatedDate, status, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceCatalogDetailDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var serviceCatalog = await service.GetByIdAsync(id, cancellationToken);
            return serviceCatalog is null ? NotFound() : Ok(serviceCatalog);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<ServiceCatalogDto>> Create([FromBody] ServiceCatalogCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            var serviceCatalog = await service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = serviceCatalog.ServiceCatalogId }, serviceCatalog);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<ServiceCatalogDto>> Update(int id, [FromBody] ServiceCatalogCreateUpdateDto dto, CancellationToken cancellationToken)
        {
            var serviceCatalog = await service.UpdateAsync(id, dto, cancellationToken);
            return serviceCatalog is null ? NotFound() : Ok(serviceCatalog);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<ServiceCatalogDto>> Delete(int id, CancellationToken cancellationToken)
        {
            var serviceCatalog = await service.DeleteAsync(id, cancellationToken);
            return serviceCatalog is null ? NotFound() : Ok(serviceCatalog);
        }
    }
}
