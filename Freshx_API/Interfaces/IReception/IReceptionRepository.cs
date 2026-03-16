using Freshx_API.Models;

namespace Freshx_API.Interfaces.IReception
{
    public interface IReceptionRepository
    {
        Task<Reception?> GetByIdAsync(int id);
        Task<IEnumerable<Reception>> GetAllAsync();
        Task<Reception?> AddAsync(Reception reception);
        Task UpdateAsync(Reception reception);
        Task DeleteAsync(int id);
    }

}
