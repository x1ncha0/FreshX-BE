using Freshx_API.Models;

namespace Freshx_API.Interfaces.ServiceType
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceTypes>> GetAllAsync(string? searchKey);
        Task<ServiceTypes?> GetByIdAsync(int id);
        Task<ServiceTypes> AddAsync(ServiceTypes serviceType);
        Task UpdateAsync(ServiceTypes serviceType);
        Task DeleteAsync(int id);
    }

}
