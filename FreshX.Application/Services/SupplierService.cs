using AutoMapper;
using FreshX.Application.Dtos.Supplier;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class SupplierService(
        ISupplierRepository repository,
        IMapper mapper,
        ITokenRepository tokenRepository) : ISupplierService
    {
        public async Task<IReadOnlyList<SupplierDetailDto>> GetAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, bool? isForeign, bool? isStateOwned, int? isDeleted, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, isForeign, isStateOwned, isDeleted);
            return mapper.Map<IReadOnlyList<SupplierDetailDto>>(entities);
        }

        public async Task<SupplierDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<SupplierDetailDto>(entity);
        }

        public async Task<SupplierDetailDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetSupplierByCodeAsync(code);
            return entity is null ? null : mapper.Map<SupplierDetailDto>(entity);
        }

        public async Task<SupplierDetailDto> CreateAsync(SupplierCreateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = mapper.Map<Supplier>(dto);
            entity.Code = GenerateUniqueCode();
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = tokenRepository.GetUserIdFromToken();
            entity.IsDeleted = false;

            var createdEntity = await repository.CreateAsync(entity);
            return mapper.Map<SupplierDetailDto>(createdEntity);
        }

        public async Task UpdateByCodeAsync(string code, SupplierUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetSupplierByCodeAsync(code)
                ?? throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            mapper.Map(dto, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;
            existingEntity.UpdatedBy = tokenRepository.GetUserIdFromToken();
            await repository.UpdateAsync(existingEntity);
        }

        public async Task UpdateByIdAsync(int id, SupplierUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            mapper.Map(dto, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;
            existingEntity.UpdatedBy = tokenRepository.GetUserIdFromToken();
            await repository.UpdateAsync(existingEntity);
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            await repository.DeleteAsyncId(existingEntity.Id);
        }

        public async Task DeleteByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetSupplierByCodeAsync(code)
                ?? throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            await repository.DeleteAsyncCode(existingEntity.Code!);
        }

        private static string GenerateUniqueCode() => Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
    }
}
