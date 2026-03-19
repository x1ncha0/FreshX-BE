using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class ServiceCatalogRepository(FreshXDbContext context) : IServiceCatalogRepository
    {
        public async Task<IEnumerable<ServiceCatalog>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status)
        {
            var query = context.ServiceCatalogs
                .AsNoTracking()
                .Include(serviceCatalog => serviceCatalog.ServiceGroup)
                .Include(serviceCatalog => serviceCatalog.ServiceTypes)
                .Include(serviceCatalog => serviceCatalog.ParentService)
                .Where(serviceCatalog => !serviceCatalog.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(serviceCatalog =>
                    serviceCatalog.Name.Contains(searchKeyword) ||
                    (serviceCatalog.Code != null && serviceCatalog.Code.Contains(searchKeyword)));
            }

            if (createdDate.HasValue)
            {
                query = query.Where(serviceCatalog => serviceCatalog.CreatedAt >= createdDate.Value);
            }

            if (updatedDate.HasValue)
            {
                query = query.Where(serviceCatalog => serviceCatalog.UpdatedAt <= updatedDate.Value);
            }

            if (status.HasValue)
            {
                var isSuspended = status.Value == 1;
                query = query.Where(serviceCatalog => serviceCatalog.IsSuspended == isSuspended);
            }

            return await query
                .OrderBy(serviceCatalog => serviceCatalog.Name)
                .ToListAsync();
        }

        public Task<ServiceCatalog?> GetByIdAsync(int id)
        {
            return context.ServiceCatalogs
                .AsNoTracking()
                .Include(serviceCatalog => serviceCatalog.ServiceGroup)
                .Include(serviceCatalog => serviceCatalog.ServiceTypes)
                .Include(serviceCatalog => serviceCatalog.ParentService)
                .FirstOrDefaultAsync(serviceCatalog => serviceCatalog.Id == id && !serviceCatalog.IsDeleted);
        }

        public async Task<ServiceCatalog> CreateAsync(ServiceCatalog entity)
        {
            await context.ServiceCatalogs.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(ServiceCatalog entity)
        {
            context.ServiceCatalogs.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.ServiceCatalogs.FirstOrDefaultAsync(serviceCatalog => serviceCatalog.Id == id && !serviceCatalog.IsDeleted)
                ?? throw new KeyNotFoundException("Danh mục dịch vụ không tồn tại.");

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public Task<bool> CheckStatusByIdAsync(int id)
        {
            return context.ServiceCatalogs
                .AsNoTracking()
                .AnyAsync(serviceCatalog => serviceCatalog.Id == id && !serviceCatalog.IsDeleted && !serviceCatalog.IsSuspended);
        }

        public Task<ServiceCatalog?> GetByCodeAsync(string code)
        {
            var normalizedCode = code.Trim().ToUpperInvariant();
            return context.ServiceCatalogs
                .AsNoTracking()
                .FirstOrDefaultAsync(serviceCatalog =>
                    serviceCatalog.Code != null &&
                    serviceCatalog.Code.ToUpper() == normalizedCode &&
                    !serviceCatalog.IsDeleted);
        }
    }
}
