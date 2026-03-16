using AutoMapper;
using Freshx_API.Dtos.Doctor;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Freshx_API.Dtos;

namespace Freshx_API.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;

        public DoctorService(IDoctorRepository repository, IMapper mapper, ITokenRepository tokenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }

        // Lấy danh sách bác sĩ với các tiêu chí tìm kiếm
        public async Task<IEnumerable<DoctorDto>> GetAllAsync(string? searchKeyword,
        int? isSuspended,
        DateTime? createdDate,
        DateTime? updatedDate, string? specialty, string? phone, string? email, string? gender)
        {
            // Gọi đến repository với các tham số tìm kiếm
            var entities = await _repository.GetAllAsync(searchKeyword, isSuspended, createdDate, updatedDate, specialty, phone, email, gender);

            // AutoMapper tự động chuyển đổi từ Model sang DTO
            return _mapper.Map<IEnumerable<DoctorDto>>(entities);
        }

        // Lấy danh sách bác sĩ chi tiết với các tiêu chí tìm kiếm
        public async Task<IEnumerable<DoctorDetailDto>> GetDetailAllAsync(string? searchKeyword,
        int? isSuspended,
        DateTime? createdDate,
        DateTime? updatedDate, string? specialty, string? phone, string? email, string? gender)
        {
            // Gọi đến repository với các tham số tìm kiếm
            var entities = await _repository.GetAllAsync(searchKeyword, isSuspended, createdDate, updatedDate, specialty, phone, email, gender);

            // AutoMapper tự động chuyển đổi từ Model sang DTO
            return _mapper.Map<IEnumerable<DoctorDetailDto>>(entities);
        }

        // Lấy chi tiết bác sĩ theo ID
        public async Task<DoctorDetailDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return null;

            return _mapper.Map<DoctorDetailDto>(entity);
        }

        // Tạo mới bác sĩ
        public async Task<DoctorDto> CreateAsync(DoctorCreateUpdateDto dto)
        {
            var entity = _mapper.Map<Doctor>(dto);

            // Thiết lập các giá trị mặc định
            entity.IsDeleted = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _tokenRepository.GetUserIdFromToken();

            var createdEntity = await _repository.CreateAsync(entity);

            return _mapper.Map<DoctorDto>(createdEntity);
        }

        // Cập nhật bác sĩ
        public async Task UpdateAsync(int id, DoctorCreateUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Bác sĩ không tồn tại.");

            // AutoMapper cập nhật dữ liệu từ DTO vào Entity
            _mapper.Map(dto, existingEntity);

            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            await _repository.UpdateAsync(existingEntity);
        }

        // Xóa mềm bác sĩ
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("Bác sĩ không tồn tại.");

            await _repository.DeleteAsync(id);
        }

        // Kiểm tra trạng thái tạm ngưng của bác sĩ
        public async Task<bool> IsDoctorSuspendedAsync(int id)
        {
            return await _repository.IsDoctorSuspendedAsync(id);
        }
    }
}
