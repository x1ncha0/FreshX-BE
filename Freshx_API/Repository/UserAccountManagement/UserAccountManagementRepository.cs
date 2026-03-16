using Freshx_API.Dtos;
using Freshx_API.Dtos.Patient;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Freshx_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository
{
    public class UserAccountManagementRepository : IUserAccountManagementRepository
    {
        private readonly FreshxDBContext _context;
        private readonly ILogger<UserAccountManagementRepository> _logger;
        private readonly IFileService _fileService;
        public UserAccountManagementRepository(FreshxDBContext context,ILogger<UserAccountManagementRepository> logger,IFileService fileService)
        {
           _context = context;
            _logger = logger;
            _fileService = fileService;
        }
        public async Task<AppUser?> GetInformationAccoutUserById(string id)
        {
            try
            {
                var account = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if(account == null)
                {
                    return null;
                }
                return account;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<AppUser?> UpdateInformationAccountUserById(string id, UserAccountRequest request)
        {
            try
            {
                var account = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (account == null)
                {
                    return null;
                }
                else
                {
                    int? avartarId;
                    if (account.AvatarId == null)
                    {
                        var listFiles = new List<IFormFile> { request.AvatarFile };
                        if (request.AvatarFile == null)
                        {
                            avartarId = null;
                        }
                        else
                        {
                            var avartar = await _fileService.SaveFileAsync(id, "avarta", listFiles);
                            avartarId = avartar[0].Id;
                        }
                        account.AvatarId = avartarId;
                    }
                    else if (request.AvatarFile != null)
                    {
                        await _fileService.UpdateFileAsync(account.AvatarId, request.AvatarFile);
                    }
                    account.FullName = request.FullName;
                    account.Gender = request.Gender;
                    account.PhoneNumber = request.PhoneNumber;
                    account.IdentityCardNumber = request.IdentityCardNumber;                  
                    account.UpdatedAt = DateTime.UtcNow;
                    account.WardId = request?.WardId;
                    account.DistrictId = request?.DistrictId;
                    account.ProvinceId = request?.ProvinceId;
                    account.Email = request?.Email;
                    account.DateOfBirth = request?.DateOfBirth;


                    // Update address after loading relations
                    await _context.Entry(account)
                        .Reference(p => p.Ward)
                        .LoadAsync();
                    await _context.Entry(account)
                        .Reference(p => p.District)
                        .LoadAsync();
                    await _context.Entry(account)
                        .Reference(p => p.Province)
                        .LoadAsync();

                    account.Address = account.FormattedAddress;

                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return account;
                    }
                }
                return null;
            }
            catch(Exception e)
            {

                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
