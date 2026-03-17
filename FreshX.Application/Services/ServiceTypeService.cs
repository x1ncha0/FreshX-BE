using FreshX.Application.Interfaces.ServiceType;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class ServiceTypeService(IServiceTypeRepository repository) : IServiceTypeService
{
    public Task<IEnumerable<ServiceTypes>> GetAllAsync(string? searchKey) => repository.GetAllAsync(searchKey);

    public Task<ServiceTypes?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

    public Task<ServiceTypes> AddAsync(ServiceTypes serviceType) => repository.AddAsync(serviceType);

    public async Task UpdateAsync(int id, ServiceTypes serviceType)
    {
        var existing = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Service type {id} was not found.");
        existing.Code = serviceType.Code;
        existing.Name = serviceType.Name;
        existing.IsDeleted = serviceType.IsDeleted;
        existing.IsSuspended = serviceType.IsSuspended;
        existing.UpdatedAt = DateTime.UtcNow;
        existing.UpdatedBy = serviceType.UpdatedBy;

        await repository.UpdateAsync(existing);
    }

    public Task DeleteAsync(int id) => repository.DeleteAsync(id);
}