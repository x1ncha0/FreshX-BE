using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Interfaces.ServiceType;
using Freshx_API.Models;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypeService _service;
        private readonly ILogger<ServiceTypesController> _logger;

        public ServiceTypesController(IServiceTypeService service, ILogger<ServiceTypesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ServiceTypes>>>> GetAll([FromQuery] string? searchKey)
        {
            try
            {
                var result = await _service.GetAllAsync(searchKey);
                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<ServiceTypes>>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result.ToList(), "Dữ liệu lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi tìm nạp danh sách ServiceTypes.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<ServiceTypes>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ServiceTypes>>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<ServiceTypes>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Dữ liệu lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi tìm nạp ServiceType.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<ServiceTypes>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ServiceTypes>>> Create([FromBody] ServiceTypes serviceType)
        {
            try
            {
                var result = await _service.AddAsync(serviceType);
                return StatusCode(StatusCodes.Status201Created,
                    ResponseFactory.Success(Request.Path, result, "Thêm mới thành công.", StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi thêm ServiceType.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<ServiceTypes>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<ServiceTypes>>> Update([FromBody] ServiceTypes serviceType)
        {
            try
            {
                await _service.UpdateAsync(serviceType);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, serviceType, "Cập nhật thành công.", StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Một lỗi đã xảy ra khi cập nhật ServiceType.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<ServiceTypes>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpDelete("{id}")]
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
                _logger.LogError(ex, "Một lỗi đã xảy ra khi xóa ServiceType.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<object>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }
    }

}
