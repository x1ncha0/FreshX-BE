using AutoMapper;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Technician;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;
using Freshx_API.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;

namespace Freshx_API.Repository
{
    public class TechnicianRepository : ITechnicianRepository
    {
        private readonly FreshxDBContext _context;
        private readonly ILogger<TechnicianRepository> _logger;
        private readonly ITokenRepository _tokenRepository;
        private readonly IFileService _fileService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public TechnicianRepository (FreshxDBContext context,ILogger<TechnicianRepository> logger,ITokenRepository tokenRepository,IFileService fileService,UserManager<AppUser> userManager,IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _tokenRepository = tokenRepository;
            _fileService = fileService;
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task<Technician?> CreateTechnicianAsync(TechnicianRequest request)
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
                    2 => "Kỹ Thuật Viên Xét Nghiệm"               
                };
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
                var technician = new Technician
                {
                    AccountId = appUser.Id,
                    PositionId = request.PositionId,                  
                    DateOfBirth = request.DateOfBirth,
                    CreatedDate = DateTime.UtcNow,
                    DepartmentId = request.DepartmentId,
                    WardId = request.WardId,
                    DistrictId = request.DistrictId,
                    ProvinceId = request.ProvinceId,
                    CreatedBy = _tokenRepository.GetUserIdFromToken(),
                    Address = formattedAddress,
                    Gender = request.Gender,
                    Name = request.Name,
                    IsSuspended = 0,
                    IsDeleted = 0,
                    IdentityCardNumber = request.IdentityCardNumber,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    AvataId = avatarId
                };

                //  await _context.Entry(technician).Reference(d => d.Position).LoadAsync();
                //  await _context.Entry(technician).Reference(d => d.Department).LoadAsync();            
                _context.Technicians.Add(technician);
                await _context.SaveChangesAsync();
                return technician;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while creating a new technician");
                throw;
            }
        }

        public async Task<Technician?> DeleteTechnicianByIdAsyn(int id)
        {
            try
            {
                var technician = await _context.Technicians.Include(d => d.Position).Include(d => d.Department).FirstOrDefaultAsync(d => d.TechnicianId == id);
                if (technician != null)
                {
                    var account = await _userManager.FindByEmailAsync(technician.Email);
                    account.IsActive = false;
                    technician.IsDeleted = 1;
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return technician;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while deleting technician by id: {id}");
                throw;
            }
        }

        public async Task<List<Technician?>> GetAllTechnicianAsync(Parameters parameters)
        {
            var query = _context.Technicians.Include(d => d.Position).Include(d => d.Department).Where(p => p.IsDeleted == 0 && p.Name != null && p.Address != null).AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(u =>
                    (u.Name != null && u.Name.Contains(parameters.SearchTerm)) ||
                    (u.Address != null && u.Address.Contains(parameters.SearchTerm)));
            }

            // Apply sorting
            // Sort by created date
            query = parameters.SortOrderAsc ?? true
               ? query.OrderBy(p => p.CreatedDate)
               : query.OrderByDescending(p => p.CreatedDate);
            return await query.ToListAsync();
        }

        public async Task<Technician?> GetTechnicianByIdAsyn(int id)
        {
            try
            {
                var technician = await _context.Technicians.Include(d => d.Position).Include(d => d.Department).FirstOrDefaultAsync(d => d.TechnicianId == id);
                if (technician == null || technician.IsDeleted == 1)
                {
                    return null;
                }
                return technician;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while getting technician");
                throw;
            }
        }

        public async Task<Technician?> UpdateTechnicianByIdAsyn(int id, TechnicianRequest request)
        {
            try
            { // Start transaction to ensure both technician and account updates succeed or fail together
                using var transaction = await _context.Database.BeginTransactionAsync();

                var technician = await _context.Technicians
                    .Include(d => d.AppUser).Include(d => d.Position).Include(d => d.Department) // Include the associated account
                    .FirstOrDefaultAsync(d => d.TechnicianId == id);

                if (technician == null)
                {
                    return null;
                }

                // Update avatar
                int? avatarId = technician.AvataId;
                if (technician.AvataId == null)
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
                        technician.AvataId = avatarId;
                        technician.AppUser.AvatarId = avatarId;
                    }
                }
                else if (request.AvatarFile != null)
                {
                    await _fileService.UpdateFileAsync(technician.AvataId, request.AvatarFile);
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

                // Update technician information

                technician.Name = request.Name;
                technician.PhoneNumber = request.PhoneNumber;
                technician.DateOfBirth = request.DateOfBirth;
                technician.Gender = request.Gender;
                technician.PositionId = request.PositionId;
                technician.DepartmentId = request.DepartmentId;                
                technician.IdentityCardNumber = request.IdentityCardNumber;
                technician.WardId = request.WardId;
                technician.DistrictId = request.DistrictId;
                technician.ProvinceId = request.ProvinceId;
                technician.Email = request.Email;
                technician.Address = formattedAddress;
                technician.UpdatedBy = _tokenRepository.GetUserIdFromToken();
                technician.UpdatedDate = DateTime.UtcNow;

                // Update account information if it exists
                if (technician.AppUser != null)
                {
                    technician.AppUser.FullName = request.Name;
                    technician.AppUser.PhoneNumber = request.PhoneNumber;
                    technician.AppUser.Gender = request.Gender;
                    technician.AppUser.Email = request.Email;
                    technician.AppUser.UserName = request.Email;
                    technician.AppUser.WardId = request.WardId;
                    technician.AppUser.DistrictId = request.DistrictId;
                    technician.AppUser.ProvinceId = request.ProvinceId;
                    technician.AppUser.IdentityCardNumber = request.IdentityCardNumber;
                    technician.AppUser.DateOfBirth = request.DateOfBirth;
                    technician.AppUser.Address = formattedAddress;
                    technician.AppUser.UpdatedAt = DateTime.UtcNow;
                }
                string? roleName = request.PositionId switch
                {
                    2 => "Kỹ Thuật Viên Xét Nghiệm"
                };
                //cập nhật lại role mới
                if (!string.IsNullOrEmpty(roleName))
                {
                    // Get current roles
                    var userRoles = await _userManager.GetRolesAsync(technician.AppUser);

                    // Remove all current roles
                    if (userRoles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(technician.AppUser, userRoles);
                    }

                    // Add new role
                    await _userManager.AddToRoleAsync(technician.AppUser, roleName);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return technician;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while updating technician by id: {id}");
                throw;
            }
        }
    }
}
