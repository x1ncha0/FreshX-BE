using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces.ServiceType
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceTypes>> GetAllAsync(string? searchKey);
        Task<ServiceTypes?> GetByIdAsync(int id);
        Task<ServiceTypes> AddAsync(ServiceTypes serviceType);
        Task UpdateAsync(int id, ServiceTypes serviceType);
        Task DeleteAsync(int id);
    }

}

