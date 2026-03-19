using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class ServiceGroupRepository(FreshXDbContext context) : IServiceGroupRepository
    {
        public async Task<IEnumerable<ServiceGroup>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status)
        {
            var query = context.ServiceGroups
                .AsNoTracking()
                .Include(serviceGroup => serviceGroup.ServiceCatalogs.Where(serviceCatalog => !serviceCatalog.IsDeleted))
                .Where(serviceGroup => !serviceGroup.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(serviceGroup =>
                    (serviceGroup.Name != null && serviceGroup.Name.Contains(searchKeyword)) ||
                    (serviceGroup.Code != null && serviceGroup.Code.Contains(searchKeyword)));
            }

            if (createdDate.HasValue)
            {
                query = query.Where(serviceGroup => serviceGroup.CreatedAt >= createdDate.Value);
            }

            if (updatedDate.HasValue)
            {
                query = query.Where(serviceGroup => serviceGroup.UpdatedAt <= updatedDate.Value);
            }

            if (status.HasValue)
            {
                var isSuspended = status.Value == 1;
                query = query.Where(serviceGroup => serviceGroup.IsSuspended == isSuspended);
            }

            return await query
                .OrderBy(serviceGroup => serviceGroup.Name)
                .ToListAsync();
        }

        public Task<ServiceGroup?> GetByIdAsync(int id)
        {
            return context.ServiceGroups
                .AsNoTracking()
                .Include(serviceGroup => serviceGroup.ServiceCatalogs.Where(serviceCatalog => !serviceCatalog.IsDeleted))
                .ThenInclude(serviceCatalog => serviceCatalog.ServiceTypes)
                .FirstOrDefaultAsync(serviceGroup => serviceGroup.Id == id && !serviceGroup.IsDeleted);
        }

        public async Task<ServiceGroup> CreateAsync(ServiceGroup entity)
        {
            await context.ServiceGroups.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(ServiceGroup entity)
        {
            context.ServiceGroups.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.ServiceGroups.FirstOrDefaultAsync(serviceGroup => serviceGroup.Id == id && !serviceGroup.IsDeleted)
                ?? throw new KeyNotFoundException("Nhóm dịch vụ không tồn tại.");

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public Task<IEnumerable<ServiceCatalog>> GetServiceCatalogsByServiceGroupIdAsync(int serviceGroupId)
        {
            return Task.FromResult<IEnumerable<ServiceCatalog>>(context.ServiceCatalogs
                .AsNoTracking()
                .Where(serviceCatalog => serviceCatalog.ServiceGroupId == serviceGroupId && !serviceCatalog.IsDeleted)
                .ToList());
        }

        public Task<ServiceGroup?> GetByNameAsync(string name)
        {
            var normalizedName = name.Trim().ToUpperInvariant();
            return context.ServiceGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(serviceGroup =>
                    serviceGroup.Name != null &&
                    serviceGroup.Name.ToUpper() == normalizedName &&
                    !serviceGroup.IsDeleted);
        }

        public Task<ServiceGroup?> GetByCodeAsync(string code)
        {
            var normalizedCode = code.Trim().ToUpperInvariant();
            return context.ServiceGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(serviceGroup =>
                    serviceGroup.Code != null &&
                    serviceGroup.Code.ToUpper() == normalizedCode &&
                    !serviceGroup.IsDeleted);
        }
    }
}
