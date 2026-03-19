using FreshX.Application.Dtos.UnitOfMeasure;

namespace FreshX.Application.Interfaces
{
    public interface IUnitOfMeasureService
    {
        Task<IReadOnlyList<UnitOfMeasureDetailDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? isSuspended,
            int? isDeleted,
            CancellationToken cancellationToken = default);

        Task<UnitOfMeasureDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<UnitOfMeasureDetailDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<UnitOfMeasureDetailDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<UnitOfMeasureDetailDto> CreateAsync(UnitOfMeasureCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task UpdateByIdAsync(int id, UnitOfMeasureCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task UpdateByCodeAsync(string code, UnitOfMeasureCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
