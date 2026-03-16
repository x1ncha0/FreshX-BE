using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models;

public partial class Department
{
    [Key]
    public int DepartmentId { get; set; } // ID phòng ban

    public string? Code { get; set; } // Mã phòng ban

    public string? Name { get; set; } // Tên phòng ban

    public int? DepartmentTypeId { get; set; } // ID loại phòng ban

    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng ktra trạng thái

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    public string? CreatedBy { get; set; } // Người tạo

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public string? UpdatedBy { get; set; } // Người cập nhật

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public virtual DepartmentType? DepartmentType { get; set; } // Loại phòng ban

}
