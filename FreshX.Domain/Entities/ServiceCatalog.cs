using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class ServiceCatalog : BaseEntity
{
    public string? Code { get; set; } // Mã danh mục dịch vụ

    public string Name { get; set; } = string.Empty; // Tên danh mục dịch vụ (bắt buộc)

    public decimal? Price { get; set; } // Giá dịch vụ

    public string? UnitOfMeasure { get; set; } // Đơn vị đo lường

    public bool? ServiceStandardValueId { get; set; } // id giá trị chuẩn

    public int? Level { get; set; } // Cấp độ dịch vụ

    public bool? IsParentService { get; set; } // có phải dịch vụ cha không?

    public int? ParentServiceId { get; set; } // ID dịch vụ cha (nếu  là dịch vụ con của?)

    public int? ServiceGroupId { get; set; } // ID nhóm dịch vụ

    public int? ServiceTypeId { get; set; } // Id loại dịch vụ 

    public virtual ServiceCatalog? ParentService { get; set; } // Dịch vụ cha

    public virtual ServiceGroup? ServiceGroup { get; set; } // Nhóm dịch vụ

    public virtual ICollection<ServiceCatalog> ChildServices { get; set; } = new HashSet<ServiceCatalog>(); // Dịch vụ con

    public virtual ICollection<ServiceStandardValue> ServiceStandardValues { get; set; } = new HashSet<ServiceStandardValue>(); // Giá trị tiêu chuẩn dịch vụ

    public virtual ServiceTypes? ServiceTypes { get; set; }
}