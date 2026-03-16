using AutoMapper;
using Freshx_API.Dtos.Country;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class CountryService
    {
        private readonly ICountryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;

        public CountryService(ICountryRepository repository, IMapper mapper, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }

        // Lấy danh sách quốc gia
        public async Task<IEnumerable<CountryDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            int? isDeleted)
        {
            var entities = await _repository.GetAllAsync(
                searchKeyword, createdDate, updatedDate, isSuspended, isDeleted);
            return _mapper.Map<IEnumerable<CountryDto>>(entities);
        }

        // Lấy danh sách chi tiết quốc gia
        public async Task<IEnumerable<CountryDto>> GetAllDetailAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            int? isDeleted)
        {
            var entities = await _repository.GetAllAsync(
                searchKeyword, createdDate, updatedDate, isSuspended, isDeleted);
            return _mapper.Map<IEnumerable<CountryDto>>(entities);
        }

        // Lấy thông tin quốc gia theo ID
        public async Task<CountryDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;
            return _mapper.Map<CountryDto>(entity);
        }

        // Tạo mới quốc gia
        public async Task<CountryDto> CreateAsync(CountryCreateUpdateDto dto)
        {
            var entity = _mapper.Map<Country>(dto);
            entity.IsDeleted = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _tokenRepository.GetUserIdFromToken();
            var createdEntity = await _repository.CreateAsync(entity);
            return _mapper.Map<CountryDto>(createdEntity);
        }

        // Cập nhật quốc gia
        public async Task UpdateAsync(int id, CountryCreateUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new KeyNotFoundException("Quốc gia không tồn tại.");
            _mapper.Map(dto, existingEntity);
            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();
            await _repository.UpdateAsync(existingEntity);
        }

        // Xóa mềm quốc gia
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Quốc gia không tồn tại.");
            await _repository.DeleteAsync(id);
        }
    }
}
