using AutoMapper;
using FreshX.Application.Dtos.Pharmacy;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class PharmacyService(
        IPharmacyRepository repository,
        IMapper mapper,
        ITokenRepository tokenRepository) : IPharmacyService
    {
        public async Task<IReadOnlyList<PharmacyDto>> GetAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, int? inventoryTypeId, int? specialtyId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, inventoryTypeId, specialtyId);
            return mapper.Map<IReadOnlyList<PharmacyDto>>(entities);
        }

        public async Task<IReadOnlyList<PharmacyDetailDto>> GetDetailAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, int? inventoryTypeId, int? specialtyId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, inventoryTypeId, specialtyId);
            return mapper.Map<IReadOnlyList<PharmacyDetailDto>>(entities);
        }

        public async Task<PharmacyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<PharmacyDto>(entity);
        }

        public async Task<PharmacyDto> CreateAsync(PharmacyCreateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureReferencesAsync(dto.DepartmentId, dto.InventoryTypeId);

            var entity = mapper.Map<Pharmacy>(dto);
            entity.Code = GenerateUniqueCode();
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = tokenRepository.GetUserIdFromToken();
            entity.IsDeleted = false;

            var createdEntity = await repository.CreateAsync(entity);
            return mapper.Map<PharmacyDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, PharmacyUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Nhà thuốc không tồn tại.");

            if (!string.IsNullOrWhiteSpace(dto.Code) && !string.Equals(existingEntity.Code, dto.Code, StringComparison.OrdinalIgnoreCase))
            {
                var duplicateCode = await repository.GetPharmacyByCodeAsync(dto.Code);
                if (duplicateCode is not null && duplicateCode.Id != existingEntity.Id)
                {
                    throw new InvalidOperationException("Mã nhà thuốc đã tồn tại.");
                }
            }

            await EnsureReferencesAsync(dto.DepartmentId, dto.InventoryTypeId);
            mapper.Map(dto, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;
            existingEntity.UpdatedBy = tokenRepository.GetUserIdFromToken();
            await repository.UpdateAsync(existingEntity);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Nhà thuốc không tồn tại.");

            await repository.DeleteAsync(existingEntity.Id);
        }

        private async Task EnsureReferencesAsync(int? departmentId, int? inventoryTypeId)
        {
            if (departmentId.HasValue && await repository.GetDepartmentByIdAsync(departmentId) is null)
            {
                throw new KeyNotFoundException("Phòng ban không tồn tại.");
            }

            if (inventoryTypeId.HasValue && await repository.GetInventoryTypeByIdAsync(inventoryTypeId) is null)
            {
                throw new KeyNotFoundException("Loại tồn kho không tồn tại.");
            }
        }

        private static string GenerateUniqueCode() => Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
    }
}
