
using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    // Interface định nghĩa các phương thức CRUD cho DrugCatalog
    public interface IDrugCatalogRepository
    {
        // Lấy tất cả danh mục thuốc với các tiêu chí tìm kiếm
        Task<IEnumerable<DrugCatalog>> GetAllAsync(
            string? searchKeyword,
            DateTime? startDate,
            DateTime? endDate,
            int? status);

        // Lấy thông tin danh mục thuốc theo ID
        Task<DrugCatalog?> GetByIdAsync(int id);

        // Tạo mới danh mục thuốc
        Task<DrugCatalog> CreateAsync(DrugCatalog entity);

        // Cập nhật thông tin danh mục thuốc
        Task UpdateAsync(DrugCatalog entity);

        // Xóa mềm danh mục thuốc theo ID (đánh dấu danh mục thuốc là đã xóa mà không thực sự xóa trong cơ sở dữ liệu)
        Task DeleteAsync(int id);

        // Lấy thông tin loại thuốc theo ID (giúp kiểm tra thông tin loại thuốc trước khi thực hiện các thao tác)
        Task<DrugType?> GetDrugTypeByIdAsync(int? drugTypeId);

        // Lấy thông tin nhà sản xuất theo ID (giúp kiểm tra thông tin nhà sản xuất trước khi thực hiện các thao tác)
        Task<Supplier?> GetManufacturerByIdAsync(int? manufacturerId);

        // Lấy thông tin đơn vị đo lường theo ID (giúp kiểm tra thông tin đơn vị đo lường trước khi thực hiện các thao tác)
        Task<UnitOfMeasure?> GetUnitOfMeasureByIdAsync(int? unitOfMeasureId);

        // Lấy thông tin quốc gia theo ID (giúp kiểm tra thông tin quốc gia trước khi thực hiện các thao tác)
        Task<Country?> GetCountryByIdAsync(int? countryId);
    }
}