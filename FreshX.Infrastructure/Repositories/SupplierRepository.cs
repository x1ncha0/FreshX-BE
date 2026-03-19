using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class SupplierRepository(FreshXDbContext context) : ISupplierRepository
    {
        public async Task<IEnumerable<Supplier>> GetAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, bool? isForeign, bool? isStateOwned, int? isDeleted)
        {
            var query = context.Suppliers.AsNoTracking().AsQueryable();

            query = isDeleted.HasValue
                ? query.Where(s => s.IsDeleted == (isDeleted.Value == 1))
                : query.Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(s =>
                    (s.Name != null && s.Name.Contains(searchKeyword)) ||
                    (s.Code != null && s.Code.Contains(searchKeyword)));
            }

            if (createdDate.HasValue)
            {
                query = query.Where(s => s.CreatedAt >= createdDate.Value);
            }

            if (updatedDate.HasValue)
            {
                query = query.Where(s => s.UpdatedAt <= updatedDate.Value);
            }

            if (isSuspended.HasValue)
            {
                query = query.Where(s => s.IsSuspended == isSuspended.Value);
            }

            if (isForeign.HasValue)
            {
                query = query.Where(s => s.IsForeign == isForeign.Value);
            }

            if (isStateOwned.HasValue)
            {
                query = query.Where(s => s.IsStateOwned == isStateOwned.Value);
            }

            return await query.OrderBy(s => s.Name).ToListAsync();
        }

        public Task<Supplier?> GetByIdAsync(int id)
        {
            return context.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<Supplier> CreateAsync(Supplier entity)
        {
            await context.Suppliers.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public Task<Supplier?> GetSupplierByCodeAsync(string code)
        {
            return context.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Code == code && !s.IsDeleted);
        }

        public async Task UpdateAsync(Supplier entity)
        {
            context.Suppliers.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsyncId(int id)
        {
            var entity = await context.Suppliers.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
            if (entity is null)
            {
                return;
            }

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsyncCode(string code)
        {
            var entity = await context.Suppliers.FirstOrDefaultAsync(s => s.Code == code && !s.IsDeleted)
                ?? throw new KeyNotFoundException("Nhà cung cấp không tồn tại.");

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }
}
