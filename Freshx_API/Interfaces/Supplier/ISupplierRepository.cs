using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho Supplier
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            bool? isForeign,
            bool? isStateOwned,
            int? isDeleted
        ); // Lấy danh sách nhà cung cấp với điều kiện lọc

        Task<Supplier?> GetByIdAsync(int id); // Lấy thông tin nhà cung cấp theo ID

        Task<Supplier> CreateAsync(Supplier entity); // Tạo mới nhà cung cấp

        Task UpdateAsync(Supplier entity); // Cập nhật nhà cung cấp

        Task DeleteAsyncId(int id); // Xóa mềm nhà cung cấp

        Task DeleteAsyncCode(string code); // Xóa mềm nhà cung cấp

        Task<Supplier> GetSupplierByCodeAsync(string code);
    }
}
