using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class MedicalServiceRequest : BaseEntity
{
    public DateTime? RequestTime { get; set; } // Thời gian yêu cầu

    /// <summary>
    /// ID của lễ tân
    /// </summary>
    public int? ReceptionId { get; set; }

    /// <summary>
    /// Số lượng dịch vụ
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// ID của dịch vụ
    /// </summary>
    public int? ServiceId { get; set; }

    public string? Results { get; set; } // kết quả

    /// <summary>
    /// Tổng số tiền của dịch vụ
    /// </summary>
    public decimal? ServiceTotalAmount { get; set; }

    /// <summary>
    /// Chiết khấu
    /// </summary>
    public decimal? Discount { get; set; }

    /// <summary>
    /// Trạng thái phê duyệt
    /// </summary>
    public bool? IsApproved { get; set; }

    /// <summary>
    /// Phòng thực hiện
    /// </summary>
    public int? DepartmentId { get; set; }

    /// <summary>
    /// Trạng thái của yêu cầu
    /// </summary>
    public bool? Status { get; set; }

    /// <summary>
    /// ID của người được giao nhiệm vụ
    /// </summary>
    public int? AssignedById { get; set; }

    public virtual Employee? AssignedByEmployee { get; set; }

    public virtual Doctor? AssignedByDoctor { get; set; }

    public virtual MedicalServiceRequest? ParentMedicalServiceRequest { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Reception? Reception { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ServiceCatalog? Service { get; set; }
}