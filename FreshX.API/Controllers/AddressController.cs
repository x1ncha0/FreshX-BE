using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController(IAddressService addressService) : ControllerBase
{
    [HttpGet("provinces")]
    public async Task<IActionResult> GetProvinces(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await addressService.GetAllProvincesAsync());
    }

    [HttpGet("provinces/{code}")]
    public async Task<IActionResult> GetProvinceByCode(string code, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await addressService.GetProvinceByCodeAsync(code));
    }

    [HttpGet("provinces/{provinceCode}/districts")]
    public async Task<IActionResult> GetDistrictsByProvince(string provinceCode, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await addressService.GetDistrictsByProvinceCodeAsync(provinceCode));
    }

    [HttpGet("districts/{code}")]
    public async Task<IActionResult> GetDistrictByCode(string code, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await addressService.GetDistrictByCodeAsync(code));
    }

    [HttpGet("districts/{districtCode}/wards")]
    public async Task<IActionResult> GetWardsByDistrict(string districtCode, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await addressService.GetWardsByDistrictCodeAsync(districtCode));
    }

    [HttpGet("wards/{code}")]
    public async Task<IActionResult> GetWardByCode(string code, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await addressService.GetWardByCodeAsync(code));
    }
}
