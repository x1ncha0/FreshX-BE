using FreshX.Application.Dtos.DepartmentDtos;

namespace FreshX.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IReadOnlyList<DepartmentDto>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status,
            CancellationToken cancellationToken = default);

        Task<DepartmentDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<DepartmentDto> CreateAsync(DepartmentCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task<DepartmentDto?> UpdateAsync(int id, DepartmentCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task<DepartmentDto?> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
