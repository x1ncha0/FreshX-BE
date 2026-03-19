using FreshX.Application.Constants;
using FreshX.Application.Dtos.Auth.Account;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace FreshX.Infrastructure.Repositories;

public class AccountRepository(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    RoleManager<IdentityRole> roleManager,
    ITokenRepository tokenRepository,
    IEmailService emailService,
    ILogger<AccountRepository> logger) : IAccountRepository
{
    private const string PasswordResetTokenProvider = "FreshX.PasswordReset";
    private const string PasswordResetOtpHashName = "OtpHash";
    private const string PasswordResetOtpExpiresAtName = "OtpExpiresAt";

    public async Task<AppUser?> RegisterAccount(AddingRegister registerDto)
    {
        var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser is not null)
        {
            return null;
        }

        var appUser = new AppUser
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(appUser, registerDto.Password);
        if (!result.Succeeded)
        {
            return null;
        }

        if (!await roleManager.RoleExistsAsync(RoleNames.User))
        {
            var createRoleResult = await roleManager.CreateAsync(new IdentityRole(RoleNames.User));
            if (!createRoleResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", createRoleResult.Errors.Select(e => e.Description)));
            }
        }

        var addRoleResult = await userManager.AddToRoleAsync(appUser, RoleNames.User);
        if (!addRoleResult.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ", addRoleResult.Errors.Select(e => e.Description)));
        }

        return appUser;
    }

    public async Task<LoginResponse> LoginAccount(LoginRequest loginRequest)
    {
        var appUser = await userManager.FindByEmailAsync(loginRequest.Email);
        if (appUser is null)
        {
            return new LoginResponse
            {
                Succeeded = false,
                Message = "Email or password is invalid."
            };
        }

        if (await userManager.IsLockedOutAsync(appUser))
        {
            var lockoutEnd = await userManager.GetLockoutEndDateAsync(appUser);
            var timeRemaining = lockoutEnd.HasValue ? (int)(lockoutEnd.Value - DateTimeOffset.UtcNow).TotalSeconds : 0;

            return new LoginResponse
            {
                Succeeded = false,
                IsLockedOut = true,
                Message = $"Your account is locked out. Try again in {timeRemaining} seconds.",
                LockoutTimeRemaining = timeRemaining
            };
        }

        if (appUser.IsActive == false)
        {
            return new LoginResponse
            {
                Succeeded = false,
                IsNotAllowed = true,
                Message = "Account is inactive."
            };
        }

        var result = await signInManager.CheckPasswordSignInAsync(appUser, loginRequest.Password, lockoutOnFailure: true);
        if (!result.Succeeded)
        {
            var failedCount = await userManager.GetAccessFailedCountAsync(appUser);
            if (failedCount == 0)
            {
                return new LoginResponse
                {
                    Succeeded = false,
                    Message = "Email or password is invalid. Your account is locked out."
                };
            }

            var attemptsLeft = Math.Max(0, 5 - failedCount);
            return new LoginResponse
            {
                Succeeded = false,
                Message = $"Email or password is invalid. {attemptsLeft} attempt(s) remaining."
            };
        }

        await userManager.ResetAccessFailedCountAsync(appUser);
        appUser.LockoutEnd = null;
        await userManager.UpdateAsync(appUser);

        var tokenInfo = await tokenRepository.IssueAccessToken(appUser);
        var refreshToken = tokenRepository.IssueRefreshToken();
        var userName = appUser.UserName ?? appUser.Email ?? throw new InvalidOperationException("The account has no username.");

        if (!await tokenRepository.SaveRefreshToken(userName, refreshToken))
        {
            throw new InvalidOperationException("Unable to persist the refresh token.");
        }

        var roles = await userManager.GetRolesAsync(appUser);
        return new LoginResponse
        {
            Succeeded = true,
            Message = "Login successful.",
            AccessToken = tokenInfo.AccessToken,
            ExpireAt = tokenInfo.ExpiresAt,
            RefreshToken = refreshToken,
            User = new UserDto
            {
                Email = appUser.Email ?? string.Empty,
                CreateAt = appUser.CreatedAt,
                UpdateAt = appUser.UpdatedAt,
                IsActive = appUser.IsActive ?? false,
                Roles = roles.ToList()
            }
        };
    }

    public async Task<bool> EmailExist(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user is not null;
    }

    public async Task<AccountDto?> GetAccountInformationByIdAsync(string id)
    {
        var account = await userManager.FindByIdAsync(id);
        if (account is null)
        {
            return null;
        }

        var role = await userManager.GetRolesAsync(account);
        return new AccountDto
        {
            Email = account.Email ?? string.Empty,
            Name = account.FullName ?? string.Empty,
            RoleName = role.FirstOrDefault(),
            IsActive = account.IsActive,
            Id = account.Id
        };
    }

    public async Task<AccountDto?> UpdateAccountAsync(string id, UpdatingAccountRequest request)
    {
        var account = await userManager.FindByIdAsync(id);
        if (account is null)
        {
            return null;
        }

        account.Email = request.Email;
        account.FullName = request.Name;
        account.UserName = request.Email;
        account.IsActive = request.IsActive;
        account.UpdatedAt = DateTime.UtcNow;

        var updateResult = await userManager.UpdateAsync(account);
        if (!updateResult.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ", updateResult.Errors.Select(e => e.Description)));
        }

        var currentRoles = await userManager.GetRolesAsync(account);
        if (currentRoles.Any())
        {
            var removeRoleResult = await userManager.RemoveFromRolesAsync(account, currentRoles);
            if (!removeRoleResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", removeRoleResult.Errors.Select(e => e.Description)));
            }
        }

        string? roleName = null;
        if (!string.IsNullOrWhiteSpace(request.RoleId))
        {
            var newRole = await roleManager.FindByIdAsync(request.RoleId);
            if (newRole is not null)
            {
                var addRoleResult = await userManager.AddToRoleAsync(account, newRole.Name!);
                if (!addRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Join("; ", addRoleResult.Errors.Select(e => e.Description)));
                }

                roleName = newRole.Name;
            }
        }

        return new AccountDto
        {
            Id = account.Id,
            Email = account.Email ?? string.Empty,
            RoleName = roleName,
            Name = account.FullName ?? string.Empty,
            IsActive = account.IsActive
        };
    }

    public async Task<List<AccountDto>> GetAllAccountsAsync()
    {
        var accounts = await userManager.Users.AsNoTracking().ToListAsync();
        var results = new List<AccountDto>(accounts.Count);

        foreach (var account in accounts)
        {
            var roles = await userManager.GetRolesAsync(account);
            results.Add(new AccountDto
            {
                Id = account.Id,
                Name = account.FullName ?? string.Empty,
                IsActive = account.IsActive,
                Email = account.Email ?? string.Empty,
                RoleName = roles.FirstOrDefault()
            });
        }

        return results;
    }

    public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is not null)
        {
            var code = RandomNumberGenerator.GetInt32(100000, 1_000_000).ToString();
            await SavePasswordResetCodeAsync(user, code, DateTime.UtcNow.AddMinutes(2));

            var targetEmail = user.Email ?? request.Email;
            await emailService.SendEmailAsync(
                targetEmail,
                "Password Reset Verification Code",
                $"Your verification code is: {code}. This code will expire in 2 minutes.");
        }

        return new ForgotPasswordResponse
        {
            Message = "If the account exists, a reset instruction has been sent."
        };
    }

    public async Task VerifyResetCodeAsync(VerifyCodeRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await IsPasswordResetCodeValidAsync(user, request.Code))
        {
            throw new ArgumentException("Email or code is invalid.");
        }
    }

    public async Task ResetPasswordAsync(string code, ResetPasswordRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new ArgumentException("Reset code is invalid.");
        }

        if (!await IsPasswordResetCodeValidAsync(user, code))
        {
            throw new ArgumentException("Reset code is invalid.");
        }

        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, resetToken, request.Password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        user.UpdatedAt = DateTime.UtcNow;
        await ClearPasswordResetCodeAsync(user);

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ", updateResult.Errors.Select(e => e.Description)));
        }
    }

    public async Task SetPasswordAsync(SetPasswordRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new ArgumentException("Email hoặc token không hợp lệ.");
        }

        var token = WebUtility.UrlDecode(request.Token);
        var result = await userManager.ResetPasswordAsync(user, token, request.Password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ", result.Errors.Select(error => error.Description)));
        }

        user.UpdatedAt = DateTime.UtcNow;
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ", updateResult.Errors.Select(error => error.Description)));
        }
    }

    private async Task SavePasswordResetCodeAsync(AppUser user, string code, DateTime expiresAtUtc)
    {
        var setHashResult = await userManager.SetAuthenticationTokenAsync(
            user,
            PasswordResetTokenProvider,
            PasswordResetOtpHashName,
            HashPasswordResetCode(user.Id, code));

        if (!setHashResult.Succeeded)
        {
            throw new InvalidOperationException("An error occurred while processing your request.");
        }

        var setExpiryResult = await userManager.SetAuthenticationTokenAsync(
            user,
            PasswordResetTokenProvider,
            PasswordResetOtpExpiresAtName,
            expiresAtUtc.ToString("O"));

        if (!setExpiryResult.Succeeded)
        {
            throw new InvalidOperationException("An error occurred while processing your request.");
        }
    }

    private async Task<bool> IsPasswordResetCodeValidAsync(AppUser user, string code)
    {
        var storedHash = await userManager.GetAuthenticationTokenAsync(user, PasswordResetTokenProvider, PasswordResetOtpHashName);
        var storedExpiry = await userManager.GetAuthenticationTokenAsync(user, PasswordResetTokenProvider, PasswordResetOtpExpiresAtName);

        if (string.IsNullOrWhiteSpace(storedHash)
            || string.IsNullOrWhiteSpace(storedExpiry)
            || !DateTime.TryParse(storedExpiry, null, System.Globalization.DateTimeStyles.RoundtripKind, out var expiresAtUtc)
            || expiresAtUtc < DateTime.UtcNow)
        {
            return false;
        }

        var hashedCode = HashPasswordResetCode(user.Id, code);
        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(storedHash),
            Encoding.UTF8.GetBytes(hashedCode));
    }

    private async Task ClearPasswordResetCodeAsync(AppUser user)
    {
        var removeHashResult = await userManager.RemoveAuthenticationTokenAsync(user, PasswordResetTokenProvider, PasswordResetOtpHashName);
        if (!removeHashResult.Succeeded)
        {
            logger.LogWarning("Password reset hash cleanup failed for user {UserId}", user.Id);
        }

        var removeExpiryResult = await userManager.RemoveAuthenticationTokenAsync(user, PasswordResetTokenProvider, PasswordResetOtpExpiresAtName);
        if (!removeExpiryResult.Succeeded)
        {
            logger.LogWarning("Password reset expiry cleanup failed for user {UserId}", user.Id);
        }
    }

    private static string HashPasswordResetCode(string userId, string code)
    {
        var payload = $"{userId}:{code}";
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(payload));
        return Convert.ToBase64String(hash);
    }
}
