using System;
using System.Collections.Generic;
using Freshx_API.Models;

namespace Freshx_API.Models;

public partial class MedicalServiceRequest
{
    public int MedicalServiceRequestId { get; set; } // ID của yêu cầu dịch vụ y tế

    public DateTime? RequestTime { get; set; } // Thời gian yêu cầu

    // ID của lễ tân
    public int? ReceptionId { get; set; }

    // Số lượng dịch vụ
    public int? Quantity { get; set; }

    // ID của dịch vụ
    public int? ServiceId { get; set; }
    public string? Results  { get; set; } // kết quả

    // Tổng số tiền của dịch vụ
    public decimal? ServiceTotalAmount { get; set; }
    
    //chiết khấu
    public decimal? discount { get; set; }

    // Trạng thái phê duyệt
    public bool? IsApproved { get; set; }

    //phòng thực hiện
    public int? DepartmentId { get; set; }

    // Trạng thái của yêu cầu
    public bool? Status { get; set; }

    // ID của người được giao nhiệm vụ
    public int? AssignedById { get; set; }

    // ID của người tạo yêu cầu
    public int? CreatedBy { get; set; }

    // Ngày tạo yêu cầu
    public DateTime? CreatedDate { get; set; }

    // Người cập nhật yêu cầu
    public string? UpdatedBy { get; set; }

    // Ngày cập nhật yêu cầu
    public DateTime? UpdatedDate { get; set; }

    // Trạng thái xóa yêu cầu
    public int? IsDeleted { get; set; }

    // Nhân viên được giao nhiệm vụ
    public virtual Employee? AssignedByEmployee { get; set; }

    // Bác sĩ được giao nhiệm vụ
    public virtual Doctor? AssignedByDoctor { get; set; }

    // Yêu cầu dịch vụ y tế cha
    public virtual MedicalServiceRequest? ParentMedicalServiceRequest { get; set; }

    // Bệnh nhân
    public virtual Patient? Patient { get; set; }

    // Lễ tân
    public virtual Reception? Reception { get; set; }

    public virtual Department? Department { get; set; }

    // Mối quan hệ 1 MedicalServiceRequest có thể có nhiều dịch vụ
    public virtual ServiceCatalog? Service { get; set; }
}