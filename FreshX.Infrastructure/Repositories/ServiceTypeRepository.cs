using FreshX.Application.Interfaces.ServiceType;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class ServiceTypeRepository(FreshXDbContext context) : IServiceTypeRepository
{
    public async Task<IEnumerable<ServiceTypes>> GetAllAsync(string? searchKey)
    {
        return await context.ServiceTypes
            .AsNoTracking()
            .Where(st => string.IsNullOrEmpty(searchKey) ||
                         (st.Name != null && st.Name.Contains(searchKey)) ||
                         (st.Code != null && st.Code.Contains(searchKey)))
            .ToListAsync();
    }

    public Task<ServiceTypes?> GetByIdAsync(int id) => context.ServiceTypes.FindAsync(id).AsTask();

    public async Task<ServiceTypes> AddAsync(ServiceTypes serviceType)
    {
        await context.ServiceTypes.AddAsync(serviceType);
        await context.SaveChangesAsync();
        return serviceType;
    }

    public async Task UpdateAsync(ServiceTypes serviceType)
    {
        context.ServiceTypes.Update(serviceType);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var serviceType = await GetByIdAsync(id);
        if (serviceType is not null)
        {
            context.ServiceTypes.Remove(serviceType);
            await context.SaveChangesAsync();
        }
    }
}