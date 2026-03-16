using Freshx_API.Dtos.Prescription;
using Freshx_API.Models;
namespace Freshx_API.Interfaces.IPrescription
{
    public interface IPrescriptionService
    {
        Task<List<PrescriptionDto>> GetAllAsync(string? searchKey);
        Task<PrescriptionDto?> GetByIdAsync(int id);
        Task<PrescriptionDto> AddAsync(CreatePrescriptionDto prescriptionDto);
        Task<PrescriptionDto> UpdateAsync(UpdatePrescriptionDto prescriptionDto);
        Task DeleteAsync(int id);
    }

}
