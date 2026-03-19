using FreshX.Application.Dtos.Position;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController(IPositionService positionService) : ControllerBase
    {
        [HttpGet("All-Positions")]
        public async Task<ActionResult<IReadOnlyList<PositionDto>>> GetAllPositions(CancellationToken cancellationToken)
        {
            return Ok(await positionService.GetAllAsync(cancellationToken));
        }
    }
}
