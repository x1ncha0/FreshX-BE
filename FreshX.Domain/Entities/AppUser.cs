using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshX.Domain.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? ExpiredTime { get; set; }

    public string? IdentityCardNumber { get; set; }

    public int? AvatarId { get; set; }

    public string? WardId { get; set; }

    public string? DistrictId { get; set; }

    public string? ProvinceId { get; set; }

    public string? Address { get; set; }

    [NotMapped]
    public string? FormattedAddress => string.Join(", ", new[]
    {
        Ward?.FullName,
        District?.FullName,
        Province?.FullName
    }.Where(x => !string.IsNullOrWhiteSpace(x)));

    public string? Gender { get; set; }

    public virtual Ward? Ward { get; set; }

    public virtual District? District { get; set; }

    public virtual Province? Province { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Technician? Technician { get; set; }
}
