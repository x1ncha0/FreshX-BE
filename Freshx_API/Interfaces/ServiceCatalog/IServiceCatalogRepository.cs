using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho ServiceCatalog
    public interface IServiceCatalogRepository
    {
        // Lấy tất cả dịch vụ với các tiêu chí tìm kiếm
        Task<IEnumerable<ServiceCatalog>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status);

        // Lấy thông tin dịch vụ theo ID
        Task<ServiceCatalog?> GetByIdAsync(int id);

        // Tạo mới dịch vụ
        Task<ServiceCatalog> CreateAsync(ServiceCatalog entity);

        // Cập nhật thông tin dịch vụ
        Task UpdateAsync(ServiceCatalog entity);

        // Xóa mềm dịch vụ theo ID (đánh dấu dịch vụ là đã xóa mà không thực sự xóa trong cơ sở dữ liệu)
        Task DeleteAsync(int id);

        // Kiểm tra trạng thái dịch vụ theo ID (giúp xác nhận trước khi thực hiện thao tác)
        Task<bool> CheckStatusByIdAsync(int id);
    }
}
