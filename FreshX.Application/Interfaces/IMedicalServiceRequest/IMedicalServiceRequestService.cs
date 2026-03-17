using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos;

namespace FreshX.Application.Interfaces
{
    public interface IMedicalServiceRequestService
    {
        Task<MedicalServiceRequestDto> GetByIdAsync(int id);
        Task<IEnumerable<MedicalServiceRequestDto>> GetAllAsync();
        Task<MedicalServiceRequestDto> AddAsync(CreateMedicalServiceRequestDto medicalServiceRequestDto);
        Task<MedicalServiceRequestDto> UpdateAsync(int id, UpdateMedicalServiceRequestDto medicalServiceRequestDto);
        Task DeleteAsync(int id);
    }

}

