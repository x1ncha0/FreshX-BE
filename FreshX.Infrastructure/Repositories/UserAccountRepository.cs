using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.UserAccount;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Application.Interfaces.UserAccount;
using FreshX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FreshX.Infrastructure.Repositories
{
    public class UserAccountRepository(
        UserManager<AppUser> userManager,
        ITokenRepository tokenRepository,
        IFileService fileService,
        ILogger<UserAccountRepository> logger) : IUserAccountRepository
    {
        public async Task<AppUser?> CreateUserAsync(AddingUserRequest request)
        {
            var currentUserId = tokenRepository.GetUserIdFromToken();
            int? avatarId = null;

            if (request.AvatarFile is not null)
            {
                var avatarFiles = await fileService.SaveFileAsync(currentUserId, "avatar", [request.AvatarFile]);
                avatarId = avatarFiles.FirstOrDefault()?.Id;
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
                Gender = request.Gender,
                IdentityCardNumber = request.IdentityCardNumber,
                ProvinceId = request.ProvinceId,
                WardId = request.WardId,
                DistrictId = request.DistrictId,
                AvatarId = avatarId,
                IsActive = true
            };

            var result = await userManager.CreateAsync(appUser);
            if (!result.Succeeded)
            {
                logger.LogWarning("Failed to create user {Email}: {Errors}", request.Email, string.Join("; ", result.Errors.Select(e => e.Description)));
                return null;
            }

            return await QueryUsers().FirstOrDefaultAsync(u => u.Id == appUser.Id);
        }

        public Task<AppUser?> GetUserByIdAsync(string id)
        {
            return QueryUsers().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<CustomPageResponse<IEnumerable<UserResponse?>>> GetUsersAsync(Parameters parameters)
        {
            var query = QueryUsers();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(u => u.FullName != null && u.FullName.Contains(parameters.SearchTerm));
            }

            query = parameters.SortOrderAsc ?? true
                ? query.OrderBy(u => u.FullName ?? string.Empty)
                : query.OrderByDescending(u => u.FullName ?? string.Empty);

            var entities = await query.ToListAsync();
            var items = entities.Select(ToUserResponse).ToList();
            return new CustomPageResponse<IEnumerable<UserResponse?>>(items, 1, items.Count == 0 ? 1 : items.Count, items.Count);
        }

        public async Task<AppUser?> DeleteUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
            {
                return null;
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return user;
        }

        public async Task<AppUser?> UpdateUserByIdAsync(string id, UpdatingUserRequest request)
        {
            var user = await QueryUsers().FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return null;
            }

            if (request.AvatarFile is not null)
            {
                if (user.AvatarId is null)
                {
                    var avatarFiles = await fileService.SaveFileAsync(tokenRepository.GetUserIdFromToken(), "avatar", [request.AvatarFile]);
                    user.AvatarId = avatarFiles.FirstOrDefault()?.Id;
                }
                else
                {
                    await fileService.UpdateFileAsync(user.AvatarId, request.AvatarFile);
                }
            }

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.UserName = request.Email;
            user.DateOfBirth = request.DateOfBirth;
            user.Gender = request.Gender;
            user.PhoneNumber = request.PhoneNumber;
            user.IdentityCardNumber = request.IdentityCardNumber;
            user.WardId = request.WardId;
            user.DistrictId = request.DistrictId;
            user.ProvinceId = request.ProvinceId;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return await QueryUsers().FirstOrDefaultAsync(u => u.Id == id);
        }

        private IQueryable<AppUser> QueryUsers() =>
            userManager.Users
                .Include(u => u.Ward)
                .Include(u => u.District)
                .Include(u => u.Province)
                .AsNoTracking();

        private static UserResponse ToUserResponse(AppUser user)
        {
            int? age = null;
            if (user.DateOfBirth.HasValue)
            {
                age = DateTime.Today.Year - user.DateOfBirth.Value.Year;
                if (user.DateOfBirth.Value.Date > DateTime.Today.AddYears(-age.Value))
                {
                    age--;
                }
            }

            return new UserResponse
            {
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                IdentityCardNumber = user.IdentityCardNumber,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AvatarId = user.AvatarId,
                Age = age,
                Ward = user.Ward,
                District = user.District,
                Province = user.Province
            };
        }
    }
}
