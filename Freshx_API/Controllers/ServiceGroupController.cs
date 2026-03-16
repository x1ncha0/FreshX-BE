using Azure.Core;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.ServiceGroup;
using Freshx_API.Services.CommonServices;
using Freshx_API.Services;
using Microsoft.AspNetCore.Mvc;
using Freshx_API.Repository;
using Freshx_API.Models;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceGroupController : ControllerBase
    {
        private readonly ServiceGroupService _service;
        private readonly ILogger<ServiceGroupController> _logger;
        private readonly RepositoryCheck _check;
        public ServiceGroupController(ServiceGroupService service, ILogger<ServiceGroupController> logger, RepositoryCheck check)
        {
            _service = service;
            _logger = logger;
            _check = check;
        }

        // Lấy tất cả nhóm dịch vụ với các bộ lọc
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ServiceGroupDto>>>> GetAll(
            string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, int? status)
        {
            try
            {
                var result = await _service.GetAllAsync(searchKeyword, createdDate, updatedDate, status);

                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<ServiceGroupDto>>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Dữ liệu lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra trong khi tìm nạp các nhóm dịch vụ.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<ServiceGroupDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Lấy chi tiết nhóm dịch vụ
        [HttpGet("detail")]
        public async Task<ActionResult<ApiResponse<List<ServiceGroupDetailDto>>>> GetDetailAll(
            string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, int? status)
        {
            try
            {
                var result = await _service.GetDetailAllAsync(searchKeyword, createdDate, updatedDate, status);

                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<ServiceGroupDetailDto>>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Dữ liệu lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra trong khi tìm nạp chi tiết nhóm dịch vụ.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<ServiceGroupDetailDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }
        // Lấy nhóm dịch vụ theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ServiceGroupDto>>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<ServiceGroupDto>(Request.Path, "Nhóm dịch vụ không tìm thấy.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Chi tiết nhóm dịch vụ lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra trong khi tìm nạp nhóm dịch vụ theo ID.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<ServiceGroupDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Tạo mới nhóm dịch vụ
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ServiceGroupDto>>> Create([FromBody] ServiceGroupCreateUpdateDto dto)
        {
            try
            {
                // Kiểm tra tính duy nhất của trường 'Code' trong bảng ServiceTypes
                var isUnique = await _check.IsUniqueAsync<ServiceGroup>("Code", dto.Code);
                if (!isUnique)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        ResponseFactory.Error<ServiceTypes>(Request.Path, "Mã danh mục đã tồn tại.", StatusCodes.Status400BadRequest));
                }


                var result = await _service.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created,
                    ResponseFactory.Success(Request.Path, result, "Nhóm dịch vụ tạo ra thành công.", StatusCodes.Status201Created));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra trong khi tạo nhóm dịch vụ.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<ServiceGroupDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Cập nhật nhóm dịch vụ
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] ServiceGroupCreateUpdateDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Cập nhật nhóm dịch vụ thành công.", "Nhóm dịch vụ cập nhật thành công.", StatusCodes.Status200OK));
            }
            catch(InvalidOperationException Oex)
            {
                _logger.LogError(Oex, "Một lỗi đã xảy ra trong khi cập nhật nhóm dịch vụ.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, Oex.Message, StatusCodes.Status500InternalServerError));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra trong khi cập nhật nhóm dịch vụ.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Xóa nhóm dịch vụ
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Nhóm dịch vụ đã xóa thành công.", "Xóa nhóm dịch vụ thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra trong khi xóa nhóm dịch vụ.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }
    }
}
