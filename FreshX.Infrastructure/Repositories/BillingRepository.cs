using FreshX.Application.Interfaces.Payments;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class BillingRepository(FreshXDbContext context) : IBillingRepository
{
    public async Task<Bill> AddBillAsync(Bill bill)
    {
        await context.Bills.AddAsync(bill);
        await context.SaveChangesAsync();
        return bill;
    }

    public async Task<Bill> GetBillByIdAsync(int billId)
    {
        return await context.Bills
                   .Include(b => b.BillDetails)
                   .Include(b => b.Payments)
                   .FirstOrDefaultAsync(b => b.Id == billId)
               ?? throw new KeyNotFoundException($"Bill {billId} was not found.");
    }

    public async Task<IEnumerable<Bill>> GetAllBillsAsync()
    {
        return await context.Bills
            .AsNoTracking()
            .Include(b => b.BillDetails)
            .Include(b => b.Payments)
            .ToListAsync();
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await context.Payments.AddAsync(payment);
        await context.SaveChangesAsync();
    }

    public async Task<Bill> UpdateAsync(Bill bill)
    {
        context.Bills.Update(bill);
        await context.SaveChangesAsync();
        return bill;
    }

    public async Task<Bill> DeleteAsync(Bill bill)
    {
        context.Bills.Remove(bill);
        await context.SaveChangesAsync();
        return bill;
    }

    public async Task<Bill> GetBillWithDetailsAsync(int billId)
    {
        return await context.Bills
                   .Include(b => b.BillDetails)
                   .ThenInclude(bd => bd.ServiceCatalog)
                   .Include(b => b.Payments)
                   .Include(b => b.Reception!)
                   .ThenInclude(r => r.Patient)
                   .FirstOrDefaultAsync(b => b.Id == billId)
               ?? throw new KeyNotFoundException($"Bill {billId} was not found.");
    }
}
