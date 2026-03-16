using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        private readonly FreshxDBContext _context;

        public UnitOfMeasureRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy danh sách đơn vị đo lường với bộ lọc
        public async Task<IEnumerable<UnitOfMeasure>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? isSuspended,
            int? isDeleted)
        {
            var query = _context.UnitOfMeasures.AsQueryable();

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(u => u.Name.Contains(searchKeyword) || u.Code.Contains(searchKeyword));
            }

            // Lọc theo ngày tạo
            if (createdDate.HasValue)
            {
                query = query.Where(u => u.CreatedDate >= createdDate.Value);
            }

            // Lọc theo ngày cập nhật
            if (updatedDate.HasValue)
            {
                query = query.Where(u => u.UpdatedDate <= updatedDate.Value);
            }

            // Lọc theo trạng thái tạm ngưng
            if (isSuspended.HasValue)
            {
                query = query.Where(u => u.IsSuspended == isSuspended.Value);
            }

            // Lọc theo trạng thái đã xóa
            if (isDeleted.HasValue)
            {
                query = query.Where(u => u.IsDeleted == isDeleted.Value);
            }

            return await query.ToListAsync();
        }

        // Lấy đơn vị đo lường theo ID
        public async Task<UnitOfMeasure?> GetByIdAsync(int id)
        {
            return await _context.UnitOfMeasures
                .FirstOrDefaultAsync(u => u.UnitOfMeasureId == id && (u.IsDeleted == 0 || u.IsDeleted == null));
        }

        public async Task<UnitOfMeasure?> GetByCodeAsync(string code)
        {
            try
            {
                return await _context.UnitOfMeasures
                    .FirstOrDefaultAsync(s => s.Code == code && (s.IsDeleted == 0 || s.IsDeleted == null));
            }
            catch (Exception ex)
            {
                // Log exception hoặc xử lý thêm
                throw new Exception("Error retrieving UnitOfMeasure", ex);
            }
        }

        public async Task<UnitOfMeasure?> GetNameAsync(string name)
        {
            try
            {
                return await _context.UnitOfMeasures
                    .FirstOrDefaultAsync(s => s.Name == name && (s.IsDeleted == 0 || s.IsDeleted == null));
            }
            catch (Exception ex)
            {
                // Log exception hoặc xử lý thêm
                throw new Exception("Error retrieving UnitOfMeasure", ex);
            }
        }

        // Tạo mới đơn vị đo lường
        public async Task<UnitOfMeasure> CreateAsync(UnitOfMeasure entity)
        {
            _context.UnitOfMeasures.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật đơn vị đo lường
        public async Task UpdateAsync(UnitOfMeasure entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm đơn vị đo lường
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.UnitOfMeasures.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsyncCode(string code)
        {
            // Tìm nhà cung cấp theo code
            var entity = await _context.UnitOfMeasures.FirstOrDefaultAsync(s => s.Code == code && (s.IsDeleted == null || s.IsDeleted == 0));

            if (entity == null)
            {
                // Nếu không tìm thấy, ném ngoại lệ hoặc xử lý theo cách khác
                throw new KeyNotFoundException("Đơn vị đo lường không tồn tại hoặc đã bị xóa.");
            }

            // Đánh dấu là đã xóa (soft delete) thay vì xóa thật
            entity.IsDeleted = 1; // Hoặc bạn có thể set IsDeleted = true tùy thuộc vào kiểu dữ liệu của bạn

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }
    }
}
