using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    public interface ILabResultRepository
    {
        Task<IEnumerable<LabResult>> GetAllAsync(string searchKey = null);
        Task<LabResult?> GetByIdAsync(int id);
        Task AddAsync(LabResult labResult);
        Task UpdateAsync(LabResult labResult);
        Task DeleteAsync(int id);
    }
}