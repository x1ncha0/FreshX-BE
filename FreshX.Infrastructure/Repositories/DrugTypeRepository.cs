using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class DrugTypeRepository(FreshXDbContext context) : IDrugTypeRepository
{
    public async Task<List<DrugType>> GetDrugTypeAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, int? status)
    {
        var query = context.DrugTypes.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchKeyword))
        {
            query = query.Where(d =>
                (d.Name != null && d.Name.Contains(searchKeyword)) ||
                (d.Code != null && d.Code.Contains(searchKeyword)));
        }

        if (createdDate.HasValue)
        {
            query = query.Where(d => d.CreatedDate >= createdDate.Value);
        }

        if (updatedDate.HasValue)
        {
            query = query.Where(d => d.UpdatedDate <= updatedDate.Value);
        }

        if (status.HasValue)
        {
            query = status.Value switch
            {
                0 => query.Where(d => !d.IsSuspended && !d.IsDeleted),
                1 => query.Where(d => d.IsSuspended),
                2 => query.Where(d => d.IsDeleted),
                _ => query
            };
        }

        return await query.ToListAsync();
    }

    public Task<DrugType?> GetDrugTypeByIdAsync(int id) =>
        context.DrugTypes.FirstOrDefaultAsync(d => d.Id == id);

    public async Task<DrugType> CreateDrugTypeAsync(DrugType drugType)
    {
        context.DrugTypes.Add(drugType);
        await context.SaveChangesAsync();
        return drugType;
    }

    public async Task<DrugType?> UpdateDrugTypeAsync(DrugType drugType)
    {
        context.DrugTypes.Update(drugType);
        await context.SaveChangesAsync();
        return drugType;
    }

    public async Task<bool> SoftDeleteDrugTypeAsync(int id)
    {
        var drugType = await GetDrugTypeByIdAsync(id);
        if (drugType is null)
        {
            return false;
        }

        drugType.IsDeleted = true;
        drugType.UpdatedDate = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteDrugTypeAsync(int id)
    {
        var drugType = await GetDrugTypeByIdAsync(id);
        if (drugType is null)
        {
            return false;
        }

        context.DrugTypes.Remove(drugType);
        await context.SaveChangesAsync();
        return true;
    }
}
