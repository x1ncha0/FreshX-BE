using Azure.Core;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.UnitOfMeasure;
using Freshx_API.Services.CommonServices;
using Freshx_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly UnitOfMeasureService _service;
        private readonly ILogger<UnitOfMeasureController> _logger;

        public UnitOfMeasureController(UnitOfMeasureService service, ILogger<UnitOfMeasureController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/UnitOfMeasure
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UnitOfMeasureDetailDto>>>> GetAllUnitOfMeasures(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? isSuspended,
            int? isDeleted)
        {
            try
            {
                var result = await _service.GetAllAsync(
                    searchKeyword,
                    createdDate,
                    updatedDate,
                    isSuspended,
                    isDeleted);

                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<UnitOfMeasureDetailDto>>(Request.Path, "Chưa có dữ liệu nào.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy dữ liệu thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp các đơn vị đo");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<UnitOfMeasureDetailDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // GET: api/UnitOfMeasure/{id}
        [HttpGet("id/{id}")]
        public async Task<ActionResult<ApiResponse<UnitOfMeasureDetailDto>>> GetUnitOfMeasureById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Đơn vị đo không tồn tại.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy thông tin đơn vị đo thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp đơn vị đo theo ID");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("code/{code}")]
        public async Task<ActionResult<ApiResponse<UnitOfMeasureDetailDto>>> GetByCodeAsync(string code)
        {
            try
            {
                var result = await _service.GetByCodeAsync(code);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Đơn vị đo không tồn tại.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy thông tin đơn vị đo thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp đơn vị đo theo ID");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<UnitOfMeasureDetailDto>>> GetNameAsync(string name)
        {
            try
            {
                var result = await _service.GetNameAsync(name);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Đơn vị đo không tồn tại.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy thông tin đơn vị đo thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp đơn vị đo theo ID");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // POST: api/UnitOfMeasure
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UnitOfMeasureCreateUpdateDto unitOfMeasureDto)
        {
            try
            {
                // Kiểm tra tính hợp lệ của Model trước khi tiếp tục
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Gọi service để tạo mới đơn vị đo
                var result = await _service.CreateAsync(unitOfMeasureDto);

                // Trả về mã thành công 201 Created với dữ liệu phản hồi thành công
                return StatusCode(StatusCodes.Status201Created,
                    ResponseFactory.Success(Request.Path, result, "Tạo mới đơn vị đo thành công.", StatusCodes.Status201Created));
            }
            catch (InvalidOperationException ex) // Lỗi khi tên trùng
            {
                // Ghi log và trả về lỗi 400 nếu tên đã tồn tại
                _logger.LogError(ex, "Lỗi tên đơn vị đo lường đã tồn tại.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // Xử lý các lỗi khác (lỗi hệ thống)
            {
                // Ghi log và trả về lỗi 500 cho các lỗi hệ thống không xác định
                _logger.LogError(ex, "Một ngoại lệ đã xảy ra trong khi tạo đơn vị đo.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại.", StatusCodes.Status500InternalServerError));
            }
        }


        // PUT: api/UnitOfMeasure/id/{id}
        [HttpPut("id/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateUnitOfMeasure(int id, [FromBody] UnitOfMeasureCreateUpdateDto unitOfMeasureDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.UpdateAsyncId(id, unitOfMeasureDto);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Cập nhật thành công!", "Cập nhật thành công!", StatusCodes.Status200OK));
            }
            catch (InvalidOperationException ex) // Lỗi khi tên trùng
            {
                // Ghi log và trả về lỗi 400 nếu tên đã tồn tại
                _logger.LogError(ex, "Lỗi tên đơn vị đo lường đã tồn tại.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // Xử lý các lỗi khác (lỗi hệ thống)
            {
                // Ghi log và trả về lỗi 500 cho các lỗi hệ thống không xác định
                _logger.LogError(ex, "Một ngoại lệ đã xảy ra trong khi tạo đơn vị đo.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại.", StatusCodes.Status500InternalServerError));
            }
        }

        // PUT: api/UnitOfMeasure/code/{code}
        [HttpPut("code/{code}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateUnitOfMeasure(string code, [FromBody] UnitOfMeasureCreateUpdateDto unitOfMeasureDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.UpdateAsyncCode(code, unitOfMeasureDto);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Cập nhật thành công!", "Cập nhật thành công!", StatusCodes.Status200OK));
            }
            catch (InvalidOperationException ex) // Lỗi khi tên trùng
            {
                // Ghi log và trả về lỗi 400 nếu tên đã tồn tại
                _logger.LogError(ex, "Lỗi tên đơn vị đo lường đã tồn tại.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // Xử lý các lỗi khác (lỗi hệ thống)
            {
                // Ghi log và trả về lỗi 500 cho các lỗi hệ thống không xác định
                _logger.LogError(ex, "Một ngoại lệ đã xảy ra trong khi tạo đơn vị đo.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<UnitOfMeasureDetailDto>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại.", StatusCodes.Status500InternalServerError));
            }
        }

        // DELETE: api/UnitOfMeasure/id/{id}
        [HttpDelete("id/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteUnitOfMeasure(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Xóa thành công!", "Xóa thành công!", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi xóa đơn vị đo");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpDelete("code/{code}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteAsyncCode(string code)
        {
            try
            {
                await _service.DeleteAsyncCode(code);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Xóa thành công!", "Xóa thành công!", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi xóa đơn vị đo");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại.", StatusCodes.Status500InternalServerError));
            }
        }
    }
}
