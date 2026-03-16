using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class ServiceCatalogRepository : IServiceCatalogRepository
    {
        private readonly FreshxDBContext _context;

        public ServiceCatalogRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy tất cả dịch vụ có áp dụng bộ lọc
        public async Task<IEnumerable<ServiceCatalog>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status)
        {
            // Lấy danh sách dịch vụ chưa bị xóa mềm
            var query = _context.ServiceCatalogs.Include(e => e.ServiceTypes)
                .Where(s => s.IsDeleted == 0 || s.IsDeleted == null);

            // Áp dụng bộ lọc từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(s => s.Name.Contains(searchKeyword) || s.Code.Contains(searchKeyword));
            }

            // Lọc theo ngày tạo
            if (createdDate.HasValue)
            {
                query = query.Where(s => s.CreatedDate >= createdDate.Value);
            }

            // Lọc theo ngày cập nhật
            if (updatedDate.HasValue)
            {
                query = query.Where(s => s.UpdatedDate <= updatedDate.Value);
            }

            // Lọc theo trạng thái
            if (status.HasValue)
            {
                query = query.Where(s => s.IsSuspended == status.Value);
            }

            return await query.ToListAsync();
        }

        // Lấy dịch vụ theo ID
        public async Task<ServiceCatalog?> GetByIdAsync(int id)
        {
            var service = await _context.ServiceCatalogs
                .Include(s => s.ServiceTypes)
                .FirstOrDefaultAsync(s => s.ServiceCatalogId == id && (s.IsDeleted == 0 || s.IsDeleted == null));

            if (service != null && service.IsSuspended != 0)
            {
                service.Name += " (Tạm ngưng hoạt động)"; // Thêm thông báo tạm ngưng nếu cần
            }

            return service;
        }

        // Tạo mới dịch vụ
        public async Task<ServiceCatalog> CreateAsync(ServiceCatalog entity)
        {
            // Kiểm tra trạng thái trước khi tạo mới
            if (entity.IsSuspended != 0)
            {
                entity.Name += " (Tạm ngưng hoạt động)";
            }

            _context.ServiceCatalogs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật thông tin dịch vụ
        public async Task UpdateAsync(ServiceCatalog entity)
        {
            // Kiểm tra trạng thái trước khi cập nhật
            if (entity.IsSuspended != 0)
            {
                entity.Name += " (Tạm ngưng hoạt động)";
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm dịch vụ
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ServiceCatalogs.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }

        // Kiểm tra trạng thái hoạt động của dịch vụ theo ID
        public async Task<bool> CheckStatusByIdAsync(int id)
        {
            var service = await _context.ServiceCatalogs
                .FirstOrDefaultAsync(s => s.ServiceCatalogId == id);

            return service != null && service.IsSuspended == 0;
        }
    }
}
