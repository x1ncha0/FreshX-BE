using Freshx_API.Models;
namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho DepartmentType
    public interface IDepartmentTypeRepository
    {
        Task<IEnumerable<DepartmentType>> GetAllAsync(string? searchKeyword,
       DateTime? CreatetDate,
  DateTime? UpdatedDate,
      int? status); // Lấy tất cả phòng ban
        Task<DepartmentType?> GetByIdAsync(int id); // Lấy thông tin phòng ban theo ID
        Task<DepartmentType> CreateAsync(DepartmentType entity); // Tạo mới phòng ban
        Task UpdateAsync(DepartmentType entity); // Cập nhật phòng ban
        Task DeleteAsync(int id); // Xóa mềm phòng ban
    }
}
