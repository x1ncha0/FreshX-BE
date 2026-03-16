using Freshx_API.Models;
namespace Freshx_API.Interfaces.IPrescription
{
    public interface IPrescriptionRepository
    {
        Task<List<Prescription>> GetAllAsync(string? searchKey);
        Task<Prescription?> GetByIdAsync(int id);
        Task<Prescription> AddAsync(Prescription prescription);
        Task<Prescription> UpdateAsync(Prescription prescription);
        Task DeleteAsync(int id);
    }

}
