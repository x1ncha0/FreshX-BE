using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class DrugCatalogRepository(FreshXDbContext context) : IDrugCatalogRepository
    {
        public async Task<IEnumerable<DrugCatalog>> GetAllAsync(string? searchKeyword, DateTime? startDate, DateTime? endDate, int? status)
        {
            var query = context.DrugCatalogs
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(d =>
                    (d.Name != null && d.Name.Contains(searchKeyword)) ||
                    (d.Code != null && d.Code.Contains(searchKeyword)) ||
                    (d.FullName != null && d.FullName.Contains(searchKeyword)));
            }

            if (startDate.HasValue)
            {
                query = query.Where(d => d.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(d => d.UpdatedAt <= endDate.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(d => d.IsSuspended == (status.Value == 1));
            }

            return await query.OrderBy(d => d.Name).ToListAsync();
        }

        public Task<DrugCatalog?> GetByIdAsync(int id)
        {
            return context.DrugCatalogs
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<DrugCatalog> CreateAsync(DrugCatalog entity)
        {
            await context.DrugCatalogs.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(DrugCatalog entity)
        {
            context.DrugCatalogs.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.DrugCatalogs.FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
            if (entity is null)
            {
                return;
            }

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public Task<DrugType?> GetDrugTypeByIdAsync(int? drugTypeId)
        {
            return context.DrugTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == drugTypeId && !d.IsDeleted);
        }

        public Task<Supplier?> GetManufacturerByIdAsync(int? manufacturerId)
        {
            return context.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == manufacturerId && !s.IsDeleted);
        }

        public Task<UnitOfMeasure?> GetUnitOfMeasureByIdAsync(int? unitOfMeasureId)
        {
            return context.UnitOfMeasures
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == unitOfMeasureId && !u.IsDeleted);
        }

        public Task<Country?> GetCountryByIdAsync(int? countryId)
        {
            return context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == countryId && !c.IsDeleted);
        }
    }
}
