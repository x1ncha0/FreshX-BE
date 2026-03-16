using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models;
// Đây là class đại diện cho bảng "DepartmentType" trong database
public partial class DepartmentType
{
    [Key] // Đánh dấu DepartmentTypeId là khóa chính
    public int DepartmentTypeId { get; set; } // ID loại phòng ban

    public string? Code { get; set; } // Mã loại phòng ban, ví dụ: "DP01"

    public string? Name { get; set; } // Tên loại phòng ban, ví dụ: "Phòng khám tổng quát"

    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng (0: hoạt động, 1: tạm ngưng)

    public int? IsDeleted { get; set; } // Trạng thái xóa mềm (0: chưa xóa, 1: đã xóa)

    public string? CreatedBy { get; set; } // ID người tạo

    public DateTime? CreatedDate { get; set; } // Thời gian tạo

    public string? UpdatedBy { get; set; } // ID người cập nhật
        
    public DateTime? UpdatedDate { get; set; } // Thời gian cập nhật

}
