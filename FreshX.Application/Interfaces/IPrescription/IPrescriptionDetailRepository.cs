using FreshX.Application.Dtos;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces.IPrescription
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

