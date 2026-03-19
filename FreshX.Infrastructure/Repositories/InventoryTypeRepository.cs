using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class InventoryTypeRepository(FreshXDbContext context) : IInventoryTypeRepository
    {
        public async Task<IEnumerable<InventoryType>> GetAllAsync(string? searchKeyword = null)
        {
            var query = context.InventoryTypes
                .AsNoTracking()
                .Where(i => !i.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(i =>
                    (i.Name != null && i.Name.Contains(searchKeyword)) ||
                    (i.Code != null && i.Code.Contains(searchKeyword)));
            }

            return await query.OrderBy(i => i.Name).ToListAsync();
        }

        public Task<InventoryType?> GetByIdAsync(int id)
        {
            return context.InventoryTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }

        public Task<InventoryType?> GetNameAsync(string name)
        {
            var normalizedName = name.Trim().ToUpperInvariant();
            return context.InventoryTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Name != null && i.Name.ToUpper() == normalizedName && !i.IsDeleted);
        }

        public Task<InventoryType?> GetByCodeAsync(string code)
        {
            return context.InventoryTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Code == code && !i.IsDeleted);
        }

        public async Task<InventoryType> CreateAsync(InventoryType entity)
        {
            await context.InventoryTypes.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(InventoryType entity)
        {
            context.InventoryTypes.Update(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await context.InventoryTypes.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
            if (entity is null)
            {
                return false;
            }

            entity.IsDeleted = true;
            return await context.SaveChangesAsync() > 0;
        }

        public async Task DeleteAsyncCode(string code)
        {
            var entity = await context.InventoryTypes.FirstOrDefaultAsync(i => i.Code == code && !i.IsDeleted)
                ?? throw new KeyNotFoundException("Inventory Type không tồn tại.");

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }
}
