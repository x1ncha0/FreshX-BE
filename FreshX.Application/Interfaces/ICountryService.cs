using FreshX.Application.Dtos.Country;

namespace FreshX.Application.Interfaces
{
    public interface ICountryService
    {
        Task<IReadOnlyList<CountryDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            int? isDeleted,
            CancellationToken cancellationToken = default);

        Task<CountryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CountryDto> CreateAsync(CountryCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, CountryCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
