using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Employee;
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
    public class EmployeeRepository(
        FreshXDbContext context,
        IFileService fileService,
        ITokenRepository tokenRepository,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmailService emailService) : IEmployeeRepository
    {
        public async Task<Employee?> CreateEmployeeAsync(EmployeeRequest request)
        {
            await EnsureUniqueAsync(request, null);
            var position = await context.Positions.FirstOrDefaultAsync(item => item.Id == request.PositionId)
                ?? throw new InvalidOperationException("Vai trò không hợp lệ.");

            var formattedAddress = await BuildFormattedAddressAsync(request.WardId, request.DistrictId, request.ProvinceId);
            var avatarId = await SaveAvatarAsync(request.AvatarFile);
            var roleName = await ResolveEmployeeRoleAsync(position.Name);
            var employeeCode = await GenerateEmployeeCodeAsync(position.Name);
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

            var employee = new Employee
            {
                AccountId = appUser.Id,
                PositionId = request.PositionId,
                DepartmentId = request.DepartmentId,
                EmployeeCode = employeeCode,
                DateOfBirth = request.DateOfBirth,
                WardId = request.WardId,
                DistrictId = request.DistrictId,
                ProvinceId = request.ProvinceId,
                CreatedBy = tokenRepository.GetUserIdFromToken(),
                CreatedAt = DateTime.UtcNow,
                Address = formattedAddress,
                Gender = request.Gender,
                FullName = request.Name,
                IsSuspended = false,
                IsDeleted = false,
                IdentityCardNumber = request.IdentityCardNumber,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                AvataId = avatarId
            };

            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            await LoadEmployeeReferencesAsync(employee);
            return employee;
        }

        public async Task<List<Employee?>> GetAllEmployeesAsync(Parameters parameters)
        {
            var query = context.Employees
                .AsNoTracking()
                .Include(employee => employee.Position)
                .Include(employee => employee.Department)
                .Where(employee => !employee.IsDeleted);

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(employee =>
                    (employee.FullName != null && employee.FullName.Contains(parameters.SearchTerm)) ||
                    (employee.Address != null && employee.Address.Contains(parameters.SearchTerm)) ||
                    (employee.EmployeeCode != null && employee.EmployeeCode.Contains(parameters.SearchTerm)));
            }

            query = parameters.SortOrderAsc ?? true
                ? query.OrderBy(employee => employee.CreatedAt)
                : query.OrderByDescending(employee => employee.CreatedAt);

            return [.. await query.ToListAsync()];
        }

        public Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return context.Employees
                .AsNoTracking()
                .Include(employee => employee.Position)
                .Include(employee => employee.Department)
                .FirstOrDefaultAsync(employee => employee.Id == id && !employee.IsDeleted);
        }

        public async Task<Employee?> DeleteEmployeeByIdAsync(int id)
        {
            var employee = await context.Employees
                .Include(item => item.AppUser)
                .Include(item => item.Position)
                .Include(item => item.Department)
                .FirstOrDefaultAsync(item => item.Id == id && !item.IsDeleted);

            if (employee is null)
            {
                return null;
            }

            if (employee.AppUser is not null)
            {
                employee.AppUser.IsActive = false;
            }

            employee.IsDeleted = true;
            employee.UpdatedAt = DateTime.UtcNow;
            employee.UpdatedBy = tokenRepository.GetUserIdFromToken();

            await context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployeeByIdAsync(int id, EmployeeRequest request)
        {
            var employee = await context.Employees
                .Include(item => item.AppUser)
                .Include(item => item.Position)
                .Include(item => item.Department)
                .FirstOrDefaultAsync(item => item.Id == id && !item.IsDeleted);

            if (employee is null)
            {
                return null;
            }

            await EnsureUniqueAsync(request, employee.Id);

            var position = await context.Positions.FirstOrDefaultAsync(item => item.Id == request.PositionId)
                ?? throw new InvalidOperationException("Vai trò không hợp lệ.");

            var formattedAddress = await BuildFormattedAddressAsync(request.WardId, request.DistrictId, request.ProvinceId);
            await UpdateAvatarAsync(employee, request.AvatarFile);

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
            employee.EmployeeCode = await GenerateEmployeeCodeAsync(position.Name, employee.Id);
            employee.UpdatedBy = tokenRepository.GetUserIdFromToken();
            employee.UpdatedAt = DateTime.UtcNow;

            if (employee.AppUser is not null)
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

                var roleName = await ResolveEmployeeRoleAsync(position.Name);
                await ReplaceRolesAsync(employee.AppUser, roleName);
            }

            await context.SaveChangesAsync();
            await LoadEmployeeReferencesAsync(employee);
            return employee;
        }

        private async Task EnsureUniqueAsync(EmployeeRequest request, int? currentId)
        {
            var normalizedEmail = request.Email?.Trim().ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(normalizedEmail))
            {
                var existingByEmail = await context.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(employee =>
                        employee.Email != null &&
                        employee.Email.ToUpper() == normalizedEmail &&
                        !employee.IsDeleted);

                if (existingByEmail is not null && existingByEmail.Id != currentId)
                {
                    throw new InvalidOperationException("Email bạn nhập đã tồn tại trong hệ thống.");
                }
            }

            var existingByIdentityCard = await context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(employee =>
                    employee.IdentityCardNumber == request.IdentityCardNumber &&
                    !employee.IsDeleted);

            if (existingByIdentityCard is not null && existingByIdentityCard.Id != currentId)
            {
                throw new InvalidOperationException("CCCD bạn nhập đã tồn tại trong hệ thống.");
            }

            var existingByPhone = await context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(employee =>
                    employee.PhoneNumber == request.PhoneNumber &&
                    !employee.IsDeleted);

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

        private async Task UpdateAvatarAsync(Employee employee, IFormFile? avatarFile)
        {
            if (avatarFile is null)
            {
                return;
            }

            if (employee.AvataId is null)
            {
                var avatarId = await SaveAvatarAsync(avatarFile);
                employee.AvataId = avatarId;

                if (employee.AppUser is not null)
                {
                    employee.AppUser.AvatarId = avatarId;
                }

                return;
            }

            await fileService.UpdateFileAsync(employee.AvataId, avatarFile);
        }

        private async Task<string> ResolveEmployeeRoleAsync(string positionName)
        {
            var candidates = positionName.StartsWith("Tiếp Nhận", StringComparison.OrdinalIgnoreCase)
                ? new[] { "Tiếp Nhận", "Lễ Tân" }
                : new[] { "Thu Ngân" };

            foreach (var candidate in candidates)
            {
                if (await roleManager.RoleExistsAsync(candidate))
                {
                    return candidate;
                }
            }

            throw new InvalidOperationException("Vai trò nhân viên chưa được cấu hình trong hệ thống.");
        }

        private async Task<string> GenerateEmployeeCodeAsync(string positionName, int? currentEmployeeId = null)
        {
            var prefix = positionName.StartsWith("Tiếp Nhận", StringComparison.OrdinalIgnoreCase) ? "NVTN" : "NVKT";

            var existingCodes = await context.Employees
                .AsNoTracking()
                .Where(employee =>
                    !employee.IsDeleted &&
                    employee.Id != currentEmployeeId &&
                    employee.EmployeeCode != null &&
                    employee.EmployeeCode.StartsWith(prefix))
                .Select(employee => employee.EmployeeCode!)
                .ToListAsync();

            var nextNumber = existingCodes
                .Select(code => code[prefix.Length..])
                .Select(numberPart => int.TryParse(numberPart, out var number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max() + 1;

            return $"{prefix}{nextNumber:D3}";
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

        private async Task LoadEmployeeReferencesAsync(Employee employee)
        {
            await context.Entry(employee).Reference(item => item.Position).LoadAsync();
            await context.Entry(employee).Reference(item => item.Department).LoadAsync();
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
