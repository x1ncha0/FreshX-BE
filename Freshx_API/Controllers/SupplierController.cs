using Azure.Core;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Supplier;
using Freshx_API.Services.CommonServices;
using Freshx_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _service;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(SupplierService service, ILogger<SupplierController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Supplier
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<SupplierDetailDto>>>> GetAllSuppliers(string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            bool? isForeign,
            bool? isStateOwned,
            int? isDeleted)
        {
            try
            {
                var result = await _service.GetAllAsync(searchKeyword,
            createdDate,
            updatedDate,
            isSuspended,
            isForeign,
            isStateOwned,
            isDeleted);

                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<SupplierDetailDto>>(Request.Path, "Chưa có dữ liệu nào.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy dữ liệu thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp các nhà cung cấp");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<SupplierDetailDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // GET: api/Supplier/id/{id}
        [HttpGet("id/{id}")]
        public async Task<ActionResult<ApiResponse<SupplierDetailDto>>> GetSupplierById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<SupplierDetailDto>(Request.Path, "Nhà cung cấp không tồn tại.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy thông tin nhà cung cấp thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp nhà cung cấp bởi ID");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<SupplierDetailDto>(Request.Path, "Một lỗi đã xảy ra,", StatusCodes.Status500InternalServerError));
            }
        }

        // GET: api/Supplier/code/{code}
        [HttpGet("code/{code}")]
        public async Task<ActionResult<ApiResponse<SupplierDetailDto>>> GetSupplierByCodeAsync(string code)
        {
            try
            {
                var result = await _service.GetSupplierByCodeAsync(code);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<SupplierDetailDto>(Request.Path, "Nhà cung cấp không tồn tại.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy thông tin nhà cung cấp thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp nhà cung cấp bởi ID");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<SupplierDetailDto>(Request.Path, "Một lỗi đã xảy ra,", StatusCodes.Status500InternalServerError));
            }
        }

        // POST: api/Supplier
        [HttpPost]
        public async Task<ActionResult<ApiResponse<SupplierDetailDto>>> CreateSupplier([FromBody] SupplierCreateDto SupplierDetailDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateAsync(SupplierDetailDto);

                return StatusCode(StatusCodes.Status201Created,
                    ResponseFactory.Success(Request.Path, result, "Tạo mới nhà cung cấp thành công.", StatusCodes.Status201Created));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tạo nhà cung cấp");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<SupplierDetailDto>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại", StatusCodes.Status500InternalServerError));
            }
        }

        // PUT: api/Supplier/code/{code}
        [HttpPut("code/{code}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateSupplier(string code, [FromBody] SupplierUpdateDto SupplierDetailDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.UpdateAsyncByCode(code, SupplierDetailDto);

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Cập nhật thành công!", "Cập nhật thành công!", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi cập nhật nhà cung cấp");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại.", StatusCodes.Status500InternalServerError));
            }
        }

        // PUT: api/Supplier/{id}
        [HttpPut("id/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateSupplier(int id, [FromBody] SupplierUpdateDto SupplierDetailDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.UpdateAsyncbyID(id, SupplierDetailDto);

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Cập nhật thành công!", "Cập nhật thành công!", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi cập nhật nhà cung cấp");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại.", StatusCodes.Status500InternalServerError));
            }
        }

        // DELETE: api/Supplier/{id}
        [HttpDelete("id/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSupplier(int id)
        {
            try
            {
                await _service.DeleteAsyncId(id);

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Xóa thành công!", "Xóa thành công!", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi xóa nhà cung cấp");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại", StatusCodes.Status500InternalServerError));
            }
        }

        // DELETE: api/Supplier/code/{code}
        [HttpDelete("code/{code}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSupplierCode(string code)
        {
            try
            {
                await _service.DeleteAsyncCode(code);

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Xóa thành công!", "Xóa thành công!", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi xóa nhà cung cấp");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại", StatusCodes.Status500InternalServerError));
            }
        }
    }
}
