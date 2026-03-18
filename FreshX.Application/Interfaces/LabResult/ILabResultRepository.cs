using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface ILabResultRepository
    {
        Task<IEnumerable<LabResult>> GetAllAsync(string? searchKey = null);
        Task<LabResult?> GetByIdAsync(int id);
        Task AddAsync(LabResult labResult);
        Task UpdateAsync(LabResult labResult);
        Task DeleteAsync(int id);
    }
}
