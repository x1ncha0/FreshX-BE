using Freshx_API.Dtos.Prescription;

namespace Freshx_API.Interfaces.IPrescription
{
    public interface IPrescriptionDetailService
    {
        Task AddAsync(CreatePrescriptionDetailDto detailDto);
        Task UpdateAsync(UpdatePrescriptionDetailDto detailDto);
        Task DeleteAsync(int id);
        Task<List<DetailDto>> GetByPrescriptionIdAsync(int prescriptionId);
    }

}
