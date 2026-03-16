using AutoMapper;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Employee;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;
using Freshx_API.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Freshx_API.Repository
{ 
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly FreshxDBContext _context;
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly ITokenRepository _tokenRepository;
        private readonly IFileService _fileService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public EmployeeRepository(FreshxDBContext context,ILogger<EmployeeRepository> logger, ITokenRepository tokenRepository, IFileService fileService, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _tokenRepository = tokenRepository;
            _fileService = fileService;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Employee?> CreateEmployeeAsync(EmployeeRequest request)
        {
            try
            {

                int? avatarId = null;
                var listfiles = new List<IFormFile> { request.AvatarFile };
                if (request.AvatarFile != null)
                {
                    var listFiles = new List<IFormFile> { request.AvatarFile };
                    var avatar = await _fileService.SaveFileAsync(
                        _tokenRepository.GetUserIdFromToken(),
                        "avatar",
                        listFiles);
                    avatarId = avatar.FirstOrDefault()?.Id;
                }
                string? accountId = null;
                // Determine role name
                string? roleName = request.PositionId switch
                {
                    3 => "Tiếp Nhận",
                    4 => "Thu Ngân",
                };
                string? employeeCode = null;
                if(roleName.StartsWith("Tiếp Nhận"))
                {
                    employeeCode = await GenerateEmployeeCode.GenerateCode(_context);
                }
                else
                {
                    employeeCode = await GenerateEmployeeCode.GenerateCode(_context, "NVKT");
                }
                // Load địa chỉ trước khi tạo user
                var ward = await _context.Wards
                    .AsNoTracking()
                    .FirstOrDefaultAsync(w => w.Code == request.WardId);
                var district = await _context.Districts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Code == request.DistrictId);
                var province = await _context.Provinces
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Code == request.ProvinceId);
                string? formattedAddress = $"{ward?.FullName}, {district?.FullName}, {province?.FullName}";
                string passWord = RegisteringCodeGenerating.GenerateSecureCode();
                var appUser = new AppUser
                {
                    FullName = request.Name,
                    Email = request.Email,
                    DateOfBirth = request.DateOfBirth,
                    IsActive = true,
                    WardId = request.WardId,
                    DistrictId = request.DistrictId,
                    ProvinceId = request.ProvinceId,
                    AvatarId = avatarId,
                    CreatedAt = DateTime.UtcNow,
                    UserName = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = formattedAddress,
                    Gender = request.Gender,
                    IdentityCardNumber = request.IdentityCardNumber
                };

                var result = await _userManager.CreateAsync(appUser, passWord);
                //UserName + PassWord nếu tạo tài khoản thành công
                if (!result.Succeeded)
                {
                    return null;
                }
                result = await _userManager.AddToRoleAsync(appUser, roleName);
                if (!result.Succeeded)
                {
                    // Clean up created user
                    await _userManager.DeleteAsync(appUser);
                    return null;
                }
                await _emailService.SendEmailAsync(request.Email, "Tài khoản đăng nhập", $"Email: {request.Email}, Password: {passWord}");
                // Create and save Doctor entity
                var employee = new Employee
                {
                    AccountId = appUser.Id,
                    PositionId = request.PositionId,
                    EmployeeCode = employeeCode,
                    DateOfBirth = request.DateOfBirth,
                    CreatedDate = DateTime.UtcNow,
                    DepartmentId = request.DepartmentId,
                    WardId = request.WardId,
                    DistrictId = request.DistrictId,
                    ProvinceId = request.ProvinceId,
                    CreatedBy = _tokenRepository.GetUserIdFromToken(),
                    Address = formattedAddress,
                    Gender = request.Gender,
                    FullName = request.Name,
                    IsSuspended = 0,
                    IsDeleted = 0,
                    IdentityCardNumber = request.IdentityCardNumber,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    AvataId = avatarId
                };

                //  await _context.Entry(employee).Reference(d => d.Position).LoadAsync();
                //  await _context.Entry(employee).Reference(d => d.Department).LoadAsync();            
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while creating a new employee");
                throw;
            }
        }

        public async Task<Employee?> DeleteEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _context.Employees.Include(d => d.Position).Include(d => d.Department).FirstOrDefaultAsync(d => d.EmployeeId == id);
                if (employee != null)
                {
                    var account = await _userManager.FindByEmailAsync(employee.Email);
                    account.IsActive = false;
                    employee.IsDeleted = 1;
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return employee;
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

        public async Task<List<Employee?>> GetAllEmployeesAsync(Parameters parameters)
        {
            var query = _context.Employees.Include(d => d.Position).Include(d => d.Department).Where(p => p.IsDeleted == 0 && p.FullName != null && p.Address != null).AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(u =>
                    (u.FullName != null && u.FullName.Contains(parameters.SearchTerm)) ||
                    (u.Address != null && u.Address.Contains(parameters.SearchTerm) || (u.EmployeeCode != null && u.EmployeeCode.Contains(parameters.SearchTerm))));
            }

            // Apply sorting
            // Sort by created date
            query = parameters.SortOrderAsc ?? true
               ? query.OrderBy(p => p.CreatedDate)
               : query.OrderByDescending(p => p.CreatedDate);
            return await query.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _context.Employees.Include(d => d.Position).Include(d => d.Department).FirstOrDefaultAsync(d => d.EmployeeId == id);
                if (employee == null || employee.IsDeleted == 1)
                {
                    return null;
                }
                return employee;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while getting employee");
                throw;
            }
        }

        public async Task<Employee?> UpdateEmployeeByIdAsync(int id, EmployeeRequest request)
        {
            try
            { // Start transaction to ensure both employee and account updates succeed or fail together
                using var transaction = await _context.Database.BeginTransactionAsync();

                var employee = await _context.Employees
                    .Include(d => d.AppUser).Include(d => d.Position).Include(d => d.Department) // Include the associated account
                    .FirstOrDefaultAsync(d => d.EmployeeId == id);

                if (employee == null)
                {
                    return null;
                }

                // Update avatar
                int? avatarId = employee.AvataId;
                if (employee.AvataId == null)
                {
                    if (request.AvatarFile != null)
                    {
                        var listFiles = new List<IFormFile> { request.AvatarFile };
                        var avatar = await _fileService.SaveFileAsync(
                            _tokenRepository.GetUserIdFromToken(),
                            "avatar",
                            listFiles
                        );
                        avatarId = avatar[0].Id;
                        employee.AvataId = avatarId;
                        employee.AppUser.AvatarId = avatarId;
                    }
                }
                else if (request.AvatarFile != null)
                {
                    await _fileService.UpdateFileAsync(employee.AvataId, request.AvatarFile);
                }

                // Load địa chỉ trước khi tạo cap nhat
                var ward = await _context.Wards
                    .AsNoTracking()
                    .FirstOrDefaultAsync(w => w.Code == request.WardId);
                var district = await _context.Districts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Code == request.DistrictId);
                var province = await _context.Provinces
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Code == request.ProvinceId);
                string? formattedAddress = $"{ward?.FullName}, {district?.FullName}, {province?.FullName}";

                // Update employee information

                employee.FullName = request.Name;
                employee.PhoneNumber = request.PhoneNumber;
                employee.DateOfBirth = request.DateOfBirth;
                employee.Gender = request.Gender;
                employee.PositionId = request.PositionId;
                employee.DepartmentId = request.DepartmentId;
                employee.IdentityCardNumber = request.IdentityCardNumber;
                employee.WardId = request.WardId;
                employee.DistrictId = request.DistrictId;
                employee.ProvinceId = request.ProvinceId;
                employee.Email = request.Email;
                employee.Address = formattedAddress;
                employee.UpdatedBy = _tokenRepository.GetUserIdFromToken();
                employee.UpdatedDate = DateTime.UtcNow;
                // Determine role name
                string? roleName = request.PositionId switch
                {
                    3 => "Tiếp Nhận",
                    4 => "Thu Ngân",
                };
                string employeeCode;
                if (roleName.StartsWith("Tiếp Nhận"))
                {
                    employeeCode = await GenerateEmployeeCode.GenerateCode(_context);
                }
                else
                {
                    employeeCode = await GenerateEmployeeCode.GenerateCode(_context, "NVKT");
                }
                employee.EmployeeCode = employeeCode;
                // Update account information if it exists
                if (employee.AppUser != null)
                {
                    employee.AppUser.FullName = request.Name;
                    employee.AppUser.PhoneNumber = request.PhoneNumber;
                    employee.AppUser.Gender = request.Gender;
                    employee.AppUser.Email = request.Email;
                    employee.AppUser.UserName = request.Email;
                    employee.AppUser.WardId = request.WardId;
                    employee.AppUser.DistrictId = request.DistrictId;
                    employee.AppUser.ProvinceId = request.ProvinceId;
                    employee.AppUser.IdentityCardNumber = request.IdentityCardNumber;
                    employee.AppUser.DateOfBirth = request.DateOfBirth;
                    employee.AppUser.Address = formattedAddress;
                    employee.AppUser.UpdatedAt = DateTime.UtcNow;
                }             
                //cập nhật lại role mới
                if (!string.IsNullOrEmpty(roleName))
                    {
                        // Get current roles
                        var userRoles = await _userManager.GetRolesAsync(employee.AppUser);

                        // Remove all current roles
                        if (userRoles.Any())
                        {
                            await _userManager.RemoveFromRolesAsync(employee.AppUser, userRoles);
                        }

                        // Add new role
                        await _userManager.AddToRoleAsync(employee.AppUser, roleName);
                    }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return employee;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while updating employee by id: {id}");
                throw;
            }
        }
    }
}
