using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class UnitOfMeasure : BaseEntity
{
    /// <summary>
    /// Mã đơn vị đo lường
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên đơn vị đo lường
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Loại thuốc
    /// </summary>
    public string? DrugType { get; set; }

    /// <summary>
    /// Giá trị chuyển đổi
    /// </summary>
    public decimal? ConversionValue { get; set; }
}