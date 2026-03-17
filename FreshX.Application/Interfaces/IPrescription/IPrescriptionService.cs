using FreshX.Application.Dtos.Prescription;
using FreshX.Domain.Entities;
namespace FreshX.Application.Interfaces.IPrescription
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

