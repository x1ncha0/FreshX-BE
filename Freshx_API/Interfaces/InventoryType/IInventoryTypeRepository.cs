using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    public interface IInventoryTypeRepository
    {
        Task<IEnumerable<InventoryType>> GetAllAsync(string? searchKeyword = null); // Lấy tất cả loại tồn kho, có thể tìm kiếm theo từ khóa
        Task<InventoryType?> GetByIdAsync(int id); // Lấy loại tồn kho theo ID
        Task<InventoryType> CreateAsync(InventoryType entity); // Tạo mới loại tồn kho
        Task<bool> UpdateAsync(InventoryType entity); // Cập nhật thông tin loại tồn kho, trả về true/false
        Task<bool> DeleteAsync(int id); // Xóa loại tồn kho, trả về true/false

        Task<InventoryType?> GetNameAsync(string name);

        Task<InventoryType?> GetByCodeAsync(string code);

        Task DeleteAsyncCode(string code); // Xóa mềm nhà cung cấp
    }
}
