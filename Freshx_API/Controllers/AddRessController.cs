using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos;
using Freshx_API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Freshx_API.Services.CommonServices;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressService service, ILogger<AddressController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("provinces")]
        public async Task<ActionResult<ApiResponse<List<ProvinceDto>>>> GetAllProvinces()
        {
            try
            {
                var result = await _service.GetAllProvincesAsync();

                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<ProvinceDto>>(Request.Path, "Chưa có dữ liệu nào.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy dữ liệu thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra trong khi tìm nạp danh sách tỉnh/thành phố");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<ProvinceDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("province/{code}")]
        public async Task<ActionResult<ApiResponse<ProvinceDto>>> GetProvinceByCode(string code)
        {
            try
            {
                var result = await _service.GetProvinceByCodeAsync(code);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<ProvinceDto>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy dữ liệu thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra khi tìm nạp tỉnh/thành phố với mã: {code}", code);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<ProvinceDto>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("districts/{provinceCode}")]
        public async Task<ActionResult<ApiResponse<List<DistrictDto>>>> GetDistrictsByProvinceCode(string provinceCode)
        {
            try
            {
                var result = await _service.GetDistrictsByProvinceCodeAsync(provinceCode);

                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<DistrictDto>>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy dữ liệu thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra khi tìm nạp danh sách huyện/quận với mã tỉnh: {provinceCode}", provinceCode);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<DistrictDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("ward/{districtCode}")]
        public async Task<ActionResult<ApiResponse<List<WardDto>>>> GetWardsByDistrictCode(string districtCode)
        {
            try
            {
                var result = await _service.GetWardsByDistrictCodeAsync(districtCode);

                if (result == null || !result.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<List<WardDto>>(Request.Path, "Không tìm thấy dữ liệu.", StatusCodes.Status404NotFound));
                }

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, result, "Lấy dữ liệu thành công.", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra khi tìm nạp danh sách xã/phường với mã huyện: {districtCode}", districtCode);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<WardDto>>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));
            }
        }
    }


}
