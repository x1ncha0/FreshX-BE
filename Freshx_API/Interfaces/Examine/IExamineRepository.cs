using Freshx_API.Models;
namespace Freshx_API.Interfaces
{
    public interface IExamineRepository
    {
        Task<Examine> AddAsync(Examine examine);
        Task<Examine?> GetByIdAsync(int id);
        Task<IEnumerable<Examine>> GetAllAsync();
        Task UpdateAsync(Examine examine);
        Task DeleteAsync(int id);
    }
}
