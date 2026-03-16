using Freshx_API.Interfaces.ServiceType;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly FreshxDBContext _context;

        public ServiceTypeRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceTypes>> GetAllAsync(string? searchKey)
        {
            return await _context.Set<ServiceTypes>()
                .Where(st => string.IsNullOrEmpty(searchKey) || st.Name.Contains(searchKey) || st.Code.Contains(searchKey))
                .ToListAsync();
        }

        public async Task<ServiceTypes?> GetByIdAsync(int id)
        {
            return await _context.Set<ServiceTypes>().FindAsync(id);
        }

        public async Task<ServiceTypes> AddAsync(ServiceTypes serviceType)
        {
            await _context.Set<ServiceTypes>().AddAsync(serviceType);
            await _context.SaveChangesAsync();
            return serviceType;
        }

        public async Task UpdateAsync(ServiceTypes serviceType)
        {
            _context.Set<ServiceTypes>().Update(serviceType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var serviceType = await GetByIdAsync(id);
            if (serviceType != null)
            {
                _context.Set<ServiceTypes>().Remove(serviceType);
                await _context.SaveChangesAsync();
            }
        }
    }

}
