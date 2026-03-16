using Freshx_API.Models;
namespace Freshx_API.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            int? isDeleted
        ); // Lấy danh sách quốc gia với điều kiện lọc

        Task<Country?> GetByIdAsync(int id); // Lấy thông tin quốc gia theo ID

        Task<Country> CreateAsync(Country entity); // Tạo mới quốc gia

        Task UpdateAsync(Country entity); // Cập nhật quốc gia

        Task DeleteAsync(int id); // Xóa mềm quốc gia
    }
}
