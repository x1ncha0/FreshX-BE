using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class Pharmacy
{
    public int PharmacyId { get; set; } // ID nhà thuốc

    public string? Code { get; set; } // Mã nhà thuốc

    public string? Name { get; set; } // Tên nhà thuốc

    public int? DepartmentId { get; set; } // ID phòng ban

    public int? InventoryTypeId { get; set; } // ID loại tồn kho

    public bool? IsSuspended { get; set; } // Trạng thái tạm ngưng

    public string? NameUnaccented { get; set; } // Tên không dấu của nhà thuốc

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public string? CreatedBy { get; set; } // Người tạo

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public string? UpdatedBy { get; set; } // Người cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    public virtual ICollection<Department>? Department { get; set; } // Phòng ban của nhà thuốc
    public virtual ICollection <InventoryType>? InventoryType { get; set; } // Loại tồn kho của nhà thuốc
}
