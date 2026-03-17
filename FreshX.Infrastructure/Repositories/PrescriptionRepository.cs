using FreshX.Application.Interfaces.IPrescription;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class PrescriptionRepository(FreshXDbContext context) : IPrescriptionRepository
{
    public async Task<List<Prescription>> GetAllAsync(string? searchKey)
    {
        return await context.Prescriptions
            .AsNoTracking()
            .Include(p => p.PrescriptionDetails)
            .ThenInclude(d => d.DrugCatalog)
            .Where(p => string.IsNullOrEmpty(searchKey) || (p.Note != null && p.Note.Contains(searchKey)))
            .ToListAsync();
    }

    public Task<Prescription?> GetByIdAsync(int id) =>
        context.Prescriptions
            .Include(p => p.PrescriptionDetails)
            .ThenInclude(d => d.DrugCatalog)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Prescription> AddAsync(Prescription prescription)
    {
        await context.Prescriptions.AddAsync(prescription);
        await context.SaveChangesAsync();
        return prescription;
    }

    public async Task<Prescription> UpdateAsync(Prescription prescription)
    {
        context.Prescriptions.Update(prescription);
        await context.SaveChangesAsync();
        return prescription;
    }

    public async Task DeleteAsync(int id)
    {
        var prescription = await context.Prescriptions.FindAsync(id);
        if (prescription is not null)
        {
            context.Prescriptions.Remove(prescription);
            await context.SaveChangesAsync();
        }
    }
}