using AutoMapper;
using Freshx_API.Dtos;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Patient;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;
using Freshx_API.Services.CommonServices;
using Freshx_API.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class FixDoctorRepository : IFixDoctorRepository
    {
        private readonly FreshxDBContext _context;
        private readonly ILogger<PatientRepository> _logger;
        private readonly ITokenRepository _tokenRepository;
        private readonly IFileService _fileService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public FixDoctorRepository(FreshxDBContext context, IMapper mapper, ILogger<PatientRepository> logger, ITokenRepository tokenRepository, IFileService fileService,UserManager<AppUser> userManager,IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _tokenRepository = tokenRepository;
            _fileService = fileService;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Doctor?> CreateDoctorAsync(DoctorCreateUpdateDto request)
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
                    1 => "Bác Sĩ Phòng Khám",
                    5 => "Bác Sĩ Siêu Âm",                  
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
                var doctor = new Doctor
                {
                    AccountId = appUser.Id,
                    PositionId = request.PositionId,
                    Specialty = request.Specialty,
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
                    Phone = request.PhoneNumber,
                    Email = request.Email,
                    AvataId = avatarId
                };
           
              //  await _context.Entry(doctor).Reference(d => d.Position).LoadAsync();
              //  await _context.Entry(doctor).Reference(d => d.Department).LoadAsync();            
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
                return doctor;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while creating a new doctor");
                throw;
            }
        }

        public async Task<Doctor?> DeleteDoctorByIdAsync(int id)
        {
            try
            {
                var doctor = await _context.Doctors.Include(d => d.Position).Include(d => d.Department).FirstOrDefaultAsync(d => d.DoctorId == id);
                if (doctor != null)
                {
                    var account = await _userManager.FindByEmailAsync(doctor.Email);
                    account.IsActive = false;                 
                    doctor.IsDeleted = 1;
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return doctor;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while deleting doctor by id: {id}");
                throw;
            }
        }

        public async Task<Doctor?> GetDoctorByIdAsycn(int id)
        {
            try
            {
                var doctor = await _context.Doctors.Include(d => d.Position).Include(d => d.Department).FirstOrDefaultAsync(d => d.DoctorId == id);
                if (doctor == null || doctor.IsDeleted == 1)
                {                   
                    return null;
                }             
                return doctor;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while getting products");
                throw;
            }
        }

        public async Task<List<Doctor?>> GetDoctorsAsync(Parameters parameters)
        {
            var query = _context.Doctors. Include(d => d.Position).Include(d => d.Department).Where(p => p.IsDeleted == 0 && p.Name!=null && p.Address!=null).AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(u =>
                    (u.Name != null && u.Name.Contains(parameters.SearchTerm)) ||
                    (u.Address != null && u.Address.Contains(parameters.SearchTerm) || (u.Specialty !=null && u.Specialty.Contains(parameters.SearchTerm))));
            }

            // Apply sorting
            // Sort by created date
            query = parameters.SortOrderAsc ?? true
               ? query.OrderBy(p => p.CreatedDate)
               : query.OrderByDescending(p => p.CreatedDate);
            return await query.ToListAsync();
        }

        public async Task<Doctor?> UpdateDoctorByIdAsync(int id, DoctorCreateUpdateDto request)
        {
            try
            { // Start transaction to ensure both doctor and account updates succeed or fail together
                using var transaction = await _context.Database.BeginTransactionAsync();

                var doctor = await _context.Doctors
                    .Include(d => d.AppUser).Include(d => d.Position).Include(d => d.Department) // Include the associated account
                    .FirstOrDefaultAsync(d => d.DoctorId == id);

                if (doctor == null)
                {
                    return null;
                }

                // Update avatar
                int? avatarId = doctor.AvataId;
                if (doctor.AvataId == null)
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
                        doctor.AvataId = avatarId;
                        doctor.AppUser.AvatarId = avatarId;
                    }
                }
                else if (request.AvatarFile != null)
                {
                    await _fileService.UpdateFileAsync(doctor.AvataId, request.AvatarFile);
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

                // Update doctor information

                doctor.Name = request.Name;
                doctor.Phone = request.PhoneNumber;
                doctor.DateOfBirth = request.DateOfBirth;
                doctor.Gender = request.Gender;
                doctor.PositionId = request.PositionId;
                doctor.DepartmentId = request.DepartmentId;
                doctor.Specialty = request.Specialty;
                doctor.IdentityCardNumber = request.IdentityCardNumber;
                doctor.WardId = request.WardId;
                doctor.DistrictId = request.DistrictId;
                doctor.ProvinceId = request.ProvinceId;
                doctor.Email = request.Email;
                doctor.Address = formattedAddress;
                doctor.UpdatedBy = _tokenRepository.GetUserIdFromToken();
                doctor.UpdatedDate = DateTime.UtcNow;

                // Update account information if it exists
                if (doctor.AppUser != null)
                {
                    doctor.AppUser.FullName = request.Name;
                    doctor.AppUser.PhoneNumber = request.PhoneNumber;
                    doctor.AppUser.Gender = request.Gender;
                    doctor.AppUser.Email = request.Email;
                    doctor.AppUser.UserName = request.Email;
                    doctor.AppUser.WardId = request.WardId;
                    doctor.AppUser.DistrictId = request.DistrictId;
                    doctor.AppUser.ProvinceId = request.ProvinceId;
                    doctor.AppUser.IdentityCardNumber = request.IdentityCardNumber;
                    doctor.AppUser.DateOfBirth = request.DateOfBirth;
                    doctor.AppUser.Address = formattedAddress;       
                    doctor.AppUser.UpdatedAt = DateTime.UtcNow;
                }
                string? roleName = request.PositionId switch
                {
                    1 => "Bác Sĩ Phòng Khám",
                    5 => "Bác Sĩ Siêu Âm",
                };
                //cập nhật lại role mới
                if (!string.IsNullOrEmpty(roleName))
                {
                    // Get current roles
                    var userRoles = await _userManager.GetRolesAsync(doctor.AppUser);

                    // Remove all current roles
                    if (userRoles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(doctor.AppUser, userRoles);
                    }

                    // Add new role
                    await _userManager.AddToRoleAsync(doctor.AppUser, roleName);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return doctor;           
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while updating doctor by id: {id}");
                throw;
            }
        }
    }
}
