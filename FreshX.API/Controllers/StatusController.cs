using FreshX.Application.Dtos.Status;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController(IStatusService statusService) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IReadOnlyList<StatusOptionDto>> GetStatuses()
        {
            return Ok(statusService.GetAll());
        }
    }
}
