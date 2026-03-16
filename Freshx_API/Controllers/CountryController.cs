using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Country;
using Freshx_API.Services;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly CountryService _service;
        private readonly ILogger<CountryController> _logger;

        public CountryController(CountryService service, ILogger<CountryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CountryDto>>>> GetAllCountries(string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            int? isDeleted)
        {
            try
            {
                var result = await _service.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, isDeleted);
                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<CountryDto>>(Request.Path, "Chưa có dữ liệu nào.", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy dữ liệu thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp các quốc gia");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<CountryDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // GET: api/Country/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CountryDto>>> GetCountryById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<CountryDto>(Request.Path, "Quốc gia không tồn tại.", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy thông tin quốc gia thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp quốc gia bởi ID");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<CountryDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // POST: api/Country
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CountryDto>>> CreateCountry([FromBody] CountryCreateUpdateDto countryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _service.CreateAsync(countryDto);
                return StatusCode(StatusCodes.Status201Created,
                    ResponseFactory.Success(Request.Path, result, "Tạo mới quốc gia thành công.", StatusCodes.Status201Created));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tạo quốc gia");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<CountryDto>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại", StatusCodes.Status500InternalServerError));
            }
        }

        // PUT: api/Country/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateCountry(int id, [FromBody] CountryCreateUpdateDto countryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _service.UpdateAsync(id, countryDto);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Cập nhật thành công!", "Cập nhật thành công!", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi cập nhật quốc gia");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại.", StatusCodes.Status500InternalServerError));
            }
        }

        // DELETE: api/Country/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteCountry(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Xóa thành công!", "Xóa thành công!", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi xóa quốc gia");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra hoặc dữ liệu không tồn tại", StatusCodes.Status500InternalServerError));
            }
        }
    }
}
