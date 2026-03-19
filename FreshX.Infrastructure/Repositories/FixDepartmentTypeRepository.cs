using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.DepartmenTypeDtos;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace FreshX.Infrastructure.Repositories
{
    public class FixDepartmentTypeRepository(
        FreshXDbContext context,
        ILogger<FixDepartmentTypeRepository> logger,
        ITokenRepository tokenRepository) : IFixDepartmentTypeRepository
    {
        public async Task<DepartmentType?> CreateDepartmentTypeAsync(DepartmentTypeCreateUpdateDto request)
        {
            try
            {
                var normalizedName = NormalizeName(request.Name);
                var isNameExists = await context.DepartmentTypes
                    .AnyAsync(d => !d.IsDeleted && d.Name != null && d.Name.ToUpper() == normalizedName);
                if (isNameExists)
                {
                    return null;
                }

                var departmentType = new DepartmentType
                {
                    Name = request.Name,
                    CreatedBy = tokenRepository.GetUserIdFromToken(),
                    CreatedAt = DateTime.UtcNow,
                    IsSuspended = (request.IsSuspended ?? 0) == 1,
                    IsDeleted = false,
                    Code = await GenerateCodeAsync(request.Name)
                };

                await context.DepartmentTypes.AddAsync(departmentType);
                await context.SaveChangesAsync();
                return departmentType;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to create department type");
                throw;
            }
        }

        public async Task<DepartmentType?> UpdateDepartmentTypeByIdAsync(int id, DepartmentTypeCreateUpdateDto request)
        {
            try
            {
                var departmentType = await context.DepartmentTypes.FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
                if (departmentType is null)
                {
                    return null;
                }

                var normalizedName = NormalizeName(request.Name);
                var existingDepartment = await context.DepartmentTypes
                    .FirstOrDefaultAsync(d => d.Id != id && !d.IsDeleted && d.Name != null && d.Name.ToUpper() == normalizedName);
                if (existingDepartment is not null)
                {
                    return null;
                }

                departmentType.UpdatedAt = DateTime.UtcNow;
                departmentType.UpdatedBy = tokenRepository.GetUserIdFromToken();
                departmentType.Code = await GenerateCodeAsync(request.Name, id);
                departmentType.Name = request.Name;
                departmentType.IsSuspended = (request.IsSuspended ?? 0) == 1;

                await context.SaveChangesAsync();
                return departmentType;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to update department type {DepartmentTypeId}", id);
                throw;
            }
        }

        public async Task<List<DepartmentType>> GetAllDepartmentTypeAsync(Parameters parameters)
        {
            try
            {
                var query = context.DepartmentTypes
                    .AsNoTracking()
                    .Where(d => !d.IsDeleted);

                if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                {
                    query = query.Where(d =>
                        (d.Name != null && d.Name.Contains(parameters.SearchTerm)) ||
                        (d.Code != null && d.Code.Contains(parameters.SearchTerm)));
                }

                query = parameters.SortOrderAsc ?? true
                    ? query.OrderBy(d => d.CreatedAt)
                    : query.OrderByDescending(d => d.CreatedAt);

                return await query.ToListAsync();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to read department types");
                throw;
            }
        }

        public Task<DepartmentType?> GetDepartmentTypeByIdAsync(int id)
        {
            return context.DepartmentTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<DepartmentType?> DeleteDepartmentTypeByIdAsync(int id)
        {
            try
            {
                var departmentType = await context.DepartmentTypes.FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
                if (departmentType is null)
                {
                    return null;
                }

                departmentType.IsDeleted = true;
                departmentType.UpdatedAt = DateTime.UtcNow;
                departmentType.UpdatedBy = tokenRepository.GetUserIdFromToken();

                var result = await context.SaveChangesAsync();
                return result > 0 ? departmentType : null;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to delete department type {DepartmentTypeId}", id);
                throw;
            }
        }

        private async Task<string> GenerateCodeAsync(string? name, int? currentId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên không được để trống", nameof(name));
            }

            var normalized = RemoveVietnameseTone(name.ToUpperInvariant());
            var words = normalized.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            var baseCode = GenerateBaseCode(words);
            var finalCode = baseCode;
            var counter = 1;

            while (await context.DepartmentTypes.AnyAsync(x => !x.IsDeleted && x.Code == finalCode && x.Id != currentId))
            {
                finalCode = $"{baseCode}{counter}";
                counter++;
            }

            return finalCode;
        }

        private static string GenerateBaseCode(string[] words)
        {
            if (words.Length == 0)
            {
                return "XX";
            }

            if (words.Length == 1)
            {
                return words[0].Length >= 2 ? words[0][..2] : $"{words[0]}X";
            }

            var code = string.Join("", words.Select(word => word[0]));
            return code.Length > 4 ? code[..4] : code;
        }

        private static string NormalizeName(string? name) => (name ?? string.Empty).Trim().ToUpperInvariant();

        private static string RemoveVietnameseTone(string text)
        {
            var result = text;
            result = Regex.Replace(result, "[áàảãạăắằẳẵặâấầẩẫậ]", "a");
            result = Regex.Replace(result, "[éèẻẽẹêếềểễệ]", "e");
            result = Regex.Replace(result, "[íìỉĩị]", "i");
            result = Regex.Replace(result, "[óòỏõọôốồổỗộơớờởỡợ]", "o");
            result = Regex.Replace(result, "[úùủũụưứừửữự]", "u");
            result = Regex.Replace(result, "[ýỳỷỹỵ]", "y");
            result = Regex.Replace(result, "[đ]", "d");
            return result;
        }
    }
}
