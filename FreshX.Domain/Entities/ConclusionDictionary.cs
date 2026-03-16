using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class ConclusionDictionary : BaseEntity
{
    /// <summary>
    /// Mã từ điển kết luận
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên từ điển kết luận
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Số thứ tự
    /// </summary>
    public string? SequenceNumber { get; set; }

    /// <summary>
    /// Tiêu đề kết luận
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Giới tính
    /// </summary>
    public int? Gender { get; set; }

    /// <summary>
    /// Chẩn đoán
    /// </summary>
    public string? Diagnosis { get; set; }

    /// <summary>
    /// Tính toán ngày đến hạn
    /// </summary>
    public bool? CalculateDueDate { get; set; }

    /// <summary>
    /// Nội dung kết luận
    /// </summary>
    public string? Conclusion { get; set; }

    /// <summary>
    /// Mô tả kết luận
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ghi chú
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Lời khuyên y tế
    /// </summary>
    public string? MedicalAdvice { get; set; }

    /// <summary>
    /// ID danh mục dịch vụ
    /// </summary>
    public int? ServiceCatalogId { get; set; }

    /// <summary>
    /// Danh mục dịch vụ
    /// </summary>
    public virtual ServiceCatalog? ServiceCatalog { get; set; }
}