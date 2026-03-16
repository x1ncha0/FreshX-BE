using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class DrugBooking : BaseEntity
{
    /// <summary>
    /// ID khám bệnh
    /// </summary>
    public int? ExamineId { get; set; }

    /// <summary>
    /// ID đơn thuốc
    /// </summary>
    public int? PrescriptionId { get; set; }

    /// <summary>
    /// ID danh mục thuốc
    /// </summary>
    public int? DrugCatalogId { get; set; }

    /// <summary>
    /// Liều buổi sáng
    /// </summary>
    public decimal? MorningDose { get; set; }

    /// <summary>
    /// Liều buổi trưa
    /// </summary>
    public decimal? NoonDose { get; set; }

    /// <summary>
    /// Liều buổi chiều
    /// </summary>
    public decimal? AfternoonDose { get; set; }

    /// <summary>
    /// Liều buổi tối
    /// </summary>
    public decimal? EveningDose { get; set; }

    /// <summary>
    /// Số ngày cung cấp
    /// </summary>
    public decimal? DaysOfSupply { get; set; }

    /// <summary>
    /// Số lượng
    /// </summary>
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Giá đơn vị
    /// </summary>
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// Tổng số tiền
    /// </summary>
    public decimal? TotalAmount { get; set; }

    /// <summary>
    /// Hành động
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Trạng thái
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// Ghi chú
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Danh mục thuốc
    /// </summary>
    public virtual DrugCatalog? DrugCatalog { get; set; }

    /// <summary>
    /// Khám bệnh liên quan
    /// </summary>
    public virtual Examine? MedicalExamination { get; set; }

    /// <summary>
    /// Đơn thuốc liên quan
    /// </summary>
    public virtual Prescription? Prescription { get; set; }
}
