using Freshx_API.Models;
namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho UnitOfMeasure
    public interface IUnitOfMeasureRepository
    {
        Task<IEnumerable<UnitOfMeasure>> GetAllAsync(
            string? searchKeyword, // Từ khóa tìm kiếm
            DateTime? createdDate, // Ngày tạo
            DateTime? updatedDate, // Ngày cập nhật
            int? isSuspended, // Trạng thái tạm ngưng
            int? isDeleted // Trạng thái đã xóa
        ); // Lấy danh sách đơn vị đo lường với điều kiện lọc

        Task<UnitOfMeasure?> GetByIdAsync(int id); // Lấy thông tin đơn vị đo lường theo ID

        Task<UnitOfMeasure?> GetByCodeAsync(string code);

        Task<UnitOfMeasure?> GetNameAsync(string name);

        Task<UnitOfMeasure> CreateAsync(UnitOfMeasure entity); // Tạo mới đơn vị đo lường

        Task UpdateAsync(UnitOfMeasure entity); // Cập nhật đơn vị đo lường

        Task DeleteAsync(int id); // Xóa mềm đơn vị đo lường

        Task DeleteAsyncCode(string code); // Xóa mềm nhà cung cấp
    }
}
