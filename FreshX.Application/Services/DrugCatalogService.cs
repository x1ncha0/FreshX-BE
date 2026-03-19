using AutoMapper;
using FreshX.Application.Dtos.Country;
using FreshX.Application.Dtos.DrugCatalog;
using FreshX.Application.Dtos.Drugs;
using FreshX.Application.Dtos.Supplier;
using FreshX.Application.Dtos.UnitOfMeasure;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class DrugCatalogService(
        IDrugCatalogRepository repository,
        IMapper mapper,
        ITokenRepository tokenRepository) : IDrugCatalogService
    {
        public async Task<IReadOnlyList<DrugCatalogDetailDto>> GetAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, int? status, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, status);
            return mapper.Map<IReadOnlyList<DrugCatalogDetailDto>>(entities);
        }

        public async Task<DrugCatalogDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<DrugCatalogDetailDto>(entity);
        }

        public async Task<DrugCatalogDetailDto> CreateAsync(DrugCatalogCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await EnsureReferencesAsync(dto);

            var entity = mapper.Map<DrugCatalog>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = tokenRepository.GetUserIdFromToken();
            entity.IsSuspended = (dto.IsSuspended ?? 0) == 1;
            entity.IsDeleted = (dto.IsDeleted ?? 0) == 1;

            var createdEntity = await repository.CreateAsync(entity);
            return mapper.Map<DrugCatalogDetailDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, DrugCatalogCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Danh mục thuốc không tồn tại.");

            await EnsureReferencesAsync(dto);
            mapper.Map(dto, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;
            existingEntity.UpdatedBy = tokenRepository.GetUserIdFromToken();

            await repository.UpdateAsync(existingEntity);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Danh mục thuốc không tồn tại.");

            await repository.DeleteAsync(existingEntity.Id);
        }

        public async Task<DrugTypeDto?> GetDrugTypeByIdAsync(int? drugTypeId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetDrugTypeByIdAsync(drugTypeId);
            return entity is null ? null : mapper.Map<DrugTypeDto>(entity);
        }

        public async Task<SupplierDetailDto?> GetManufacturerByIdAsync(int? manufacturerId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetManufacturerByIdAsync(manufacturerId);
            return entity is null ? null : mapper.Map<SupplierDetailDto>(entity);
        }

        public async Task<UnitOfMeasureDetailDto?> GetUnitOfMeasureByIdAsync(int? unitOfMeasureId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetUnitOfMeasureByIdAsync(unitOfMeasureId);
            return entity is null ? null : mapper.Map<UnitOfMeasureDetailDto>(entity);
        }

        public async Task<CountryDto?> GetCountryByIdAsync(int? countryId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetCountryByIdAsync(countryId);
            return entity is null ? null : mapper.Map<CountryDto>(entity);
        }

        private async Task EnsureReferencesAsync(DrugCatalogCreateUpdateDto dto)
        {
            if (dto.DrugTypeId.HasValue && await repository.GetDrugTypeByIdAsync(dto.DrugTypeId) is null)
            {
                throw new KeyNotFoundException("Loại thuốc không tồn tại.");
            }

            if (dto.ManufacturerId.HasValue && await repository.GetManufacturerByIdAsync(dto.ManufacturerId) is null)
            {
                throw new KeyNotFoundException("Nhà sản xuất không tồn tại.");
            }

            if (dto.UnitOfMeasureId.HasValue && await repository.GetUnitOfMeasureByIdAsync(dto.UnitOfMeasureId) is null)
            {
                throw new KeyNotFoundException("Đơn vị đo không tồn tại.");
            }

            if (dto.CountryId.HasValue && await repository.GetCountryByIdAsync(dto.CountryId) is null)
            {
                throw new KeyNotFoundException("Quốc gia không tồn tại.");
            }
        }
    }
}
