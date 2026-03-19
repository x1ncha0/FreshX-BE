using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class PharmacyRepository(FreshXDbContext context) : IPharmacyRepository
    {
        public async Task<IEnumerable<Pharmacy>> GetAllAsync(string? searchKeyword, DateTime? CreatedDate, DateTime? UpdatedDate, bool? isSuspended, int? inventoryTypeId, int? specialtyId)
        {
            var query = context.Pharmacies
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(p =>
                    (p.Name != null && p.Name.Contains(searchKeyword)) ||
                    (p.Code != null && p.Code.Contains(searchKeyword)));
            }

            if (CreatedDate.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= CreatedDate.Value);
            }

            if (UpdatedDate.HasValue)
            {
                query = query.Where(p => p.UpdatedAt <= UpdatedDate.Value);
            }

            if (isSuspended.HasValue)
            {
                query = query.Where(p => p.IsSuspended == isSuspended.Value);
            }

            if (inventoryTypeId.HasValue)
            {
                query = query.Where(p => p.InventoryTypeId == inventoryTypeId.Value);
            }

            return await query.OrderBy(p => p.Name).ToListAsync();
        }

        public Task<Pharmacy?> GetByIdAsync(int id)
        {
            return context.Pharmacies
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<Pharmacy> CreateAsync(Pharmacy entity)
        {
            await context.Pharmacies.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Pharmacy entity)
        {
            context.Pharmacies.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.Pharmacies.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (entity is null)
            {
                return;
            }

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public Task<Department?> GetDepartmentByIdAsync(int? departmentId)
        {
            return context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == departmentId && !d.IsDeleted);
        }

        public Task<InventoryType?> GetInventoryTypeByIdAsync(int? inventoryTypeId)
        {
            return context.InventoryTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == inventoryTypeId && !i.IsDeleted);
        }

        public Task<Pharmacy?> GetPharmacyByCodeAsync(string code)
        {
            return context.Pharmacies
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Code == code && !p.IsDeleted);
        }
    }
}
