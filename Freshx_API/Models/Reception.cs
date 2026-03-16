using System;
using System.Collections.Generic;
using Freshx_API.Models;

namespace Freshx_API.Models;

public partial class Reception
{
    public int ReceptionId { get; set; }
    public int? SequenceNumber { get; set; } // dùng cho xếp hàng, tự tăng
    public bool? IsPriority { get; set; } //có phải bện nhân ưu tiên
    public int? PatientId { get; set; }
    public int? ReceptionLocationId { get; set; }
    public int? ReceptionistId { get; set; } // nhan vien le tan khoa ngoai
    public DateTime? ReceptionDate { get; set; }
    public string? Note { get; set; }
    public int? AssignedDoctorId { get; set; } // bác sĩ chỉ định
    public int? MedicalServiceRequestId { get; set; } // dịch vụ chỉ định
    public int? ServiceTotalAmount { get; set; } // tổng tiền dịch vụ
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? IsDeleted { get; set; }
    public string? ReasonForVisit { get; set; } //lý do khám
    //public int? MedicalSer
    public virtual ICollection<MedicalServiceRequest> MedicalServiceRequest { get; set; } = new List<MedicalServiceRequest>();
    public virtual Doctor? AssignedDoctor { get; set; } // bác sĩ khám

    public virtual Patient? Patient { get; set; } // bệnh nhân khám
    public virtual Employee? Receptionist { get; set; } // nhân viên tiếp nhận
    public virtual Examine? Examine { get; set; }
    public virtual LabResult LabResult { get; set; }
}
