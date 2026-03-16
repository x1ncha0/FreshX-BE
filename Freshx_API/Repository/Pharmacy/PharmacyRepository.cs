using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private readonly FreshxDBContext _context;

        public PharmacyRepository(FreshxDBContext context)
        {
            _context = context;
        }

        // Lấy tất cả nhà thuốc có áp dụng bộ lọc
        public async Task<IEnumerable<Pharmacy>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            int? inventoryTypeId,
            int? specialtyId)
        {
            var query = _context.Pharmacies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(p => p.Name.Contains(searchKeyword) || p.Code.Contains(searchKeyword));
            }

            if (createdDate.HasValue)
            {
                query = query.Where(p => p.CreatedDate >= createdDate.Value);
            }

            if (updatedDate.HasValue)
            {
                query = query.Where(p => p.UpdatedDate <= updatedDate.Value);
            }

            if (isSuspended.HasValue)
            {
                query = query.Where(p => p.IsSuspended == isSuspended.Value);
            }

            if (inventoryTypeId.HasValue)
            {
                query = query.Where(p => p.InventoryTypeId == inventoryTypeId.Value);
            }


            return await query.ToListAsync();
        }

        // Lấy thông tin nhà thuốc theo ID
        public async Task<Pharmacy?> GetByIdAsync(int id)
        {
            var pharmacy = await _context.Pharmacies
                .Include(p => p.InventoryType)
                .FirstOrDefaultAsync(p => p.PharmacyId == id);

            return pharmacy;
        }

        // Tạo mới nhà thuốc
        public async Task<Pharmacy> CreateAsync(Pharmacy entity)
        {
            var inventoryType = await _context.InventoryTypes
                .FirstOrDefaultAsync(it => it.InventoryTypeId == entity.InventoryTypeId);

            if (inventoryType == null)
            {
                throw new Exception("InventoryType không hợp lệ.");
            }

            _context.Pharmacies.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Cập nhật thông tin nhà thuốc
        public async Task UpdateAsync(Pharmacy entity)
        {
            var inventoryType = await _context.InventoryTypes
                .FirstOrDefaultAsync(it => it.InventoryTypeId == entity.InventoryTypeId);

            if (inventoryType == null)
            {
                throw new Exception("InventoryType không hợp lệ.");
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa mềm nhà thuốc
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Pharmacies.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = 1;
                await _context.SaveChangesAsync();
            }
        }

        // Lấy thông tin Department theo ID
        public async Task<Department?> GetDepartmentByIdAsync(int? departmentId)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        // Lấy thông tin InventoryType theo ID
        public async Task<InventoryType?> GetInventoryTypeByIdAsync(int? inventoryTypeId)
        {
            return await _context.InventoryTypes
                .FirstOrDefaultAsync(it => it.InventoryTypeId == inventoryTypeId);
        }

        public async Task<Pharmacy> GetPharmacyByCodeAsync(string code)
        {
            return await _context.Pharmacies
                .Where(p => p.Code == code && p.IsDeleted == 0)  // Kiểm tra mã và trạng thái không bị xóa
                .FirstOrDefaultAsync();  // Trả về nhà thuốc đầu tiên hoặc null nếu không tìm thấy
        }
    }
}
