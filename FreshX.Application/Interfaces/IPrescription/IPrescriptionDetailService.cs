using FreshX.Application.Dtos.Prescription;

namespace FreshX.Application.Interfaces.IPrescription
{
    public interface IPrescriptionDetailService
    {
        Task AddAsync(CreatePrescriptionDetailDto detailDto);
        Task UpdateAsync(UpdatePrescriptionDetailDto detailDto);
        Task DeleteAsync(int id);
        Task<List<DetailDto>> GetByPrescriptionIdAsync(int prescriptionId);
    }

}

