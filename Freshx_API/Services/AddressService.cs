using AutoMapper;
using Freshx_API.Dtos;
using Freshx_API.Interfaces;

namespace Freshx_API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProvinceDto>> GetAllProvincesAsync()
        {
            var provinces = await _repository.GetAllProvincesAsync();
            return _mapper.Map<List<ProvinceDto>>(provinces);
        }

        public async Task<ProvinceDto> GetProvinceByCodeAsync(string code)
        {
            var province = await _repository.GetProvinceByCodeAsync(code);
            return _mapper.Map<ProvinceDto>(province);
        }

        public async Task<List<DistrictDto>> GetDistrictsByProvinceCodeAsync(string provinceCode)
        {
            var districts = await _repository.GetDistrictsByProvinceCodeAsync(provinceCode);
            return _mapper.Map<List<DistrictDto>>(districts);
        }

        public async Task<DistrictDto> GetDistrictByCodeAsync(string code)
        {
            var district = await _repository.GetDistrictByCodeAsync(code);
            return _mapper.Map<DistrictDto>(district);
        }

        public async Task<List<WardDto>> GetWardsByDistrictCodeAsync(string districtCode)
        {
            var wards = await _repository.GetWardsByDistrictCodeAsync(districtCode);
            return _mapper.Map<List<WardDto>>(wards);
        }

        public async Task<WardDto> GetWardByCodeAsync(string code)
        {
            var ward = await _repository.GetWardByCodeAsync(code);
            return _mapper.Map<WardDto>(ward);
        }
    }

}
