using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly FreshxDBContext _context;

        public DoctorRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy tất cả bác sĩ với các tiêu chí tìm kiếm
        public async Task<IEnumerable<Doctor>> GetAllAsync(
    string? searchKeyword,
    int? isSuspended,
    DateTime? createdDate,
    DateTime? updatedDate,
    string? specialty,   // Thêm tham số chuyên khoa
    string? phone,       // Thêm tham số số điện thoại
    string? email,       // Thêm tham số email
    string? gender)      // Thêm tham số giới tính
        {
            // Lấy danh sách bác sĩ chưa bị xóa mềm
            var query = _context.Doctors
                .Where(d => d.IsDeleted == 0 || d.IsDeleted == null);

            // Nếu có từ khóa tìm kiếm, thêm điều kiện tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(d => d.Name.Contains(searchKeyword));
            }

            // Nếu có khoảng thời gian bắt đầu, thêm điều kiện lọc theo CreatedDate
            if (createdDate.HasValue)
            {
                query = query.Where(d => d.CreatedDate >= createdDate.Value);
            }

            // Nếu có khoảng thời gian kết thúc, thêm điều kiện lọc theo UpdatedDate
            if (updatedDate.HasValue)
            {
                query = query.Where(d => d.UpdatedDate <= updatedDate.Value);
            }

            // Nếu có trạng thái IsSuspended, thêm điều kiện lọc
            if (isSuspended.HasValue)
            {
                query = query.Where(d => d.IsSuspended == isSuspended.Value);
            }

            // Nếu có chuyên khoa, thêm điều kiện lọc theo Specialty
            if (!string.IsNullOrWhiteSpace(specialty))
            {
                query = query.Where(d => d.Specialty.Contains(specialty));
            }

            // Nếu có số điện thoại, thêm điều kiện lọc theo Phone
            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(d => d.Phone.Contains(phone));
            }

            // Nếu có email, thêm điều kiện lọc theo Email
            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(d => d.Email.Contains(email));
            }

            // Nếu có giới tính, thêm điều kiện lọc theo Gender
            if (!string.IsNullOrWhiteSpace(gender))
            {
                query = query.Where(d => d.Gender.Contains(gender));
            }

            // Trả về danh sách sau khi áp dụng các bộ lọc
            return await query.ToListAsync();
        }

        // Lấy thông tin bác sĩ theo ID
        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == id && (d.IsDeleted == 0 || d.IsDeleted == null));
        }

        // Tạo mới bác sĩ
        public async Task<Doctor> CreateAsync(Doctor entity)
        {
            _context.Doctors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật thông tin bác sĩ
        public async Task UpdateAsync(Doctor entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm bác sĩ (đánh dấu IsDeleted = 1)
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Doctors.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }

        // Kiểm tra trạng thái bác sĩ trước khi thực hiện các thao tác
        public async Task<bool> IsDoctorSuspendedAsync(int id)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == id && (d.IsDeleted == 0 || d.IsDeleted == null));

            return doctor != null && doctor.IsSuspended == 1;
        }
    }
}
