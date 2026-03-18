using Microsoft.AspNetCore.Identity;

namespace FreshX.Domain.Common;

public abstract class AuditableIdentityUser : IdentityUser
{
    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
