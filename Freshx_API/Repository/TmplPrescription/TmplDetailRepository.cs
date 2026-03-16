using Freshx_API.Interfaces.IPrescription;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class TmplDetailRepository
    {
        private readonly FreshxDBContext _context;

        public TmplDetailRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<List<TemplatePrescriptionDetail>> GetAllAsync(string? searchKey)
        {
             var query = await _context.TemplatePrescriptionDetails.ToListAsync();
              if (!string.IsNullOrWhiteSpace(searchKey))
            {
                query = query.Where(p => p.Note.Contains(searchKey)).ToList();
            }
            return query;
        }

        public async Task<TemplatePrescriptionDetail?> GetByIdAsync(int id)
        {
            return await _context.TemplatePrescriptionDetails.FindAsync(id);
        }
        public async Task<List<TemplatePrescriptionDetail?>> GetByPrescriptionIdAsync(int templateprescriptionId)
        {
            return await _context.TemplatePrescriptionDetails
                .Where(d => d.TemplatePrescriptionId == templateprescriptionId)
                .Include(d => d.DrugCatalog)
                .ToListAsync();
        }

        public async Task AddAsync(TemplatePrescriptionDetail templatePrescriptionDetail)
        {
            await _context.TemplatePrescriptionDetails.AddAsync(templatePrescriptionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TemplatePrescriptionDetail templatePrescriptionDetail)
        {
            _context.TemplatePrescriptionDetails.Update(templatePrescriptionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TemplatePrescriptionDetail detail)
        {
            if (detail != null)
            {
                _context.TemplatePrescriptionDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }
    }

}
