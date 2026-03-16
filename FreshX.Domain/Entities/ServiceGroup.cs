using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class ServiceGroup : BaseEntity
{
    public string? Code { get; set; } // Mã nhóm dịch vụ - ktra trùng lặp

    public string? Name { get; set; }

    public virtual ICollection<ServiceCatalog> ServiceCatalogs { get; set; } = new List<ServiceCatalog>();
}