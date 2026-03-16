using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Freshx_API.Models;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Services;
using Freshx_API.Services;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Http;
using Sprache;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabResultController : ControllerBase
    {
        private readonly ILabResultService _service;
        private readonly ILogger<LabResultController> _logger;

        public LabResultController(ILabResultService service, ILogger<LabResultController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<LabResultDto>>>> GetAll([FromQuery] string? searchKey)
        {
            try
            {
                var result = await _service.GetAllAsync(searchKey);

                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<LabResultDto>>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Dữ liệu lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi tìm nạp danh sách kết quả xét nghiệm.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<LabResultDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<LabResultDto>>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<LabResultDto>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Dữ liệu lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi tìm nạp kết quả xét nghiệm với ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<LabResultDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateLabResultDto>>> Create([FromBody] CreateLabResultDto labResultDto)
        {
            try
            {
                await _service.AddAsync(labResultDto);
                return StatusCode(StatusCodes.Status201Created,
                    ResponseFactory.Success(Request.Path, labResultDto, "Thêm mới thành công.", StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi tạo mới kết quả xét nghiệm.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<CreateLabResultDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<UpdateLabResultDto>>> Update([FromBody] UpdateLabResultDto labResultDto)
        {
            try
            {
                await _service.UpdateAsync(labResultDto);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, labResultDto, "Cập nhật thành công.", StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi cập nhật kết quả xét nghiệm.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<UpdateLabResultDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, true, "Xóa thành công.", StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi xóa kết quả xét nghiệm với ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<object>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }
    }

}
