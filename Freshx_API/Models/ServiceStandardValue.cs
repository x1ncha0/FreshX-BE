using System;
using System.Collections.Generic;

namespace Freshx_API.Models;

public partial class ServiceStandardValue
{
    public int ServiceStandardValueId { get; set; } // ID giá trị tiêu chuẩn dịch vụ

    public int? ServiceCatalogId { get; set; } // ID danh mục dịch vụ

    public string? CommonValue { get; set; } // Giá trị chung

    public string? MaleMaximum { get; set; } // Giá trị tối đa cho nam

    public string? MaleMinimum { get; set; } // Giá trị tối thiểu cho nam

    public string? FemaleMaximum { get; set; } // Giá trị tối đa cho nữ

    public string? FemaleMinimum { get; set; } // Giá trị tối thiểu cho nữ

    public string? ChildrenMaximum { get; set; } // Giá trị tối đa cho trẻ em

    public string? ChildrenMinimum { get; set; } // Giá trị tối thiểu cho trẻ em

    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? UpdatedBy { get; set; } // Người cập nhật

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    public bool? IsLessThanOrEqualToMinimum { get; set; } // Trạng thái nhỏ hơn hoặc bằng tối thiểu

    public bool? IsGreaterThanOrEqualToMaximum { get; set; } // Trạng thái lớn hơn hoặc bằng tối đa

    public virtual ServiceCatalog? ServiceCatalog { get; set; } // Danh mục dịch vụ
}
