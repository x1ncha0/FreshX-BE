using Freshx_API.Interfaces.IPrescription;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class PrescriptionDetailRepository : IPrescriptionDetailRepository
    {
        private readonly FreshxDBContext _context;

        public PrescriptionDetailRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<List<PrescriptionDetail>> GetAllAsync(string? searchKey)
        {
             var query = await _context.PrescriptionDetail.ToListAsync();
              if (!string.IsNullOrWhiteSpace(searchKey))
            {
                query = query.Where(p => p.Note.Contains(searchKey)).ToList();
            }
            return query;
        }

        public async Task<PrescriptionDetail?> GetByIdAsync(int id)
        {
            return await _context.PrescriptionDetail.FindAsync(id);
        }
        public async Task<List<PrescriptionDetail?>> GetByPrescriptionIdAsync(int prescriptionId)
        {
            return await _context.PrescriptionDetail
                .Where(d => d.PrescriptionId == prescriptionId)
                .Include(d => d.DrugCatalog)
                .ToListAsync();
        }

        public async Task AddAsync(PrescriptionDetail prescriptionDetail)
        {
            await _context.PrescriptionDetail.AddAsync(prescriptionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PrescriptionDetail prescriptionDetail)
        {
            _context.PrescriptionDetail.Update(prescriptionDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PrescriptionDetail detail)
        {
            if (detail != null)
            {
                _context.PrescriptionDetail.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }
    }

}
