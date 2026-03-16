using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Freshx_API.Models;

namespace Freshx_API.Models;

public partial class DiagnosticImagingResult
{
    [Key]
    public int DiagnosticImagingResultId { get; set; } // Bảng kết quả cận lâm sàng chẩn đoán hình ảnh 

    public DateTime? ExecutionDate { get; set; }

    public DateTime? ExecutionTime { get; set; }

    public int? MedicalServiceRequestId { get; set; } // id yc dịch vụ

    public int? TechnicianId { get; set; } // id người thực hiẻn
    public int? ConclusionDictionaryId { get; set; } // từ điển kết luận

    public int? ConcludingDoctorId { get; set; } // bác sĩ kết luận

    public string? Conclusion { get; set; } // kết luận

    public string? Result { get; set; } // kết quả

    public string? Description { get; set; }

    public string? Note { get; set; }

    public string? Instruction { get; set; }

    public string? Diagnosis { get; set; }

    public int? ResultTypeId { get; set; }

    public DateTime? SampleReceivedTime { get; set; }

    public int? SampleTypeId { get; set; }

    public int? SampleQualityId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? IsDeleted { get; set; }

    public string? GpbmacroDescription { get; set; }

    public string? GpbmicroDescription { get; set; }

    public string? SpouseName { get; set; }

    public int? SpouseYearOfBirth { get; set; }

    public int? SampleCollectionLocationMedicalFacilityId { get; set; }

    public int? IsSampleCollectedAtHome { get; set; }

    public DateTime? SampleReceivedDate { get; set; }

    public DateTime? SampleCollectionDate { get; set; }

    public DateTime? SampleCollectionTime { get; set; }

    public string? OtherComment { get; set; }

    public string? SampleCollectionLocation { get; set; }

    public int? SampleCollectorId { get; set; }

    public virtual Doctor? ConcludingDoctor { get; set; }
    public virtual MedicalServiceRequest? MedicalServiceRequest { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Reception? Reception { get; set; }

    public virtual Employee? SampleCollector { get; set; }

    public virtual Technician? Technician { get; set; }
    public virtual ConclusionDictionary ConclusionDictionary { get; set; } // từ điển kết luận
}
