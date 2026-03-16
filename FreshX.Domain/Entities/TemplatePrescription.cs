using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class TemplatePrescription : BaseEntity
{
    /// <summary>
    /// Mã mẫu đơn thuốc
    /// </summary>
    public string? Code { get; set; } 

    /// <summary>
    /// Tên mẫu đơn thuốc
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Từ điển chẩn đoán
    /// </summary>
    public int? DiagnosisDictionaryId { get; set; } 

    /// <summary>
    /// Ghi chú chung
    /// </summary>
    public string? Note { get; set; } 

    /// <summary>
    /// Chi tiết toa thuốc
    /// </summary>
    public virtual ICollection<TemplatePrescriptionDetail> TemplatePrescriptionDetails { get; set; } = new List<TemplatePrescriptionDetail>();

    /// <summary>
    /// Từ điển chẩn đoán
    /// </summary>
    public virtual ICollection<DiagnosisDictionary>? DiagnosisDictionary { get; set; }
}