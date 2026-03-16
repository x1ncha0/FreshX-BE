using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho Pharmacy
    public interface IPharmacyRepository
    {
        // Lấy tất cả nhà thuốc với các tiêu chí tìm kiếm
        Task<IEnumerable<Pharmacy>> GetAllAsync(
            string? searchKeyword,
            DateTime? CreatedDate,
            DateTime? UpdatedDate,
            bool? isSuspended,
            int? inventoryTypeId,
            int? specialtyId);

        // Lấy thông tin nhà thuốc theo ID
        Task<Pharmacy?> GetByIdAsync(int id);

        // Tạo mới nhà thuốc
        Task<Pharmacy> CreateAsync(Pharmacy entity);

        // Cập nhật thông tin nhà thuốc
        Task UpdateAsync(Pharmacy entity);

        // Xóa mềm nhà thuốc theo ID (đánh dấu nhà thuốc là đã xóa mà không thực sự xóa trong cơ sở dữ liệu)
        Task DeleteAsync(int id);

        // Lấy thông tin Department theo ID (để kiểm tra trạng thái phòng ban liên quan trước khi thực hiện các thao tác)
        Task<Department?> GetDepartmentByIdAsync(int? departmentId);

        // Lấy thông tin InventoryType theo ID
        Task<InventoryType?> GetInventoryTypeByIdAsync(int? inventoryTypeId);

        Task<Pharmacy> GetPharmacyByCodeAsync(string code);
        
    }
}
