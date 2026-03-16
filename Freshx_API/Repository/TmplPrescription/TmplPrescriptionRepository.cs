
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class TmplPrescriptionRepository 
    {
        private readonly FreshxDBContext _context;

        public TmplPrescriptionRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<List<TemplatePrescription>> GetAllAsync(string? searchKey)
        {
            return await _context.TemplatePrescriptions
                .Include(p => p.TemplatePrescriptionDetails)
                .ThenInclude(d => d.DrugCatalog)
                .Where(p => string.IsNullOrEmpty(searchKey) || p.Note.Contains(searchKey))
                .ToListAsync();
        }

        public async Task<TemplatePrescription?> GetByIdAsync(int id)
        {
            return await _context.TemplatePrescriptions
                .Include(p => p.TemplatePrescriptionDetails)
                .ThenInclude(d => d.DrugCatalog)
                .FirstOrDefaultAsync(p => p.TemplatePrescriptionId == id);
        }

        public async Task<TemplatePrescription> AddAsync(TemplatePrescription TemplatePrescription)
        {
            await _context.TemplatePrescriptions.AddAsync(TemplatePrescription);
            await _context.SaveChangesAsync();
            return TemplatePrescription;
        }

        public async Task<TemplatePrescription> UpdateAsync(TemplatePrescription TemplatePrescription)
        {
            _context.TemplatePrescriptions.Update(TemplatePrescription);
            await _context.SaveChangesAsync();
            return TemplatePrescription;
        }

        public async Task DeleteAsync(int id)
        {
            var TemplatePrescription = await _context.TemplatePrescriptions.FindAsync(id);
            if (TemplatePrescription != null)
            {
                _context.TemplatePrescriptions.Remove(TemplatePrescription);
                await _context.SaveChangesAsync();
            }
        }
    }

}
