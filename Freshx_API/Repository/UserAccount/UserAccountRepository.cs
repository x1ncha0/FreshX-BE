using AutoMapper;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.UserAccount;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Interfaces.UserAccount;
using Freshx_API.Models;
using Freshx_API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Freshx_API.Repository.UserAccount
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UserAccountRepository> _logger;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        private readonly IFileService _fileService;
        public UserAccountRepository(UserManager<AppUser> userManager, ILogger<UserAccountRepository> logger, IMapper mapper, ITokenRepository tokenRepository,IFileService file)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
            _fileService = file;
        }

        public async Task<AppUser?> CreateUserAsync(AddingUserRequest request) 
        {
            try
            {
                string userId = _tokenRepository.GetUserIdFromToken();
                var listfiles = new List<IFormFile> { request.AvatarFile };
                int? avartarId;
                if(request.AvatarFile == null)
                {
                    avartarId = null;
                }
                else
                {
                   var avartar = await _fileService.SaveFileAsync(userId, "avarta", listfiles);
                   avartarId = avartar[0].Id;
                }
      
                var appUser = new AppUser
                {
                    UserName = request.Email,
                    FullName = request.FullName,
                    DateOfBirth = request.DateOfBirth,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                   
                    IdentityCardNumber = request.IdentityCardNumber,
                  //  Age = request.DateOfBirth?.Year == null ? null : (DateTime.Now.Year - request.DateOfBirth.Value.Year),   
                    ProvinceId = request.ProvinceId,
                    WardId = request.WardId,
                    DistrictId = request.DistrictId,
                    AvatarId = avartarId,
                    IsActive = true
                };
                var result = await _userManager.CreateAsync(appUser);
                if(result.Succeeded)
                {
                    // Load related entities
                    return await _userManager.Users                     
                        .Include(u => u.Ward)
                        .Include(u => u.District)
                        .Include(u => u.Province)
                        .FirstOrDefaultAsync(u => u.Id == appUser.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while creating an UserAccount");
                throw;
            }
        }

        public async Task<AppUser?> DeleteUserByIdAsync(string id)
        {
            try
            {
                id = _tokenRepository.GetUserIdFromToken();
                return await _userManager.FindByIdAsync(id);
          
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while");
                throw;
            }

        }
        //
        public async Task<AppUser?> GetUserByIdAsync(string id)
        {
            try
            {
                id = _tokenRepository.GetUserIdFromToken();
                if(id == null)
                {
                    return null;
                }

                return await _userManager.Users
                        .Include(u => u.Ward)
                        .Include(u => u.District)
                        .Include(u => u.Province)
                        .FirstOrDefaultAsync(u => u.Id == id);

            }
            catch(Exception e)
            {
                _logger.LogError(e, $"An exception occured while getting information");
                throw;
            }
        }

        public async Task<CustomPageResponse<IEnumerable<UserResponse?>>> GetUsersAsync(Parameters parameters)
        {
            /*try
            {
                var query = _userManager.Users
                .Include(u => u.Ward)
                .Include(u => u.District)
            //    .Include(u => u.Province).Where(u => u.PositionId != null) // Thêm điều kiện lọc mặc định
                .AsQueryable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                {
                query = query.Where(u =>
                    (u.FullName != null && u.FullName.Contains(parameters.SearchTerm)));

                }

                // Apply sorting
                if (parameters.SortOrderAsc ?? true)
                {
                    query = query.OrderBy(u => u.FullName ?? string.Empty);
                }
                else
                {
                    query = query.OrderByDescending(u => u.FullName ?? string.Empty);
                }

                // Get total count before pagination


                return new CustomPageResponse<IEnumerable<UserResponse?>>(
                    items,
                    parameters.PageNumber,
                    parameters.PageSize,
                    totalRecords);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An excepton occured while getting users");
                throw;
            }         */
            return null;
        }
        public async Task<AppUser?> UpdateUserByIdAsync(string id, UpdatingUserRequest request)
        {
            try
            {
                int? avartarId;
                string userId = _tokenRepository.GetUserIdFromToken();
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return null;
                }
                if(user.AvatarId == null)
                {                
                    var listfiles = new List<IFormFile> { request.AvatarFile };
                   
                    if (request.AvatarFile == null)
                    {
                        avartarId = null;
                    }
                    else
                    {
                        var avartar = await _fileService.SaveFileAsync(userId, "avarta", listfiles);
                        avartarId = avartar[0].Id;
                    }
                    user.FullName = request?.FullName;
                    user.Email = request?.Email;
                  
                    if (request.DateOfBirth != null)
                    {
                        user.DateOfBirth = request.DateOfBirth;
                      //  user.Age = (int)(DateTime.Now.Year - user.DateOfBirth.Value.Year);
                    }
                    else
                    {
                        user.DateOfBirth = null;
                      //  user.Age = null;
                    }
                    user.UpdatedAt = DateTime.UtcNow;
                    user.WardId = request?.WardId;
                    user.DistrictId = request?.DistrictId;
                    user.ProvinceId = request?.ProvinceId;
                   
                    user.PhoneNumber = request?.PhoneNumber;
                    user.IdentityCardNumber = request?.IdentityCardNumber;
                    user.AvatarId = avartarId;
                }
                else
                {
                    await _fileService.UpdateFileAsync(user.AvatarId, request.AvatarFile);
                }    
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return
                        await _userManager.Users
                     .Include(u => u.Ward)
                     .Include(u => u.District)
                     .Include(u => u.Province)
                     .FirstOrDefaultAsync(u => u.Id == userId);
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while updating user account");
                throw;
            }
        }
    }
}
