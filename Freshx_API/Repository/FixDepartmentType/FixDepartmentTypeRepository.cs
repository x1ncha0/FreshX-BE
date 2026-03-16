using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DepartmenTypeDtos;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;
using Freshx_API.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class FixDepartmentTypeRepository : IFixDepartmentTypeRepository
    {
        private readonly FreshxDBContext _context;
        private readonly ILogger<FixDepartmentTypeRepository> _logger;
        private readonly ITokenRepository _tokenRepository;  
        public FixDepartmentTypeRepository(FreshxDBContext context, ILogger<FixDepartmentTypeRepository> logger, ITokenRepository tokenRepository)
        {
            _context = context;
            _logger = logger;
            _tokenRepository = tokenRepository;
        }

        public async Task<DepartmentType?> CreateDepartmentTypeAsync(DepartmentTypeCreateUpdateDto request)
        {
            try
            {
                var normalizedName = request.Name.ToLower();
                var isNameExists = await _context.DepartmentTypes
                    .AnyAsync(d => string.Equals(d.Name.ToLower(), normalizedName));
                if (isNameExists)
                {
                    return null;
                }
                var departmentType = new DepartmentType
                {
                    Name = request.Name,
                    CreatedBy = _tokenRepository.GetUserIdFromToken(),
                    CreatedDate = DateTime.UtcNow,
                    IsSuspended = 0,
                    IsDeleted = 0,
                    Code = DepartmentTypeCodeGenerator.GenerateCode(_context, request.Name)
                };
                await _context.DepartmentTypes.AddAsync(departmentType);
                await _context.SaveChangesAsync();
                return departmentType;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<DepartmentType?> DeleteDepartmentTypeByIdAsync(int id)
        {
            try
            {
                var departmentType = await _context.DepartmentTypes.FindAsync(id);
                if (departmentType != null)
                {
                    departmentType.IsDeleted = 1;
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return departmentType;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while deleting product by id: {id}");
                throw;
            }
        }

        public async Task<List<DepartmentType>> GetAllDepartmentTypeAsync(Parameters parameters)
        {
            try
            {
                var query = _context.DepartmentTypes.Where(p => p.IsDeleted == 0).AsQueryable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                {
                    query = query.Where(u =>
                        (u.Name != null && u.Name.Contains(parameters.SearchTerm)) ||
                        (u.Code != null && u.Code.Contains(parameters.SearchTerm)));
                }

                // Apply sorting
                // Sort by created date
                query = parameters.SortOrderAsc ?? true
                   ? query.OrderBy(p => p.CreatedDate)
                   : query.OrderByDescending(p => p.CreatedDate);
                return await query.ToListAsync();

            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<DepartmentType?> GetDepartmentTypeByIdAsync(int id)
        {
            try
            {
                var departmentTypes = await _context.DepartmentTypes.FindAsync(id);
                if (departmentTypes == null || departmentTypes.IsDeleted == 1)
                {
                    return null;
                }
                return departmentTypes;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw; 
            }
        }

        public async Task<DepartmentType?> UpdateDepartmentTypeByIdAsync(int id, DepartmentTypeCreateUpdateDto request)
        {
            try
            {
                var normalizedName = request.Name.ToLower();
                var existingDepartment = await _context.DepartmentTypes
      .FirstOrDefaultAsync(d => d.DepartmentTypeId != id &&
                               d.Name.ToLower() == normalizedName);
                if (existingDepartment!=null)
                {                 
                    return null;
                }
                var departmentType = await _context.DepartmentTypes.FindAsync(id);
                if(departmentType == null)
                {
                    return null;
                }

                departmentType.UpdatedDate = DateTime.Now;
                departmentType.UpdatedBy = _tokenRepository.GetUserIdFromToken();
                departmentType.Code = DepartmentTypeCodeGenerator.GenerateCode(_context, request.Name);
                departmentType.Name = request.Name;
                departmentType.IsSuspended = request.IsSuspended;
                await _context.SaveChangesAsync();
                return departmentType;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
