using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class Department : BaseEntity
{
    /// <summary>
    /// Mã phòng ban
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên phòng ban
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// ID loại phòng ban
    /// </summary>
    public int? DepartmentTypeId { get; set; }

    /// <summary>
    /// Loại phòng ban
    /// </summary>
    public virtual DepartmentType? DepartmentType { get; set; }
}