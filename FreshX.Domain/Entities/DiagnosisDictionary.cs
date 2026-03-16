using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class DiagnosisDictionary : BaseEntity
{
    /// <summary>
    /// Mã chẩn đoán
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Tên chẩn đoán
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Số thứ tự
    /// </summary>
    public string? SequenceNumber { get; set; }

    /// <summary>
    /// Tiêu đề chẩn đoán
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Nội dung chẩn đoán
    /// </summary>
    public string? Diagnosis { get; set; }

    /// <summary>
    /// Tính toán ngày đến hạn
    /// </summary>
    public bool? CalculateDueDate { get; set; }

    /// <summary>
    /// Lời khuyên y tế
    /// </summary>
    public string? MedicalAdvice { get; set; }
}