using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces.IReception
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

