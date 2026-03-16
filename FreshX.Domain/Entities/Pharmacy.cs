using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Pharmacy : BaseEntity
{
    public int PharmacyId { get; set; } // ID nhà thuốc

    public string? Code { get; set; } // Mã nhà thuốc

    public string? Name { get; set; } // Tên nhà thuốc

    public int? DepartmentId { get; set; } // ID phòng ban

    public int? InventoryTypeId { get; set; } // ID loại tồn kho

    public string? NameUnaccented { get; set; } // Tên không dấu của nhà thuốc

    public virtual ICollection<Department>? Department { get; set; } // Phòng ban của nhà thuốc

    public virtual ICollection<InventoryType>? InventoryType { get; set; } // Loại tồn kho của nhà thuốc
}