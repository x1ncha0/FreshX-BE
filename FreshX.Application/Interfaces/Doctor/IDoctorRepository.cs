using FreshX.Application.Dtos;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces;

public interface IDoctorRepository
{
    Task<Doctor> CreateAsync(DoctorCreateUpdateDto request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Doctor>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Doctor?> UpdateAsync(int id, DoctorCreateUpdateDto request, CancellationToken cancellationToken = default);
    Task<Doctor?> SoftDeleteAsync(int id, CancellationToken cancellationToken = default);
}
