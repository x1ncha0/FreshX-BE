using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface IPositionRepository
    {
        Task<IReadOnlyList<Position>> GetAllAsync();
        Task<Position?> GetByIdAsync(int id);
    }
}
