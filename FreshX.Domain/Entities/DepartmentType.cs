using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class DepartmentType : BaseEntity
{
    /// <summary>
    /// Mã loại phòng ban, ví dụ: "DP01"
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên loại phòng ban, ví dụ: "Phòng khám tổng quát"
    /// </summary>
    public string? Name { get; set; }
}