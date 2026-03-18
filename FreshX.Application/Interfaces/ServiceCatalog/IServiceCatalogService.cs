using FreshX.Application.Dtos.ServiceCatalog;

namespace FreshX.Application.Interfaces
{
    public interface IServiceCatalogService
    {
        Task<IReadOnlyList<ServiceCatalogDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status,
            CancellationToken cancellationToken = default);

        Task<ServiceCatalogDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ServiceCatalogDto> CreateAsync(ServiceCatalogCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task<ServiceCatalogDto?> UpdateAsync(int id, ServiceCatalogCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task<ServiceCatalogDto?> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
