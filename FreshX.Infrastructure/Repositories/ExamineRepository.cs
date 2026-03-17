using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class ExamineRepository(FreshXDbContext context) : IExamineRepository
{
    public async Task<Examine> AddAsync(Examine examine)
    {
        await context.Examines.AddAsync(examine);
        await context.SaveChangesAsync();
        return examine;
    }

    public Task<Examine?> GetByIdAsync(int id) =>
        context.Examines
            .Include(e => e.Reception)
            .Include(e => e.Prescription)
            .ThenInclude(p => p!.PrescriptionDetails)
            .ThenInclude(d => d.DrugCatalog)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<Examine>> GetAllAsync()
    {
        return await context.Examines
            .AsNoTracking()
            .Include(e => e.Reception)
            .Include(e => e.Prescription)
            .ThenInclude(p => p!.PrescriptionDetails)
            .ThenInclude(d => d.DrugCatalog)
            .ToListAsync();
    }

    public async Task UpdateAsync(Examine examine)
    {
        context.Examines.Update(examine);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var examine = await context.Examines.FindAsync(id);
        if (examine is not null)
        {
            examine.IsDeleted = true;
            examine.UpdatedDate = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }
}