using Azure.Core;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.ExamineDtos;
using Freshx_API.Interfaces;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamineController : ControllerBase
    {
        private readonly IExamineService _service;

        public ExamineController(IExamineService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ExamineResponseDto>>> CreateExamine([FromBody] CreateExamDto dto)
        {
            try
            {
                var examine = await _service.AddAsync(dto);
                return Ok(ResponseFactory.Success(Request.Path, examine));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<object>(Request.Path, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ExamineResponseDto>>> GetExamine(int id)
        {
            var examine = await _service.GetByIdAsync(id);
            return examine == null
                ? NotFound(ResponseFactory.Error<object>(Request.Path, "Examine not found"))
                : Ok(ResponseFactory.Success(Request.Path, examine));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ExamineResponseDto>>>> GetAllExamine()
        {
            var examines = await _service.GetAllAsync();
            return Ok(ResponseFactory.Success(Request.Path, examines));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateExamine(int id, [FromBody] ExamineRequestDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return Ok(ResponseFactory.Success(Request.Path, "Updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<object>(Request.Path, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteExamine(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(ResponseFactory.Success(Request.Path, "Deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<object>(Request.Path, ex.Message));
            }
        }
    }

}
