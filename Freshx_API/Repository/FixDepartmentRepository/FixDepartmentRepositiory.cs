using AutoMapper;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DepartmentDtos;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;
using Freshx_API.Services.CommonServices;
using Freshx_API.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Freshx_API.Repository
{
    public class FixDepartmentRepositiory : IFixDepartmentRepository
    {
        private readonly FreshxDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientRepository> _logger;
        private readonly ITokenRepository _tokenRepository;
        public FixDepartmentRepositiory(FreshxDBContext context, IMapper mapper, ILogger<PatientRepository> logger, ITokenRepository tokenRepository)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _tokenRepository = tokenRepository;
        }

        public async Task<Department?> CreateDepartmentAsync(DepartmentCreateUpdateDto request)
        {
            try
            {
                var departmentTypeExists = await _context.DepartmentTypes
                 .AnyAsync(dt => dt.DepartmentTypeId == request.DepartmentTypeId);

                if (!departmentTypeExists)
                {
                    throw new ArgumentException("DepartmentTypeId không hợp lệ.");
                }

                var (code, name) = await GenerateDepartmentCode.GenerateCode(_context, request.DepartmentTypeId);
                var department = new Department
                {
                    Code = code,
                    Name = name,
                    CreatedBy = _tokenRepository.GetUserIdFromToken(),
                    CreatedDate = DateTime.UtcNow,
                    DepartmentTypeId = request.DepartmentTypeId,
                    IsSuspended = 0,
                    IsDeleted  = 0
                };
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Department?> DeleteDepartmentAsync(int id)
        {

            try
            {
                var department = await _context.Departments.Include(d => d.DepartmentType).FirstOrDefaultAsync(d => d.DepartmentId == id);
                if (department != null)
                {
                    department.IsDeleted = 1;
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return department;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while deleting department by id: {id}");
                throw;
            }
        }

        public async Task<List<Department>> GetAllDepartmentsAsync(Parameters parameters)
        {
            try
            {
                var query = _context.Departments.Include(p => p.DepartmentType).Where(p => p.IsDeleted == 0).AsQueryable();

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
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            try
            {
                var department = await _context.Departments.Include(d => d.DepartmentType).FirstOrDefaultAsync(d => d.DepartmentId == id);
                if (department== null || department.IsDeleted == 1)
                {
                    return null;
                }
                return department;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<Department?> UpdateDepartmentAsync(int id, DepartmentCreateUpdateDto request)
        {
            try
            {
                var department = await _context.Departments.Include(d => d.DepartmentType).FirstOrDefaultAsync(d => d.DepartmentId == id);
                if(department == null)
                {
                    return null;
                }
                var (code, name) = await GenerateDepartmentCode.GenerateCode(_context, request.DepartmentTypeId);
                department.DepartmentTypeId = request.DepartmentTypeId;
                department.Name = name;
                department.Code = code;
                department.UpdatedDate = DateTime.UtcNow;
                department.UpdatedBy = _tokenRepository.GetUserIdFromToken();
                department.IsSuspended = request.IsSuspended;
                await _context.SaveChangesAsync();
                return department;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
