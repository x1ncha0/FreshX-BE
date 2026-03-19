using AutoMapper;
using FreshX.Application.Dtos.UnitOfMeasure;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class UnitOfMeasureService(
        IUnitOfMeasureRepository repository,
        IMapper mapper,
        ITokenRepository tokenRepository) : IUnitOfMeasureService
    {
        public async Task<IReadOnlyList<UnitOfMeasureDetailDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? isSuspended,
            int? isDeleted,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, isDeleted);
            return mapper.Map<IReadOnlyList<UnitOfMeasureDetailDto>>(entities);
        }

        public async Task<UnitOfMeasureDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<UnitOfMeasureDetailDto>(entity);
        }

        public async Task<UnitOfMeasureDetailDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByCodeAsync(code);
            return entity is null ? null : mapper.Map<UnitOfMeasureDetailDto>(entity);
        }

        public async Task<UnitOfMeasureDetailDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetNameAsync(name);
            return entity is null ? null : mapper.Map<UnitOfMeasureDetailDto>(entity);
        }

        public async Task<UnitOfMeasureDetailDto> CreateAsync(UnitOfMeasureCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureUniqueNameAsync(dto.Name, null);

            var entity = mapper.Map<UnitOfMeasure>(dto);
            entity.Code = GenerateUniqueCode();
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = tokenRepository.GetUserIdFromToken();
            entity.IsDeleted = false;

            var createdEntity = await repository.CreateAsync(entity);
            return mapper.Map<UnitOfMeasureDetailDto>(createdEntity);
        }

        public async Task UpdateByIdAsync(int id, UnitOfMeasureCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Đơn vị đo không tồn tại.");

            await EnsureUniqueNameAsync(dto.Name, existingEntity.Id);

            mapper.Map(dto, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;
            existingEntity.UpdatedBy = tokenRepository.GetUserIdFromToken();

            await repository.UpdateAsync(existingEntity);
        }

        public async Task UpdateByCodeAsync(string code, UnitOfMeasureCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByCodeAsync(code)
                ?? throw new KeyNotFoundException("Đơn vị đo không tồn tại.");

            await EnsureUniqueNameAsync(dto.Name, existingEntity.Id);

            mapper.Map(dto, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;
            existingEntity.UpdatedBy = tokenRepository.GetUserIdFromToken();

            await repository.UpdateAsync(existingEntity);
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Đơn vị đo không tồn tại.");

            await repository.DeleteAsync(existingEntity.Id);
        }

        public async Task DeleteByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByCodeAsync(code)
                ?? throw new KeyNotFoundException("Đơn vị đo không tồn tại.");

            await repository.DeleteAsyncCode(existingEntity.Code!);
        }

        private async Task EnsureUniqueNameAsync(string? name, int? currentId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var existingEntity = await repository.GetNameAsync(name);
            if (existingEntity is not null && existingEntity.Id != currentId)
            {
                throw new InvalidOperationException($"Đơn vị đo với tên '{name}' đã tồn tại.");
            }
        }

        private static string GenerateUniqueCode() => Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
    }
}
