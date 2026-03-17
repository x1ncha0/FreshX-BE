using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
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

