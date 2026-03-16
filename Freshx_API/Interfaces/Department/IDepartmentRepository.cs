using Freshx_API.Models;
namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho Department
    public interface IDepartmentRepository
    {
        // Lấy tất cả phòng ban với các tiêu chí tìm kiếm
        Task<IEnumerable<Department>> GetAllAsync(
            string? searchKeyword,
            DateTime? CreatedDate,
            DateTime? UpdatedDate,
            int? status);

        // Lấy thông tin phòng ban theo ID
        Task<Department?> GetByIdAsync(int id);

        // Tạo mới phòng ban
        Task<Department> CreateAsync(Department entity);

        // Cập nhật thông tin phòng ban
        Task UpdateAsync(Department entity);

        // Xóa mềm phòng ban theo ID (đánh dấu phòng ban là đã xóa mà không thực sự xóa trong cơ sở dữ liệu)
        Task DeleteAsync(int id);

        // Lấy DepartmentType theo ID (giúp kiểm tra trạng thái phòng ban trước khi thực hiện các thao tác)
        Task<DepartmentType?> GetDepartmentTypeByIdAsync(int? departmentTypeId);

        
    }
}
