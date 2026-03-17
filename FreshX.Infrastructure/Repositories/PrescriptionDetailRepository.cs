using FreshX.Application.Interfaces.IPrescription;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class PrescriptionDetailRepository(FreshXDbContext context) : IPrescriptionDetailRepository
{
    public async Task<List<PrescriptionDetail>> GetAllAsync(string? searchKey)
    {
        var query = await context.PrescriptionDetails.AsNoTracking().ToListAsync();
        if (!string.IsNullOrWhiteSpace(searchKey))
        {
            query = query.Where(p => p.Note != null && p.Note.Contains(searchKey)).ToList();
        }

        return query;
    }

    public Task<PrescriptionDetail?> GetByIdAsync(int id) => context.PrescriptionDetails.FindAsync(id).AsTask();

    public Task<List<PrescriptionDetail?>> GetByPrescriptionIdAsync(int prescriptionId) =>
        context.PrescriptionDetails
            .AsNoTracking()
            .Where(d => d.PrescriptionId == prescriptionId)
            .Include(d => d.DrugCatalog)
            .Cast<PrescriptionDetail?>()
            .ToListAsync();

    public async Task AddAsync(PrescriptionDetail prescriptionDetail)
    {
        await context.PrescriptionDetails.AddAsync(prescriptionDetail);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PrescriptionDetail prescriptionDetail)
    {
        context.PrescriptionDetails.Update(prescriptionDetail);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(PrescriptionDetail detail)
    {
        context.PrescriptionDetails.Remove(detail);
        await context.SaveChangesAsync();
    }
}