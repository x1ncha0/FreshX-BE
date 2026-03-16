using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class ServiceGroupRepository : IServiceGroupRepository
    {
        private readonly FreshxDBContext _context;

        public ServiceGroupRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy tất cả nhóm dịch vụ có áp dụng bộ lọc
        public async Task<IEnumerable<ServiceGroup>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status)
        {
            // Lấy danh sách nhóm dịch vụ chưa bị xóa mềm
            var query = _context.ServiceGroups.Include(r => r.ServiceCatalogs).ThenInclude(s => s.ServiceTypes)
                .Where(sg => sg.IsDeleted == 0 || sg.IsDeleted == null);

            // Áp dụng bộ lọc từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(sg => sg.Name.Contains(searchKeyword) || sg.Code.Contains(searchKeyword));
            }

            // Lọc theo ngày tạo
            if (createdDate.HasValue)
            {
                query = query.Where(sg => sg.CreatedDate >= createdDate.Value);
            }

            // Lọc theo ngày cập nhật
            if (updatedDate.HasValue)
            {
                query = query.Where(sg => sg.UpdatedDate <= updatedDate.Value);
            }

            // Lọc theo trạng thái
            if (status.HasValue)
            {
                query = query.Where(sg => sg.IsSuspended == status.Value);
            }

            return await query.ToListAsync();
        }

        // Lấy nhóm dịch vụ theo ID
        public async Task<ServiceGroup?> GetByIdAsync(int id)
        {
            return await _context.ServiceGroups
                .FirstOrDefaultAsync(sg => sg.ServiceGroupId == id && (sg.IsDeleted == 0 || sg.IsDeleted == null));
        }

        // Tạo mới nhóm dịch vụ
        public async Task<ServiceGroup> CreateAsync(ServiceGroup entity)
        {
            _context.ServiceGroups.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật nhóm dịch vụ
        public async Task UpdateAsync(ServiceGroup entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm nhóm dịch vụ
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ServiceGroups.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }

        // Lấy danh sách ServiceCatalogs theo ID nhóm dịch vụ
        public async Task<IEnumerable<ServiceCatalog>> GetServiceCatalogsByServiceGroupIdAsync(int serviceGroupId)
        {
            return await _context.ServiceCatalogs
                .Where(sc => sc.ServiceGroupId == serviceGroupId && (sc.IsDeleted == 0 || sc.IsDeleted == null))
                .ToListAsync();
        }

        // Tìm nhóm dịch vụ theo tên
        public async Task<ServiceGroup?> GetByNameAsync(string name)
        {
            return await _context.ServiceGroups
                .FirstOrDefaultAsync(sg => sg.Name.ToLower() == name.ToLower() && (sg.IsDeleted == 0 || sg.IsDeleted == null));
        }
    }
}
