using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshX.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; protected set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public bool IsSuspended { get; set; } = false;
}