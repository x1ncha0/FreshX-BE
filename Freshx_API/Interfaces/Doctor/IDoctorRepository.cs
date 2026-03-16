using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho Doctor
    public interface IDoctorRepository
    {
        // Lấy tất cả bác sĩ với các tiêu chí tìm kiếm
        Task<IEnumerable<Doctor>> GetAllAsync(
            string? searchKeyword,
            int? isSuspended,
            DateTime? createdDate,
            DateTime? updatedDate, string? specialty, string? phone, string? email, string? gender);

        // Lấy thông tin bác sĩ theo ID
        Task<Doctor?> GetByIdAsync(int id);

        // Tạo mới bác sĩ
        Task<Doctor> CreateAsync(Doctor entity);

        // Cập nhật thông tin bác sĩ
        Task UpdateAsync(Doctor entity);

        // Xóa mềm bác sĩ theo ID
        Task DeleteAsync(int id);

        // Kiểm tra trạng thái bác sĩ trước khi thực hiện các thao tác
        Task<bool> IsDoctorSuspendedAsync(int id);

    }
}
