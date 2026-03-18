using FreshX.Application.Dtos;
using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FreshX.Infrastructure.Repositories
{
    public class UserAccountManagementRepository(
        FreshXDbContext context,
        IFileService fileService,
        ILogger<UserAccountManagementRepository> logger) : IUserAccountManagementRepository
    {
        public Task<AppUser?> GetInformationAccoutUserById(string id)
        {
            return context.Users
                .Include(x => x.Ward)
                .Include(x => x.District)
                .Include(x => x.Province)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser?> UpdateInformationAccountUserById(string id, UserAccountRequest request)
        {
            var account = await context.Users
                .Include(x => x.Ward)
                .Include(x => x.District)
                .Include(x => x.Province)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (account is null)
            {
                return null;
            }

            if (request.AvatarFile is not null)
            {
                if (account.AvatarId is null)
                {
                    var avatarFiles = await fileService.SaveFileAsync(id, "avatar", [request.AvatarFile]);
                    account.AvatarId = avatarFiles.FirstOrDefault()?.Id;
                }
                else
                {
                    await fileService.UpdateFileAsync(account.AvatarId, request.AvatarFile);
                }
            }

            account.FullName = request.FullName;
            account.Gender = request.Gender;
            account.PhoneNumber = request.PhoneNumber;
            account.IdentityCardNumber = request.IdentityCardNumber;
            account.UpdatedAt = DateTime.UtcNow;
            account.WardId = request.WardId;
            account.DistrictId = request.DistrictId;
            account.ProvinceId = request.ProvinceId;
            account.Email = request.Email;
            account.UserName = request.Email;
            account.DateOfBirth = request.DateOfBirth;

            var result = await context.SaveChangesAsync();
            if (result <= 0)
            {
                logger.LogWarning("No rows were updated for user account {UserId}", id);
            }

            await context.Entry(account).Reference(p => p.Ward).LoadAsync();
            await context.Entry(account).Reference(p => p.District).LoadAsync();
            await context.Entry(account).Reference(p => p.Province).LoadAsync();
            return account;
        }
    }
}
