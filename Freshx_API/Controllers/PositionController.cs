using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Models;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly FreshxDBContext _context;
        public PositionController(FreshxDBContext context)
        {
            _context = context;
        }
        [HttpGet("All-Positions")]
        public async Task<ActionResult<ApiResponse<List<Position?>>>> GetAllPositions()
        {
            try
            {
                var data = await _context.Positions.ToListAsync();
                return Ok(ResponseFactory.Success<List<Position>>(Request.Path, data));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi lấy danh sách vai trò", StatusCodes.Status500InternalServerError));
            }
        }

    }
}
