using FreshX.Application.Dtos.Supplier;

namespace FreshX.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IReadOnlyList<SupplierDetailDto>> GetAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, bool? isForeign, bool? isStateOwned, int? isDeleted, CancellationToken cancellationToken = default);
        Task<SupplierDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<SupplierDetailDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<SupplierDetailDto> CreateAsync(SupplierCreateDto dto, CancellationToken cancellationToken = default);
        Task UpdateByCodeAsync(string code, SupplierUpdateDto dto, CancellationToken cancellationToken = default);
        Task UpdateByIdAsync(int id, SupplierUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
