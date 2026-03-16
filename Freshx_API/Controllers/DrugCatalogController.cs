using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Freshx_API.Dtos;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DrugCatalog;
using Freshx_API.Services.CommonServices;
using Freshx_API.Services.Drugs;
using Freshx_API.Models;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugCatalogController : ControllerBase
    {
        private readonly DrugCatalogService _service;
        private readonly ILogger<DrugCatalogController> _logger;

        public DrugCatalogController(DrugCatalogService service, ILogger<DrugCatalogController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // Lấy danh sách danh mục thuốc
        [HttpGet()]
        public async Task<ActionResult<ApiResponse<List<DrugCatalogDetailDto>>>> GetAll(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, int? status)
        {
            try
            {
                var result = await _service.GetAllAsync(searchKeyword, createdDate, updatedDate, status);
                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<DrugCatalogDetailDto>>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Dữ liệu lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi tìm nạp danh mục thuốc.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<DrugCatalogDetailDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Lấy danh mục thuốc theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DrugCatalogDetailDto>>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<DrugCatalogDetailDto>(Request.Path, "Danh mục thuốc không tìm thấy.", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Chi tiết danh mục thuốc lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi tìm nạp danh mục thuốc bởi ID.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<DrugCatalogDetailDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Tạo mới danh mục thuốc
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DrugCatalogDetailDto>>> Create([FromBody] DrugCatalogCreateUpdateDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created,
                    ResponseFactory.Success(Request.Path, result, "Danh mục thuốc tạo thành công.", StatusCodes.Status201Created));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi tạo danh mục thuốc.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<DrugCatalogDetailDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Cập nhật danh mục thuốc
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(int id, [FromBody] DrugCatalogCreateUpdateDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Cập nhật thành công.", "Danh mục thuốc đã được cập nhật.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi cập nhật danh mục thuốc.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Xóa danh mục thuốc
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, "Danh mục thuốc đã xóa thành công.", "Xóa danh mục thuốc thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi xóa danh mục thuốc.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Lấy thông tin loại thuốc theo ID
        [HttpGet("drug-type/{drugTypeId}")]
        public async Task<ActionResult<ApiResponse<DrugType>>> GetDrugTypeById(int? drugTypeId)
        {
            try
            {
                var result = await _service.GetDrugTypeByIdAsync(drugTypeId);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<DrugType>(Request.Path, "Loại thuốc không tìm thấy.", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Thông tin loại thuốc lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi tìm nạp thông tin loại thuốc.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<DrugType>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Lấy thông tin nhà sản xuất theo ID
        [HttpGet("manufacturer/{manufacturerId}")]
        public async Task<ActionResult<ApiResponse<Supplier>>> GetManufacturerById(int? manufacturerId)
        {
            try
            {
                var result = await _service.GetManufacturerByIdAsync(manufacturerId);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<Supplier>(Request.Path, "Nhà sản xuất không tìm thấy.", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Thông tin nhà sản xuất lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi tìm nạp thông tin nhà sản xuất.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<Supplier>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Lấy thông tin đơn vị đo lường theo ID
        [HttpGet("unit-of-measure/{unitOfMeasureId}")]
        public async Task<ActionResult<ApiResponse<UnitOfMeasure>>> GetUnitOfMeasureById(int? unitOfMeasureId)
        {
            try
            {
                var result = await _service.GetUnitOfMeasureByIdAsync(unitOfMeasureId);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<UnitOfMeasure>(Request.Path, "Đơn vị đo lường không tìm thấy.", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Thông tin đơn vị đo lường lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi tìm nạp thông tin đơn vị đo lường.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<UnitOfMeasure>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        // Lấy thông tin quốc gia theo ID
        [HttpGet("country/{countryId}")]
        public async Task<ActionResult<ApiResponse<Country>>> GetCountryById(int? countryId)
        {
            try
            {
                var result = await _service.GetCountryByIdAsync(countryId);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<Country>(Request.Path, "Quốc gia không tìm thấy.", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Thông tin quốc gia lấy thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một lỗi đã xảy ra khi tìm nạp thông tin quốc gia.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<Country>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }
    }
}