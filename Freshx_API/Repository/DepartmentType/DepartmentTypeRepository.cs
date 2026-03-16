using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class DepartmentTypeRepository : IDepartmentTypeRepository
    {
        private readonly FreshxDBContext _context;

        public DepartmentTypeRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy tất cả phòng ban lọc
        public async Task<IEnumerable<DepartmentType>> GetAllAsync(
      string? searchKeyword,
       DateTime? CreatetDate,
  DateTime? UpdatedDate,
      int? status)
        {
            // Lấy danh sách phòng ban chưa bị xóa mềm
            var query = _context.DepartmentTypes
                .Where(d => d.IsDeleted == 0 || d.IsDeleted == null);

            // Nếu có từ khóa tìm kiếm, thêm điều kiện tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(d => d.Name.Contains(searchKeyword) || d.Code.Contains(searchKeyword));
            }

            // Nếu có khoảng thời gian bắt đầu, thêm điều kiện lọc theo CreatedDate
            if (CreatetDate.HasValue)
            {
                query = query.Where(d => d.CreatedDate >= UpdatedDate.Value);
            }

            // Nếu có khoảng thời gian kết thúc, thêm điều kiện lọc theo CreatedDate
            if (UpdatedDate.HasValue)
            {
                query = query.Where(d => d.UpdatedDate <= UpdatedDate.Value);
            }

            // Nếu có trạng thái IsSuspended, thêm điều kiện lọc
            if (status.HasValue)
            {
                query = query.Where(d => d.IsSuspended == status.Value);
            }

            // Trả về danh sách sau khi áp dụng các bộ lọc
            return await query.ToListAsync();
        }


        // Lấy phòng ban theo ID (lọc bản ghi chưa bị xóa)
        public async Task<DepartmentType?> GetByIdAsync(int id)
        {
            return await _context.DepartmentTypes
                .FirstOrDefaultAsync(d => d.DepartmentTypeId == id && (d.IsDeleted == 0 || d.IsDeleted == null));
        }

        // Tạo mới phòng ban
        public async Task<DepartmentType> CreateAsync(DepartmentType entity)
        {
            _context.DepartmentTypes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật phòng ban
        public async Task UpdateAsync(DepartmentType entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm phòng ban (đánh dấu IsDeleted = 1)
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DepartmentTypes.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }

    }
}
