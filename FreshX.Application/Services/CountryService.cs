using AutoMapper;
using FreshX.Application.Dtos.Country;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services
{
    public class CountryService(
        ICountryRepository repository,
        IMapper mapper,
        ITokenRepository tokenRepository) : ICountryService
    {
        public async Task<IReadOnlyList<CountryDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            int? isDeleted,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await repository.GetAllAsync(searchKeyword, createdDate, updatedDate, isSuspended, isDeleted);
            return mapper.Map<IReadOnlyList<CountryDto>>(entities);
        }

        public async Task<CountryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<CountryDto>(entity);
        }

        public async Task<CountryDto> CreateAsync(CountryCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = mapper.Map<Country>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = tokenRepository.GetUserIdFromToken();
            entity.IsSuspended = (dto.IsSuspended ?? 0) == 1;
            entity.IsDeleted = (dto.IsDeleted ?? 0) == 1;

            var createdEntity = await repository.CreateAsync(entity);
            return mapper.Map<CountryDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, CountryCreateUpdateDto dto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Quốc gia không tồn tại.");

            mapper.Map(dto, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;
            existingEntity.UpdatedBy = tokenRepository.GetUserIdFromToken();

            await repository.UpdateAsync(existingEntity);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var existingEntity = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Quốc gia không tồn tại.");

            await repository.DeleteAsync(existingEntity.Id);
        }
    }
}
