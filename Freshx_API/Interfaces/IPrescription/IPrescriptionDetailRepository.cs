using Freshx_API.Dtos;
using Freshx_API.Models;

namespace Freshx_API.Interfaces.IPrescription
{
    public interface IPrescriptionDetailRepository
    {
        Task AddAsync(PrescriptionDetail prescriptionDetail);
        Task UpdateAsync(PrescriptionDetail prescriptionDetail);
        Task DeleteAsync(PrescriptionDetail detail);
        Task<List<PrescriptionDetail>> GetAllAsync(string? searchKey);
        Task<PrescriptionDetail?> GetByIdAsync(int id);
        Task<List<PrescriptionDetail?>> GetByPrescriptionIdAsync(int prescriptionId);
    }

}
