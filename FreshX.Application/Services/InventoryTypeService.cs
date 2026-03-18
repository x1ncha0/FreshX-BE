using AutoMapper;
using FreshX.Application.Dtos.InventoryType;
using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class InventoryTypeService(IInventoryTypeRepository repository, IMapper mapper) : IInventoryTypeService
    {
        public async Task<IReadOnlyList<InventoryTypeDto>> GetAllAsync(string? searchKeyword = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword);
            return mapper.Map<IReadOnlyList<InventoryTypeDto>>(entities);
        }

        public async Task<InventoryTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<InventoryTypeDto>(entity);
        }

        public async Task<InventoryTypeDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByCodeAsync(code);
            return entity is null ? null : mapper.Map<InventoryTypeDto>(entity);
        }

        public async Task<InventoryTypeDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetNameAsync(name);
            return entity is null ? null : mapper.Map<InventoryTypeDto>(entity);
        }

        public async Task<InventoryTypeDto> CreateAsync(InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureUniqueNameAsync(dto.Name, null);

            var entity = mapper.Map<InventoryType>(dto);
            entity.Code = GenerateUniqueCode();
            var createdEntity = await repository.CreateAsync(entity);
            return mapper.Map<InventoryTypeDto>(createdEntity);
        }

        public async Task UpdateByIdAsync(int id, InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Inventory Type không tồn tại.");

            await EnsureUniqueNameAsync(dto.Name, existingEntity.Id);
            mapper.Map(dto, existingEntity);
            await repository.UpdateAsync(existingEntity);
        }

        public async Task UpdateByCodeAsync(string code, InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByCodeAsync(code)
                ?? throw new KeyNotFoundException("Inventory Type không tồn tại.");

            await EnsureUniqueNameAsync(dto.Name, existingEntity.Id);
            mapper.Map(dto, existingEntity);
            await repository.UpdateAsync(existingEntity);
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Inventory Type không tồn tại.");

            await repository.DeleteAsync(existingEntity.Id);
        }

        public async Task DeleteByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByCodeAsync(code)
                ?? throw new KeyNotFoundException("Inventory Type không tồn tại.");

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
                throw new InvalidOperationException($"Inventory Type với tên '{name}' đã tồn tại.");
            }
        }

        private static string GenerateUniqueCode() => Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
    }
}
