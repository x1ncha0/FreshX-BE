using FreshX.Application.Constants;
using FreshX.Application.Dtos;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class DoctorRepository(
    FreshXDbContext context,
    UserManager<AppUser> userManager,
    ITokenRepository tokenRepository,
    IFileService fileService,
    IEmailService emailService) : IDoctorRepository
{
    public async Task<Doctor> CreateAsync(DoctorCreateUpdateDto request, CancellationToken cancellationToken = default)
    {
        var avatarId = await SaveAvatarAsync(request.AvatarFile, cancellationToken);
        var formattedAddress = await BuildFormattedAddressAsync(request.WardId, request.DistrictId, request.ProvinceId, cancellationToken);
        var password = Guid.NewGuid().ToString("N")[..12] + "!";

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
            CreatedAt = DateTime.UtcNow,
            PhoneNumber = request.PhoneNumber,
            Address = formattedAddress,
            Gender = request.Gender,
            IdentityCardNumber = request.IdentityCardNumber
        };

        var userResult = await userManager.CreateAsync(appUser, password);
        if (!userResult.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ", userResult.Errors.Select(e => e.Description)));
        }

        var roleName = ResolveRoleName(request.PositionId);
        if (!string.IsNullOrWhiteSpace(roleName))
        {
            var roleResult = await userManager.AddToRoleAsync(appUser, roleName);
            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(appUser);
                throw new InvalidOperationException(string.Join("; ", roleResult.Errors.Select(e => e.Description)));
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            await emailService.SendEmailAsync(request.Email, "Tài khoản đăng nhập", $"Email: {request.Email}, Password: {password}");
        }

        var doctor = new Doctor
        {
            AccountId = appUser.Id,
            PositionId = request.PositionId,
            Specialty = request.Specialty,
            DateOfBirth = request.DateOfBirth,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = tokenRepository.GetUserIdFromToken(),
            DepartmentId = request.DepartmentId,
            WardId = request.WardId,
            DistrictId = request.DistrictId,
            ProvinceId = request.ProvinceId,
            Address = formattedAddress,
            Gender = request.Gender,
            Name = request.Name,
            IsSuspended = false,
            IsDeleted = false,
            IdentityCardNumber = request.IdentityCardNumber,
            Phone = request.PhoneNumber,
            Email = request.Email,
            AvataId = avatarId
        };

        context.Doctors.Add(doctor);
        await context.SaveChangesAsync(cancellationToken);

        return await LoadDoctorGraphAsync(doctor.Id, cancellationToken) ?? doctor;
    }

    public async Task<IReadOnlyList<Doctor>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default)
    {
        var query = context.Doctors
            .Include(d => d.Position)
            .Include(d => d.Department)
            .Where(d => !d.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
        {
            query = query.Where(d =>
                (d.Name != null && d.Name.Contains(parameters.SearchTerm)) ||
                (d.Address != null && d.Address.Contains(parameters.SearchTerm)) ||
                (d.Specialty != null && d.Specialty.Contains(parameters.SearchTerm)));
        }

        query = parameters.SortOrderAsc ?? true
            ? query.OrderBy(d => d.CreatedAt)
            : query.OrderByDescending(d => d.CreatedAt);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Doctor?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await LoadDoctorGraphAsync(id, cancellationToken);
    }

    public async Task<Doctor?> UpdateAsync(int id, DoctorCreateUpdateDto request, CancellationToken cancellationToken = default)
    {
        var doctor = await context.Doctors
            .Include(d => d.AppUser)
            .Include(d => d.Position)
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted, cancellationToken);

        if (doctor is null)
        {
            return null;
        }

        if (request.AvatarFile is not null)
        {
            if (doctor.AvataId is null)
            {
                doctor.AvataId = await SaveAvatarAsync(request.AvatarFile, cancellationToken);
            }
            else
            {
                await fileService.UpdateFileAsync(doctor.AvataId, request.AvatarFile);
            }
        }

        var formattedAddress = await BuildFormattedAddressAsync(request.WardId, request.DistrictId, request.ProvinceId, cancellationToken);

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
        doctor.UpdatedBy = tokenRepository.GetUserIdFromToken();
        doctor.UpdatedAt = DateTime.UtcNow;

        if (doctor.AppUser is not null)
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
            doctor.AppUser.AvatarId = doctor.AvataId;

            var targetRole = ResolveRoleName(request.PositionId);
            if (!string.IsNullOrWhiteSpace(targetRole))
            {
                var currentRoles = await userManager.GetRolesAsync(doctor.AppUser);
                if (currentRoles.Count > 0)
                {
                    await userManager.RemoveFromRolesAsync(doctor.AppUser, currentRoles);
                }

                await userManager.AddToRoleAsync(doctor.AppUser, targetRole);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
        return await LoadDoctorGraphAsync(id, cancellationToken);
    }

    public async Task<Doctor?> SoftDeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var doctor = await context.Doctors
            .Include(d => d.AppUser)
            .Include(d => d.Position)
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted, cancellationToken);

        if (doctor is null)
        {
            return null;
        }

        doctor.IsDeleted = true;
        if (doctor.AppUser is not null)
        {
            doctor.AppUser.IsActive = false;
        }

        await context.SaveChangesAsync(cancellationToken);
        return doctor;
    }

    private async Task<int?> SaveAvatarAsync(IFormFile? avatarFile, CancellationToken cancellationToken)
    {
        if (avatarFile is null)
        {
            return null;
        }

        var files = await fileService.SaveFileAsync(
            tokenRepository.GetUserIdFromToken(),
            "avatar",
            [avatarFile]);

        cancellationToken.ThrowIfCancellationRequested();
        return files.FirstOrDefault()?.Id;
    }

    private async Task<string?> BuildFormattedAddressAsync(string? wardCode, string? districtCode, string? provinceCode, CancellationToken cancellationToken)
    {
        var ward = await context.Wards.AsNoTracking().FirstOrDefaultAsync(w => w.Code == wardCode, cancellationToken);
        var district = await context.Districts.AsNoTracking().FirstOrDefaultAsync(d => d.Code == districtCode, cancellationToken);
        var province = await context.Provinces.AsNoTracking().FirstOrDefaultAsync(p => p.Code == provinceCode, cancellationToken);

        return string.Join(", ", new[] { ward?.FullName, district?.FullName, province?.FullName }
            .Where(x => !string.IsNullOrWhiteSpace(x)));
    }

    private async Task<Doctor?> LoadDoctorGraphAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Doctors
            .Include(d => d.Position)
            .Include(d => d.Department)
            .Include(d => d.AppUser)
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted, cancellationToken);
    }

    private static string? ResolveRoleName(int? positionId) => positionId switch
    {
        1 => RoleNames.ClinicDoctor,
        5 => RoleNames.UltrasoundDoctor,
        _ => null
    };
}
