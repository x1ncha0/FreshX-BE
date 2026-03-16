using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class ServiceStandardValue : BaseEntity
{
    public int? ServiceCatalogId { get; set; } // ID danh mục dịch vụ

    public string? CommonValue { get; set; } // Giá trị chung

    public string? MaleMaximum { get; set; } // Giá trị tối đa cho nam

    public string? MaleMinimum { get; set; } // Giá trị tối thiểu cho nam

    public string? FemaleMaximum { get; set; } // Giá trị tối đa cho nữ

    public string? FemaleMinimum { get; set; } // Giá trị tối thiểu cho nữ

    public string? ChildrenMaximum { get; set; } // Giá trị tối đa cho trẻ em

    public string? ChildrenMinimum { get; set; } // Giá trị tối thiểu cho trẻ em

    public bool? IsLessThanOrEqualToMinimum { get; set; } // Trạng thái nhỏ hơn hoặc bằng tối thiểu

    public bool? IsGreaterThanOrEqualToMaximum { get; set; } // Trạng thái lớn hơn hoặc bằng tối đa

    public virtual ServiceCatalog? ServiceCatalog { get; set; } // Danh mục dịch vụ
}