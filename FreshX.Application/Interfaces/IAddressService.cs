using FreshX.Application.Dtos;

namespace FreshX.Application.Interfaces
{
    public interface IAddressService
    {
        Task<List<ProvinceDto>> GetAllProvincesAsync();
        Task<ProvinceDto> GetProvinceByCodeAsync(string code);
        Task<List<DistrictDto>> GetDistrictsByProvinceCodeAsync(string provinceCode);
        Task<DistrictDto> GetDistrictByCodeAsync(string code);
        Task<List<WardDto>> GetWardsByDistrictCodeAsync(string districtCode);
        Task<WardDto> GetWardByCodeAsync(string code);
    }

}

