using AutoMapper;
using FreshX.Application.Dtos;
using FreshX.Application.Interfaces;

namespace FreshX.Application.Services;

public class AddressService(IAddressRepository repository, IMapper mapper) : IAddressService
{
    public async Task<List<ProvinceDto>> GetAllProvincesAsync() =>
        mapper.Map<List<ProvinceDto>>(await repository.GetAllProvincesAsync());

    public async Task<ProvinceDto> GetProvinceByCodeAsync(string code) =>
        mapper.Map<ProvinceDto>(await repository.GetProvinceByCodeAsync(code));

    public async Task<List<DistrictDto>> GetDistrictsByProvinceCodeAsync(string provinceCode) =>
        mapper.Map<List<DistrictDto>>(await repository.GetDistrictsByProvinceCodeAsync(provinceCode));

    public async Task<DistrictDto> GetDistrictByCodeAsync(string code) =>
        mapper.Map<DistrictDto>(await repository.GetDistrictByCodeAsync(code));

    public async Task<List<WardDto>> GetWardsByDistrictCodeAsync(string districtCode) =>
        mapper.Map<List<WardDto>>(await repository.GetWardsByDistrictCodeAsync(districtCode));

    public async Task<WardDto> GetWardByCodeAsync(string code) =>
        mapper.Map<WardDto>(await repository.GetWardByCodeAsync(code));
}