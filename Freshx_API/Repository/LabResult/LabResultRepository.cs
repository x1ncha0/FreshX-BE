using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Freshx_API.Repository.LabResults
{
    public class LabResultRepository : ILabResultRepository
    {
        private readonly FreshxDBContext _context;

        public LabResultRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LabResult>> GetAllAsync(string searchKey = null)
        {
            var query = _context.LabResults.AsQueryable();

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(lr =>
                    lr.Conclusion.Contains(searchKey) ||
                    lr.Description.Contains(searchKey) ||
                    lr.Note.Contains(searchKey));
            }

            return await query.ToListAsync();
        }

        public async Task<LabResult?> GetByIdAsync(int id)
        {
            return await _context.LabResults.FindAsync(id);
        }

        public async Task AddAsync(LabResult labResult)
        {
            await _context.LabResults.AddAsync(labResult);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LabResult labResult)
        {
            _context.LabResults.Update(labResult);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var labResult = await GetByIdAsync(id);
            if (labResult != null)
            {
                labResult.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }
    }

}
