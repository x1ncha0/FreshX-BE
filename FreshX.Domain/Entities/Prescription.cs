using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;
public partial class Prescription : BaseEntity
{
    public int? MedicalExaminationId { get; set; } // ID khám bệnh

    public decimal? TotalAmount { get; set; } // Tổng số tiền

    public bool? IsPaid { get; set; } // Trạng thái đã thanh toán

    public string? Note { get; set; } // Ghi chú chung

    public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>(); // Chi tiết toa thuốc
}