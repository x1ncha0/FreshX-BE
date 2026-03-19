using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class DepartmentRepository(FreshXDbContext context) : IDepartmentRepository
    {
        public async Task<IEnumerable<Department>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? status)
        {
            var query = context.Departments
                .AsNoTracking()
                .Include(department => department.DepartmentType)
                .Where(department => !department.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(department =>
                    (department.Name != null && department.Name.Contains(searchKeyword)) ||
                    (department.Code != null && department.Code.Contains(searchKeyword)));
            }

            if (createdDate.HasValue)
            {
                query = query.Where(department => department.CreatedAt >= createdDate.Value);
            }

            if (updatedDate.HasValue)
            {
                query = query.Where(department => department.UpdatedAt <= updatedDate.Value);
            }

            if (status.HasValue)
            {
                var isSuspended = status.Value == 1;
                query = query.Where(department => department.IsSuspended == isSuspended);
            }

            return await query
                .OrderBy(department => department.CreatedAt)
                .ToListAsync();
        }

        public Task<Department?> GetByIdAsync(int id)
        {
            return context.Departments
                .AsNoTracking()
                .Include(department => department.DepartmentType)
                .FirstOrDefaultAsync(department => department.Id == id && !department.IsDeleted);
        }

        public async Task<Department> CreateAsync(Department entity)
        {
            var departmentType = await GetDepartmentTypeByIdAsync(entity.DepartmentTypeId)
                ?? throw new InvalidOperationException("Loại phòng ban không hợp lệ.");

            var (code, name) = await GenerateDepartmentIdentityAsync(departmentType.Id, departmentType.Code, departmentType.Name, null);
            entity.Code = code;
            entity.Name = name;
            entity.IsDeleted = false;
            entity.IsSuspended = entity.IsSuspended;

            await context.Departments.AddAsync(entity);
            await context.SaveChangesAsync();

            await context.Entry(entity).Reference(department => department.DepartmentType).LoadAsync();
            return entity;
        }

        public async Task UpdateAsync(Department entity)
        {
            var departmentType = await GetDepartmentTypeByIdAsync(entity.DepartmentTypeId)
                ?? throw new InvalidOperationException("Loại phòng ban không hợp lệ.");

            var (code, name) = await GenerateDepartmentIdentityAsync(departmentType.Id, departmentType.Code, departmentType.Name, entity.Id);
            entity.Code = code;
            entity.Name = name;

            context.Departments.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.Departments.FirstOrDefaultAsync(department => department.Id == id && !department.IsDeleted)
                ?? throw new KeyNotFoundException("Phòng ban không tồn tại.");

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public Task<DepartmentType?> GetDepartmentTypeByIdAsync(int? departmentTypeId)
        {
            return context.DepartmentTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(departmentType => departmentType.Id == departmentTypeId && !departmentType.IsDeleted);
        }

        private async Task<(string code, string name)> GenerateDepartmentIdentityAsync(
            int departmentTypeId,
            string? departmentTypeCode,
            string? departmentTypeName,
            int? currentDepartmentId)
        {
            var prefix = string.IsNullOrWhiteSpace(departmentTypeCode) ? "XX" : departmentTypeCode.Trim();

            var existingCodes = await context.Departments
                .AsNoTracking()
                .Where(department =>
                    department.DepartmentTypeId == departmentTypeId &&
                    !department.IsDeleted &&
                    department.Id != currentDepartmentId &&
                    department.Code != null &&
                    department.Code.StartsWith(prefix))
                .Select(department => department.Code!)
                .ToListAsync();

            var nextNumber = existingCodes
                .Select(code => code[prefix.Length..])
                .Select(numberPart => int.TryParse(numberPart, out var number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max() + 1;

            var code = $"{prefix}{nextNumber:D2}";
            var namePrefix = string.IsNullOrWhiteSpace(departmentTypeName) ? "Department" : departmentTypeName.Trim();
            var name = $"{namePrefix} {nextNumber:D2}";

            return (code, name);
        }
    }
}
