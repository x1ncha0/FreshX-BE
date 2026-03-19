using FreshX.Application.Dtos.ServiceGroup;

namespace FreshX.Application.Interfaces
{
    public interface IServiceGroupService
    {
        Task<IReadOnlyList<ServiceGroupDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ServiceGroupDetailDto>> GetDetailAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status,
            CancellationToken cancellationToken = default);

        Task<ServiceGroupDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ServiceGroupDto> CreateAsync(ServiceGroupCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task<ServiceGroupDto?> UpdateAsync(int id, ServiceGroupCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task<ServiceGroupDto?> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
