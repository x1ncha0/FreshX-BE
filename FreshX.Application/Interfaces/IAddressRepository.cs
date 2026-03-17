using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Province>> GetAllProvincesAsync();
        Task<Province> GetProvinceByCodeAsync(string code);
        Task<List<District>> GetDistrictsByProvinceCodeAsync(string provinceCode);
        Task<District> GetDistrictByCodeAsync(string code);
        Task<List<Ward>> GetWardsByDistrictCodeAsync(string districtCode);
        Task<Ward> GetWardByCodeAsync(string code);
    }

}

