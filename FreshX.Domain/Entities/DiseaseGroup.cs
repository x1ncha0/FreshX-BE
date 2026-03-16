using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class DiseaseGroup : BaseEntity
{
    /// <summary>
    /// Mã nhóm bệnh { ktra trùng lặp}
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên nhóm bệnh
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Danh mục bệnh theo tiêu chuẩn quốc tế
    /// </summary>
    public ICollection<ICDCatalog>? Catalog = new List<ICDCatalog>();
}