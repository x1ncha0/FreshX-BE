using FreshX.Application.Dtos;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Doctor;

namespace FreshX.Application.Interfaces;

public interface IDoctorService
{
    Task<DoctorDto> CreateAsync(DoctorCreateUpdateDto request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DoctorDto>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default);
    Task<DoctorDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DoctorDto?> UpdateAsync(int id, DoctorCreateUpdateDto request, CancellationToken cancellationToken = default);
    Task<DoctorDto?> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
