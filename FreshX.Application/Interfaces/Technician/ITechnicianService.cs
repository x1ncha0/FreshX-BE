using FreshX.Application.Dtos;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Technician;

namespace FreshX.Application.Interfaces
{
    public interface ITechnicianService
    {
        Task<TechnicianDto> CreateAsync(TechnicianRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TechnicianDto>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default);
        Task<TechnicianDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<TechnicianDto?> UpdateAsync(int id, TechnicianRequest request, CancellationToken cancellationToken = default);
        Task<TechnicianDto?> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
