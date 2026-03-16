using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;
public partial class LabResult : BaseEntity
{
    public DateTime? ExecutionDate { get; set; } // Ngày thực hiện

    public DateTime? ExecutionTime { get; set; } // Thời gian thực hiện

    public int? ReceptionId { get; set; } // ID tiếp nhận

    public int? TechnicianId { get; set; } // ID kỹ thuật viên

    public int? ConcludingDoctorId { get; set; } // ID bác sĩ kết luận

    public string? Conclusion { get; set; } // Kết luận

    public string? Description { get; set; } // Mô tả

    public string? Note { get; set; } // Ghi chú

    public int? SampleTypeId { get; set; } // ID loại mẫu

    public int? SampleQuality { get; set; } // ID chất lượng mẫu 0 đạt,  1 k đạt

    public bool IsPaid { get; set; } // Đổi trạng thái khi xét nghiệm xong

    public string? SampleCollectionLocation { get; set; } // nơi lấy mẫu 

    public DateTime? SampleReceivedTime { get; set; } // Thời gian mà bệnh viện nhận được mẫu

    public DateTime? SampleCollectionTime { get; set; } // Thời gian mà mẫu vật được thu thập

    public virtual Doctor? ConcludingDoctor { get; set; } // Bác sĩ kết luận

    public virtual Patient? Patient { get; set; } // Bệnh nhân

    public virtual Reception? Reception { get; set; } // Tiếp nhận

    public virtual Technician? Technician { get; set; } // Kỹ thuật viên
}