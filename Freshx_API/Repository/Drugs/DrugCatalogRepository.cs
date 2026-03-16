using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository.Drugs
{
    public class DrugCatalogRepository : IDrugCatalogRepository
    {
        private readonly FreshxDBContext _context;

        public DrugCatalogRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy tất cả danh mục thuốc với bộ lọc
        public async Task<IEnumerable<DrugCatalog>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status)
        {
            // Lấy danh sách danh mục thuốc chưa bị xóa mềm
            var query = _context.DrugCatalogs
                .Where(dc => dc.IsDeleted == 0 || dc.IsDeleted == null);

            // Áp dụng bộ lọc từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(dc => dc.Name.Contains(searchKeyword) || dc.Code.Contains(searchKeyword));
            }

            // Lọc theo ngày tạo
            if (createdDate.HasValue)
            {
                query = query.Where(dc => dc.CreatedDate >= createdDate.Value);
            }

            // Lọc theo ngày cập nhật
            if (updatedDate.HasValue)
            {
                query = query.Where(dc => dc.UpdatedDate <= updatedDate.Value);
            }

            // Lọc theo trạng thái
            if (status.HasValue)
            {
                query = query.Where(dc => dc.IsSuspended == status.Value);
            }

            var drugCatalogs = await query.ToListAsync();

            // Lọc các danh mục thuốc bị xóa hoặc tạm ngưng
            foreach (var drugCatalog in drugCatalogs.ToList())
            {
                if (drugCatalog.IsSuspended != 0)
                {
                    drugCatalog.Name = drugCatalog.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên danh mục thuốc
                }
            }

            return drugCatalogs;
        }

        // Lấy danh mục thuốc theo ID
        public async Task<DrugCatalog?> GetByIdAsync(int id)
        {
            var drugCatalog = await _context.DrugCatalogs
                .FirstOrDefaultAsync(dc => dc.DrugCatalogId == id && (dc.IsDeleted == 0 || dc.IsDeleted == null));

            if (drugCatalog != null && drugCatalog.IsSuspended != 0)
            {
                drugCatalog.Name = drugCatalog.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên danh mục thuốc
            }

            return drugCatalog;
        }

        // Tạo mới danh mục thuốc
        public async Task<DrugCatalog> CreateAsync(DrugCatalog entity)
        {
            // Kiểm tra trạng thái trước khi tạo mới
            if (entity.IsSuspended != 0)
            {
                entity.Name = entity.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên danh mục thuốc
            }

            _context.DrugCatalogs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật danh mục thuốc
        public async Task UpdateAsync(DrugCatalog entity)
        {
            // Kiểm tra trạng thái trước khi cập nhật
            if (entity.IsSuspended != 0)
            {
                entity.Name = entity.Name + " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng vào tên danh mục thuốc
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm danh mục thuốc
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DrugCatalogs.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1; // Đánh dấu là đã bị xóa
                await _context.SaveChangesAsync();
            }
        }

        // Lấy danh mục thuốc theo ID
        public async Task<DrugCatalog?> GetDrugCatalogByIdAsync(int? drugCatalogId)
        {
            return await _context.DrugCatalogs
                .FirstOrDefaultAsync(dc => dc.DrugCatalogId == drugCatalogId);
        }

        // Lấy thông tin loại thuốc theo ID
        public async Task<DrugType?> GetDrugTypeByIdAsync(int? drugTypeId)
        {
            if (drugTypeId == null)
                return null;

            return await _context.DrugTypes
                .FirstOrDefaultAsync(dt => dt.DrugTypeId == drugTypeId);
        }

        // Lấy nhà sản xuất (nhà cung cấp) theo ID
        public async Task<Supplier?> GetManufacturerByIdAsync(int? manufacturerId)
        {
            if (manufacturerId == null)
                return null;

            return await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == manufacturerId);
        }

        // Lấy đơn vị đo lường theo ID
        public async Task<UnitOfMeasure?> GetUnitOfMeasureByIdAsync(int? unitOfMeasureId)
        {
            if (unitOfMeasureId == null)
                return null;

            return await _context.UnitOfMeasures
                .FirstOrDefaultAsync(u => u.UnitOfMeasureId == unitOfMeasureId);
        }

        // Lấy quốc gia theo ID
        public async Task<Country?> GetCountryByIdAsync(int? countryId)
        {
            if (countryId == null)
                return null;

            return await _context.Countries
                .FirstOrDefaultAsync(c => c.CountryId == countryId);
        }
    }
}
