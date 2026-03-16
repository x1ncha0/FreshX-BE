using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho ServiceGroup
    public interface IServiceGroupRepository
    {
        // Lấy tất cả nhóm dịch vụ với các tiêu chí tìm kiếm
        Task<IEnumerable<ServiceGroup>> GetAllAsync(
            string? searchKeyword,
            DateTime? CreatedDate,
            DateTime? UpdatedDate,
            int? status);

        // Lấy thông tin nhóm dịch vụ theo ID
        Task<ServiceGroup?> GetByIdAsync(int id);

        // Tạo mới nhóm dịch vụ
        Task<ServiceGroup> CreateAsync(ServiceGroup entity);

        // Cập nhật thông tin nhóm dịch vụ
        Task UpdateAsync(ServiceGroup entity);

        // Xóa mềm nhóm dịch vụ theo ID (đánh dấu nhóm dịch vụ là đã xóa mà không thực sự xóa trong cơ sở dữ liệu)
        Task DeleteAsync(int id);

        // Lấy thông tin về các ServiceCatalogs của ServiceGroup (có thể dùng khi cần lấy các dịch vụ con của nhóm)
        Task<IEnumerable<ServiceCatalog>> GetServiceCatalogsByServiceGroupIdAsync(int serviceGroupId);

        // Tìm nhóm dịch vụ theo tên
        Task<ServiceGroup?> GetByNameAsync(string name);
    }
}
