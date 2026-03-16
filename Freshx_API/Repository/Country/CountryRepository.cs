using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly FreshxDBContext _context;

        public CountryRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy danh sách quốc gia với bộ lọc
        public async Task<IEnumerable<Country>> GetAllAsync(
            string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, bool? isSuspended, int? isDeleted)
        {
            var query = _context.Countries.AsQueryable();

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(c => c.Name.Contains(searchKeyword) || c.Code.Contains(searchKeyword));
            }

            // Lọc theo ngày tạo
            if (createdDate.HasValue)
            {
                query = query.Where(c => c.CreatedDate >= createdDate.Value);
            }

            // Lọc theo ngày cập nhật
            if (updatedDate.HasValue)
            {
                query = query.Where(c => c.UpdatedDate <= updatedDate.Value);
            }


            // Lọc theo trạng thái đã xóa
            if (isDeleted.HasValue)
            {
                query = query.Where(c => c.IsDeleted == isDeleted.Value);
            }

            return await query.ToListAsync();
        }

        // Lấy quốc gia theo ID
        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _context.Countries
                .FirstOrDefaultAsync(c => c.CountryId == id && (c.IsDeleted == 0 || c.IsDeleted == null));
        }

        // Tạo mới quốc gia
        public async Task<Country> CreateAsync(Country entity)
        {
            _context.Countries.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật quốc gia
        public async Task UpdateAsync(Country entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm quốc gia
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Countries.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }

    }
}