using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class ServiceGroup
{
    public int ServiceGroupId { get; set; } 

    public string? Code { get; set; } // Mã nhóm dịch vụ - ktra trùng lặp

    public string? Name { get; set; }

    public int? IsSuspended { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public int? IsDeleted { get; set; }
    public virtual ICollection<ServiceCatalog> ServiceCatalogs { get; set; } = new List<ServiceCatalog>();
}
