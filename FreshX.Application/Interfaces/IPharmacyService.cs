using FreshX.Application.Dtos.Pharmacy;

namespace FreshX.Application.Interfaces
{
    public interface IPharmacyService
    {
        Task<IReadOnlyList<PharmacyDto>> GetAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, int? inventoryTypeId, int? specialtyId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PharmacyDetailDto>> GetDetailAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, int? inventoryTypeId, int? specialtyId, CancellationToken cancellationToken = default);
        Task<PharmacyDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<PharmacyDto> CreateAsync(PharmacyCreateDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, PharmacyUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
