using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    public interface IMedicalServiceRequestRepository
    {
        Task<MedicalServiceRequest> GetByIdAsync(int id);
        Task<IEnumerable<MedicalServiceRequest>> GetAllAsync();
        Task<MedicalServiceRequest> AddAsync(MedicalServiceRequest medicalServiceRequest);
        Task<MedicalServiceRequest> UpdateAsync(MedicalServiceRequest medicalServiceRequest);
        Task DeleteAsync(int id);
    }

}
