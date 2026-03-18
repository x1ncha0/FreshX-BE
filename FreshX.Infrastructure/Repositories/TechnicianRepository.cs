using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Technician;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FreshX.Infrastructure.Repositories
{
    public class TechnicianRepository(
        FreshXDbContext context,
        IFileService fileService,
        ITokenRepository tokenRepository,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmailService emailService) : ITechnicianRepository
    {
        public async Task<Technician?> CreateTechnicianAsync(TechnicianRequest request)
        {
            await EnsureUniqueAsync(request, null);
            var position = await context.Positions.FirstOrDefaultAsync(item => item.Id == request.PositionId)
                ?? throw new InvalidOperationException("Vai trò không hợp lệ.");

            var formattedAddress = await BuildFormattedAddressAsync(request.WardId, request.DistrictId, request.ProvinceId);
            var avatarId = await SaveAvatarAsync(request.AvatarFile);
            var roleName = await ResolveTechnicianRoleAsync(position.Name);
            var tempPassword = GenerateSecurePassword();

            var appUser = new AppUser
            {
                FullName = request.Name,
                Email = request.Email,
                UserName = request.Email,
                DateOfBirth = request.DateOfBirth,
                IsActive = true,
                WardId = request.WardId,
                DistrictId = request.DistrictId,
                ProvinceId = request.ProvinceId,
                AvatarId = avatarId,
                PhoneNumber = request.PhoneNumber,
                Address = formattedAddress,
                Gender = request.Gender,
                IdentityCardNumber = request.IdentityCardNumber,
                CreatedAt = DateTime.UtcNow
            };

            await using var transaction = await context.Database.BeginTransactionAsync();

            var identityResult = await userManager.CreateAsync(appUser, tempPassword);
            if (!identityResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", identityResult.Errors.Select(error => error.Description)));
            }

            identityResult = await userManager.AddToRoleAsync(appUser, roleName);
            if (!identityResult.Succeeded)
            {
                await userManager.DeleteAsync(appUser);
                throw new InvalidOperationException(string.Join("; ", identityResult.Errors.Select(error => error.Description)));
            }

            var setupToken = await userManager.GeneratePasswordResetTokenAsync(appUser);
            await emailService.SendEmailAsync(
                request.Email!,
                "Thiết lập mật khẩu tài khoản FreshX",
                BuildPasswordSetupEmailBody(request.Email!, setupToken));

            var technician = new Technician
            {
                AccountId = appUser.Id,
                PositionId = request.PositionId,
                DepartmentId = request.DepartmentId,
                DateOfBirth = request.DateOfBirth,
                WardId = request.WardId,
                DistrictId = request.DistrictId,
                ProvinceId = request.ProvinceId,
                CreatedBy = tokenRepository.GetUserIdFromToken(),
                CreatedAt = DateTime.UtcNow,
                Address = formattedAddress,
                Gender = request.Gender,
                Name = request.Name,
                IsSuspended = false,
                IsDeleted = false,
                IdentityCardNumber = request.IdentityCardNumber,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                AvataId = avatarId
            };

            context.Technicians.Add(technician);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            await LoadTechnicianReferencesAsync(technician);
            return technician;
        }

        public async Task<List<Technician?>> GetAllTechnicianAsync(Parameters parameters)
        {
            var query = context.Technicians
                .AsNoTracking()
                .Include(technician => technician.Position)
                .Include(technician => technician.Department)
                .Where(technician => !technician.IsDeleted);

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(technician =>
                    (technician.Name != null && technician.Name.Contains(parameters.SearchTerm)) ||
                    (technician.Address != null && technician.Address.Contains(parameters.SearchTerm)));
            }

            query = parameters.SortOrderAsc ?? true
                ? query.OrderBy(technician => technician.CreatedAt)
                : query.OrderByDescending(technician => technician.CreatedAt);

            return [.. await query.ToListAsync()];
        }

        public Task<Technician?> GetTechnicianByIdAsyn(int id)
        {
            return context.Technicians
                .AsNoTracking()
                .Include(technician => technician.Position)
                .Include(technician => technician.Department)
                .FirstOrDefaultAsync(technician => technician.Id == id && !technician.IsDeleted);
        }

        public async Task<Technician?> DeleteTechnicianByIdAsyn(int id)
        {
            var technician = await context.Technicians
                .Include(item => item.AppUser)
                .Include(item => item.Position)
                .Include(item => item.Department)
                .FirstOrDefaultAsync(item => item.Id == id && !item.IsDeleted);

            if (technician is null)
            {
                return null;
            }

            if (technician.AppUser is not null)
            {
                technician.AppUser.IsActive = false;
            }

            technician.IsDeleted = true;
            technician.UpdatedAt = DateTime.UtcNow;
            technician.UpdatedBy = tokenRepository.GetUserIdFromToken();

            await context.SaveChangesAsync();
            return technician;
        }

        public async Task<Technician?> UpdateTechnicianByIdAsyn(int id, TechnicianRequest request)
        {
            var technician = await context.Technicians
                .Include(item => item.AppUser)
                .Include(item => item.Position)
                .Include(item => item.Department)
                .FirstOrDefaultAsync(item => item.Id == id && !item.IsDeleted);

            if (technician is null)
            {
                return null;
            }

            await EnsureUniqueAsync(request, technician.Id);

            var position = await context.Positions.FirstOrDefaultAsync(item => item.Id == request.PositionId)
                ?? throw new InvalidOperationException("Vai trò không hợp lệ.");

            var formattedAddress = await BuildFormattedAddressAsync(request.WardId, request.DistrictId, request.ProvinceId);
            await UpdateAvatarAsync(technician, request.AvatarFile);

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
            technician.UpdatedBy = tokenRepository.GetUserIdFromToken();
            technician.UpdatedAt = DateTime.UtcNow;

            if (technician.AppUser is not null)
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

                var roleName = await ResolveTechnicianRoleAsync(position.Name);
                await ReplaceRolesAsync(technician.AppUser, roleName);
            }

            await context.SaveChangesAsync();
            await LoadTechnicianReferencesAsync(technician);
            return technician;
        }

        private async Task EnsureUniqueAsync(TechnicianRequest request, int? currentId)
        {
            var normalizedEmail = request.Email?.Trim().ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(normalizedEmail))
            {
                var existingByEmail = await context.Technicians
                    .AsNoTracking()
                    .FirstOrDefaultAsync(technician =>
                        technician.Email != null &&
                        technician.Email.ToUpper() == normalizedEmail &&
                        !technician.IsDeleted);

                if (existingByEmail is not null && existingByEmail.Id != currentId)
                {
                    throw new InvalidOperationException("Email bạn nhập đã tồn tại trong hệ thống.");
                }
            }

            var existingByIdentityCard = await context.Technicians
                .AsNoTracking()
                .FirstOrDefaultAsync(technician =>
                    technician.IdentityCardNumber == request.IdentityCardNumber &&
                    !technician.IsDeleted);

            if (existingByIdentityCard is not null && existingByIdentityCard.Id != currentId)
            {
                throw new InvalidOperationException("CCCD bạn nhập đã tồn tại trong hệ thống.");
            }

            var existingByPhone = await context.Technicians
                .AsNoTracking()
                .FirstOrDefaultAsync(technician =>
                    technician.PhoneNumber == request.PhoneNumber &&
                    !technician.IsDeleted);

            if (existingByPhone is not null && existingByPhone.Id != currentId)
            {
                throw new InvalidOperationException("Số điện thoại bạn nhập đã tồn tại trong hệ thống.");
            }
        }

        private async Task<string> BuildFormattedAddressAsync(string? wardId, string? districtId, string? provinceId)
        {
            var ward = await context.Wards.AsNoTracking().FirstOrDefaultAsync(item => item.Code == wardId);
            var district = await context.Districts.AsNoTracking().FirstOrDefaultAsync(item => item.Code == districtId);
            var province = await context.Provinces.AsNoTracking().FirstOrDefaultAsync(item => item.Code == provinceId);

            return string.Join(", ", new[] { ward?.FullName, district?.FullName, province?.FullName }
                .Where(value => !string.IsNullOrWhiteSpace(value)));
        }

        private async Task<int?> SaveAvatarAsync(IFormFile? avatarFile)
        {
            if (avatarFile is null)
            {
                return null;
            }

            var savedFiles = await fileService.SaveFileAsync(
                tokenRepository.GetUserIdFromToken(),
                "avatar",
                [avatarFile]);

            return savedFiles.FirstOrDefault()?.Id;
        }

        private async Task UpdateAvatarAsync(Technician technician, IFormFile? avatarFile)
        {
            if (avatarFile is null)
            {
                return;
            }

            if (technician.AvataId is null)
            {
                var avatarId = await SaveAvatarAsync(avatarFile);
                technician.AvataId = avatarId;

                if (technician.AppUser is not null)
                {
                    technician.AppUser.AvatarId = avatarId;
                }

                return;
            }

            await fileService.UpdateFileAsync(technician.AvataId, avatarFile);
        }

        private async Task<string> ResolveTechnicianRoleAsync(string positionName)
        {
            if (!positionName.StartsWith("Kỹ Thuật", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Vai trò không đúng hợp lệ.");
            }

            foreach (var candidate in new[] { "Kỹ Thuật Viên Xét Nghiệm", "Kỹ Thuật Viên" })
            {
                if (await roleManager.RoleExistsAsync(candidate))
                {
                    return candidate;
                }
            }

            throw new InvalidOperationException("Vai trò kỹ thuật viên chưa được cấu hình trong hệ thống.");
        }

        private async Task ReplaceRolesAsync(AppUser user, string roleName)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Count > 0)
            {
                await userManager.RemoveFromRolesAsync(user, userRoles);
            }

            var addRoleResult = await userManager.AddToRoleAsync(user, roleName);
            if (!addRoleResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", addRoleResult.Errors.Select(error => error.Description)));
            }
        }

        private async Task LoadTechnicianReferencesAsync(Technician technician)
        {
            await context.Entry(technician).Reference(item => item.Position).LoadAsync();
            await context.Entry(technician).Reference(item => item.Department).LoadAsync();
        }

        private static string GenerateSecurePassword(int length = 12)
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            var allChars = lowercase + uppercase + numbers + special;

            Span<char> password = stackalloc char[length];
            password[0] = lowercase[RandomNumberGenerator.GetInt32(lowercase.Length)];
            password[1] = uppercase[RandomNumberGenerator.GetInt32(uppercase.Length)];
            password[2] = numbers[RandomNumberGenerator.GetInt32(numbers.Length)];
            password[3] = special[RandomNumberGenerator.GetInt32(special.Length)];

            for (var index = 4; index < length; index++)
            {
                password[index] = allChars[RandomNumberGenerator.GetInt32(allChars.Length)];
            }

            for (var index = password.Length - 1; index > 0; index--)
            {
                var swapIndex = RandomNumberGenerator.GetInt32(index + 1);
                (password[index], password[swapIndex]) = (password[swapIndex], password[index]);
            }

            return new string(password);
        }

        private static string BuildPasswordSetupEmailBody(string email, string setupToken)
        {
            var encodedToken = Uri.EscapeDataString(setupToken);
            var requestPayload = $$"""
{
  "email": "{{email}}",
  "token": "{{encodedToken}}",
  "password": "<mật-khẩu-mới>",
  "confirmPassword": "<mật-khẩu-mới>"
}
""";

            var builder = new StringBuilder();
            builder.AppendLine("<p>Tài khoản FreshX của bạn đã được tạo.</p>");
            builder.AppendLine("<p>Vì lý do bảo mật, hệ thống không gửi mật khẩu qua email.</p>");
            builder.AppendLine("<p>Hãy dùng token một lần bên dưới để thiết lập mật khẩu qua API <code>POST /api/Account/set-password</code>:</p>");
            builder.AppendLine($"<p><strong>Email:</strong> {email}</p>");
            builder.AppendLine($"<p><strong>Token:</strong> <code>{encodedToken}</code></p>");
            builder.AppendLine("<pre>");
            builder.AppendLine(requestPayload);
            builder.AppendLine("</pre>");
            builder.AppendLine("<p>Token chỉ nên dùng một lần và cần được giữ bí mật.</p>");
            return builder.ToString();
        }
    }
}
