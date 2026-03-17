using AutoMapper;
using FreshX.Application.Dtos;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Doctor;
using FreshX.Application.Interfaces;

namespace FreshX.Application.Services;

public class DoctorService(IDoctorRepository doctorRepository, IMapper mapper) : IDoctorService
{
    public async Task<DoctorDto> CreateAsync(DoctorCreateUpdateDto request, CancellationToken cancellationToken = default)
    {
        var doctor = await doctorRepository.CreateAsync(request, cancellationToken);
        return mapper.Map<DoctorDto>(doctor);
    }

    public async Task<IReadOnlyList<DoctorDto>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default)
    {
        var doctors = await doctorRepository.GetAsync(parameters, cancellationToken);
        return mapper.Map<IReadOnlyList<DoctorDto>>(doctors);
    }

    public async Task<DoctorDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var doctor = await doctorRepository.GetByIdAsync(id, cancellationToken);
        return doctor is null ? null : mapper.Map<DoctorDetailDto>(doctor);
    }

    public async Task<DoctorDto?> UpdateAsync(int id, DoctorCreateUpdateDto request, CancellationToken cancellationToken = default)
    {
        var doctor = await doctorRepository.UpdateAsync(id, request, cancellationToken);
        return doctor is null ? null : mapper.Map<DoctorDto>(doctor);
    }

    public async Task<DoctorDto?> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var doctor = await doctorRepository.SoftDeleteAsync(id, cancellationToken);
        return doctor is null ? null : mapper.Map<DoctorDto>(doctor);
    }
}
