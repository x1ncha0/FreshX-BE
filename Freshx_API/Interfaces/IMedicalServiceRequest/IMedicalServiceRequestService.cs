using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos;

namespace Freshx_API.Interfaces
{
    public interface IMedicalServiceRequestService
    {
        Task<MedicalServiceRequestDto> GetByIdAsync(int id);
        Task<IEnumerable<MedicalServiceRequestDto>> GetAllAsync();
        Task<MedicalServiceRequestDto> AddAsync(CreateMedicalServiceRequestDto medicalServiceRequestDto);
        Task<MedicalServiceRequestDto> UpdateAsync(CreateMedicalServiceRequestDto medicalServiceRequestDto);
        Task DeleteAsync(int id);
    }

}
