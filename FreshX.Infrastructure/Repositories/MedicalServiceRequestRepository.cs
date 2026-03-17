using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class MedicalServiceRequestRepository(FreshXDbContext context) : IMedicalServiceRequestRepository
{
    public async Task<MedicalServiceRequest> GetByIdAsync(int id)
    {
        return await context.MedicalServiceRequests
                   .Include(r => r.Service)
                   .ThenInclude(s => s!.ServiceTypes)
                   .FirstOrDefaultAsync(msr => msr.Id == id)
               ?? throw new KeyNotFoundException($"Medical service request {id} was not found.");
    }

    public async Task<IEnumerable<MedicalServiceRequest>> GetAllAsync()
    {
        return await context.MedicalServiceRequests
            .AsNoTracking()
            .Include(r => r.Service)
            .ThenInclude(s => s!.ServiceTypes)
            .ToListAsync();
    }

    public async Task<MedicalServiceRequest> AddAsync(MedicalServiceRequest medicalServiceRequest)
    {
        context.MedicalServiceRequests.Add(medicalServiceRequest);
        await context.SaveChangesAsync();
        return medicalServiceRequest;
    }

    public async Task<MedicalServiceRequest> UpdateAsync(MedicalServiceRequest medicalServiceRequest)
    {
        context.MedicalServiceRequests.Update(medicalServiceRequest);
        await context.SaveChangesAsync();
        return medicalServiceRequest;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        context.MedicalServiceRequests.Remove(entity);
        await context.SaveChangesAsync();
    }
}