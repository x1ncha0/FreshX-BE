using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.DepartmenTypeDtos;

namespace FreshX.Application.Interfaces
{
    public interface IFixDepartmentTypeService
    {
        Task<DepartmentTypeDto> CreateAsync(DepartmentTypeCreateUpdateDto request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DepartmentTypeDto>> GetAllAsync(Parameters parameters, CancellationToken cancellationToken = default);
        Task<DepartmentTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<DepartmentTypeDto> UpdateAsync(int id, DepartmentTypeCreateUpdateDto request, CancellationToken cancellationToken = default);
        Task<DepartmentTypeDto> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
