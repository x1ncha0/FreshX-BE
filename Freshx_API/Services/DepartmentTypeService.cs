using AutoMapper;
using Freshx_API.Dtos.DepartmenTypeDtos;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class DepartmentTypeService
    {
        private readonly IDepartmentTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;

        public DepartmentTypeService(IDepartmentTypeRepository repository, IMapper mapper, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }

        // Lấy danh sách phòng ban
        public async Task<IEnumerable<DepartmentTypeDto>> GetAllAsync(string? searchKeyword,
        DateTime? CreatetDate,
      DateTime? UpdatedDate,
      int? status)
        {
            var entities = await _repository.GetAllAsync(searchKeyword, CreatetDate, UpdatedDate, status);

            // AutoMapper tự động chuyển đổi từ Model sang DTO
            return _mapper.Map<IEnumerable<DepartmentTypeDto>>(entities);
        }

        // Lấy danh sách phòng ban
        public async Task<IEnumerable<DepartmentTypeDetailDto>> GetAllDetailAsync(string? searchKeyword,
       DateTime? CreatetDate,
    DateTime? UpdatedDate,
      int? status)
        {
            var entities = await _repository.GetAllAsync(searchKeyword, CreatetDate, UpdatedDate, status);

            // AutoMapper tự động chuyển đổi từ Model sang DTO
            return _mapper.Map<IEnumerable<DepartmentTypeDetailDto>>(entities);
        }
        // Lấy phòng ban theo ID
        public async Task<DepartmentTypeDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return null;

            return _mapper.Map<DepartmentTypeDto>(entity);
        }

        // Tạo mới phòng ban
        public async Task<DepartmentTypeDto> CreateAsync(DepartmentTypeCreateUpdateDto dto)
        {
            var entity = _mapper.Map<DepartmentType>(dto);

            // Thiết lập các giá trị mặc định
            entity.IsDeleted = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _tokenRepository.GetUserIdFromToken();

            var createdEntity = await _repository.CreateAsync(entity);

            return _mapper.Map<DepartmentTypeDto>(createdEntity);
        }

        // Cập nhật phòng ban
        public async Task UpdateAsync(int id, DepartmentTypeCreateUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Phòng ban không tồn tại.");

            // AutoMapper cập nhật dữ liệu từ DTO vào Entity
            _mapper.Map(dto, existingEntity);

            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            await _repository.UpdateAsync(existingEntity);
        }

        // Xóa mềm phòng ban
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("Phòng ban không tồn tại.");

            await _repository.DeleteAsync(id);
        }
    }
}
