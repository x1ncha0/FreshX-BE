using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class DrugType : BaseEntity
{
    /// <summary>
    /// Mã loại thuốc
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên loại thuốc
    /// </summary>
    public string? Name { get; set; }
}