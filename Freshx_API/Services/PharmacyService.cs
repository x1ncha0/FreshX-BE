using AutoMapper;
using Freshx_API.Dtos.Pharmacy;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;
using Freshx_API.Repository;

namespace Freshx_API.Services
{
    public class PharmacyService
    {
        private readonly IPharmacyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        private readonly RepositoryCheck _check;
        private readonly FreshxDBContext _context;

        public PharmacyService(IPharmacyRepository repository, IMapper mapper, ITokenRepository tokenRepository, RepositoryCheck check, FreshxDBContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
            _check = check;
            _context = context;
        }

        // Lấy danh sách nhà thuốc với các bộ lọc
        public async Task<IEnumerable<PharmacyDto>> GetAllAsync(string? searchKeyword,
            DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, int? inventoryTypeId, int? specialtyId)
        {
            var entities = await _repository.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, inventoryTypeId, specialtyId);
            return _mapper.Map<IEnumerable<PharmacyDto>>(entities);
        }

        // Lấy danh sách nhà thuốc với các bộ lọc
        public async Task<IEnumerable<PharmacyDetailDto>> GetDetailAllAsync(string? searchKeyword,
            DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, int? inventoryTypeId, int? specialtyId)
        {
            var entities = await _repository.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, inventoryTypeId, specialtyId);
            return _mapper.Map<IEnumerable<PharmacyDetailDto>>(entities);
        }

        // Lấy nhà thuốc theo ID
        public async Task<PharmacyDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null || entity.IsDeleted == 1)
                return null;

            return _mapper.Map<PharmacyDto>(entity);
        }

        // Tạo mới nhà thuốc
        public async Task<PharmacyDto> CreateAsync(PharmacyCreateDto dto)
        {
            string code = GenerateUniqueCode();
            var existingPharmacy = await _repository.GetPharmacyByCodeAsync(code);
            if (existingPharmacy != null)
            {
                throw new Exception("Mã nhà thuốc đã tồn tại.");
            }

            var inventoryType = await _repository.GetInventoryTypeByIdAsync(dto.InventoryTypeId);

            if (inventoryType == null)
            {
                throw new Exception("InventoryType không hợp lệ.");
            }

            var entity = _mapper.Map<Pharmacy>(dto);
            entity.Code = code;
            entity.IsDeleted = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _tokenRepository.GetUserIdFromToken();

            var createdEntity = await _repository.CreateAsync(entity);

            return _mapper.Map<PharmacyDto>(createdEntity);
        }

        // Cập nhật nhà thuốc
        public async Task UpdateAsync(int id, PharmacyUpdateDto dto)
        {
            // Lấy thông tin nhà thuốc cần cập nhật
            var existingEntity = await _repository.GetByIdAsync(id);

            if (existingEntity == null || existingEntity.IsDeleted == 1)
                throw new KeyNotFoundException("Nhà thuốc không tồn tại.");

            // Kiểm tra nếu mã code thay đổi
            if (existingEntity.Code != dto.Code)
            {
                // Kiểm tra xem có nhà thuốc nào có mã code giống nhau không
                var existingPharmacyWithCode = await _repository.GetPharmacyByCodeAsync(dto.Code);
                if (existingPharmacyWithCode != null && existingPharmacyWithCode.IsDeleted == 0)  // Nếu nhà thuốc trùng mã và chưa bị xóa
                {
                    throw new InvalidOperationException("Mã nhà thuốc đã tồn tại."); // Ném ra exception InvalidOperationException
                }
            }

            // Kiểm tra tính hợp lệ của InventoryType
            var inventoryType = await _repository.GetInventoryTypeByIdAsync(dto.InventoryTypeId);
            if (inventoryType == null)
            {
                throw new Exception("InventoryType không hợp lệ.");
            }

            // Cập nhật entity
            _mapper.Map(dto, existingEntity);
            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            await _repository.UpdateAsync(existingEntity);
        }


        // Xóa mềm nhà thuốc
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted == 1)
                throw new KeyNotFoundException("Nhà thuốc không tồn tại.");

            //var checkResuilt = await _context.CheckDependencies(entity, "PharmacyId");
            //if (!checkResuilt.CanDelete)
            //    throw new InvalidOperationException(checkResuilt.Message + checkResuilt.ActiveDependencies.ToString());

            entity.IsDeleted = 1;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = _tokenRepository.GetUserIdFromToken();

            await _repository.UpdateAsync(entity);
        }

        public async Task<Pharmacy> GetPharmacyByCodeAsync(string code)
        {
            return await _repository.GetPharmacyByCodeAsync(code);
        }

        // Hàm tạo mã duy nhất từ GUID
        private string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); // Mã gồm 8 ký tự
        }

    }
}
