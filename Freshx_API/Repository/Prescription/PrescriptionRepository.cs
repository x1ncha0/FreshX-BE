using Freshx_API.Interfaces.IPrescription;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly FreshxDBContext _context;

        public PrescriptionRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<List<Prescription>> GetAllAsync(string? searchKey)
        {
            return await _context.Prescriptions
                .Include(p => p.PrescriptionDetails)
                .ThenInclude(d => d.DrugCatalog)
                .Where(p => string.IsNullOrEmpty(searchKey) || p.Note.Contains(searchKey))
                .ToListAsync();
        }

        public async Task<Prescription?> GetByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.PrescriptionDetails)
                .ThenInclude(d => d.DrugCatalog)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        public async Task<Prescription> AddAsync(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<Prescription> UpdateAsync(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task DeleteAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();
            }
        }
    }

}
