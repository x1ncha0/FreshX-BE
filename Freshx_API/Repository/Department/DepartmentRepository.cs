using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly FreshxDBContext _context;

        public DepartmentRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy tất cả phòng ban có áp dụng bộ lọc
        public async Task<IEnumerable<Department>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status)
        {
            // Lấy danh sách phòng ban chưa bị xóa mềm
            var query = _context.Departments
                .Where(d => d.IsDeleted == 0 || d.IsDeleted == null);

            // Áp dụng bộ lọc từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(d => d.Name.Contains(searchKeyword) || d.Code.Contains(searchKeyword));
            }

            // Lọc theo ngày tạo
            if (createdDate.HasValue)
            {
                query = query.Where(d => d.CreatedDate >= createdDate.Value);
            }

            // Lọc theo ngày cập nhật
            if (updatedDate.HasValue)
            {
                query = query.Where(d => d.UpdatedDate <= updatedDate.Value);
            }

            // Lọc theo trạng thái
            if (status.HasValue)
            {
                query = query.Where(d => d.IsSuspended == status.Value);
            }

            // Kiểm tra trạng thái của DepartmentType trước khi trả về kết quả
            var departments = await query.ToListAsync();

            // Lọc các phòng ban bị xóa hoặc tạm ngưng theo trạng thái DepartmentType
            foreach (var department in departments.ToList())
            {
                if (department.DepartmentType != null)
                {
                    if (department.DepartmentType.IsDeleted != 0)
                    {
                        departments.Remove(department); // Xóa phòng ban nếu DepartmentType bị xóa
                    }
                    else if (department.DepartmentType.IsSuspended != 0)
                    {
                        department.Name = department.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên phòng ban
                    }
                }
            }

            return departments;
        }

        // Lấy phòng ban theo ID
        public async Task<Department?> GetByIdAsync(int id)
        {
            var department = await _context.Departments
        .Include(d => d.DepartmentType) // Include DepartmentType để kiểm tra trạng thái
        .FirstOrDefaultAsync(d => d.DepartmentId == id && (d.IsDeleted == 0 || d.IsDeleted == null));

            if (department != null && department.DepartmentType != null)
            {
                // Kiểm tra trạng thái của DepartmentType
                if (department.DepartmentType.IsDeleted != 0)
                {
                    return null; // Trả về null nếu DepartmentType bị xóa
                }
                else if (department.DepartmentType.IsSuspended != 0)
                {
                    department.Name = department.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên phòng ban
                }
            }

            return department;
        }

        // Tạo mới phòng ban
        public async Task<Department> CreateAsync(Department entity)
        {
            // Kiểm tra trạng thái của DepartmentType trước khi tạo mới
            var departmentType = await _context.DepartmentTypes
                .FirstOrDefaultAsync(dt => dt.DepartmentTypeId == entity.DepartmentTypeId);

            if (departmentType == null || departmentType.IsDeleted != 0)
            {
                throw new Exception("DepartmentType không hợp lệ hoặc đã bị xóa.");
            }

            // Nếu DepartmentType bị tạm ngưng
            if (departmentType.IsSuspended != 0)
            {
                entity.Name = entity.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên phòng ban
            }

            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật phòng ban
        public async Task UpdateAsync(Department entity)
        {
            // Kiểm tra trạng thái của DepartmentType trước khi cập nhật
            var departmentType = await _context.DepartmentTypes
                .FirstOrDefaultAsync(dt => dt.DepartmentTypeId == entity.DepartmentTypeId);

            if (departmentType == null || departmentType.IsDeleted != 0)
            {
                throw new Exception("DepartmentType không hợp lệ hoặc đã bị xóa.");
            }

            // Nếu DepartmentType bị tạm ngưng
            if (departmentType.IsSuspended != 0)
            {
                entity.Name = entity.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên phòng ban
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm phòng ban
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DepartmentType?> GetDepartmentTypeByIdAsync(int? departmentTypeId)
        {
            return await _context.DepartmentTypes
                .FirstOrDefaultAsync(dt => dt.DepartmentTypeId == departmentTypeId);
        }
    }
}
