using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class LabResultRepository(FreshXDbContext context) : ILabResultRepository
{
    public async Task<IEnumerable<LabResult>> GetAllAsync(string? searchKey = null)
    {
        var query = context.LabResults.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(searchKey))
        {
            query = query.Where(lr =>
                (lr.Conclusion != null && lr.Conclusion.Contains(searchKey)) ||
                (lr.Description != null && lr.Description.Contains(searchKey)) ||
                (lr.Note != null && lr.Note.Contains(searchKey)));
        }

        return await query.ToListAsync();
    }

    public Task<LabResult?> GetByIdAsync(int id) => context.LabResults.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(LabResult labResult)
    {
        await context.LabResults.AddAsync(labResult);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LabResult labResult)
    {
        context.LabResults.Update(labResult);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var labResult = await GetByIdAsync(id);
        if (labResult is not null)
        {
            labResult.IsDeleted = true;
            labResult.UpdatedDate = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }
}
