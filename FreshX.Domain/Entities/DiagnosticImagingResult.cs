using FreshX.Domain.Common;

namespace FreshX.Domain.Entities;

public partial class DiagnosticImagingResult : BaseEntity
{
    public DateTime? ExecutionDate { get; set; }

    public DateTime? ExecutionTime { get; set; }

    /// <summary>
    /// Id yc dịch vụ
    /// </summary>
    public int? MedicalServiceRequestId { get; set; }

    /// <summary>
    /// Id người thực hiện
    /// </summary>
    public int? TechnicianId { get; set; }

    /// <summary>
    /// Từ điển kết luận
    /// </summary>
    public int? ConclusionDictionaryId { get; set; }

    /// <summary>
    /// Bác sĩ kết luận
    /// </summary>
    public int? ConcludingDoctorId { get; set; }

    /// <summary>
    /// Kết luận
    /// </summary>
    public string? Conclusion { get; set; }

    /// <summary>
    /// Kết quả
    /// </summary>
    public string? Result { get; set; }

    public string? Description { get; set; }

    public string? Note { get; set; }

    public string? Instruction { get; set; }

    public string? Diagnosis { get; set; }

    public int? ResultTypeId { get; set; }

    public DateTime? SampleReceivedTime { get; set; }

    public int? SampleTypeId { get; set; }

    public int? SampleQualityId { get; set; }

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

    /// <summary>
    /// Từ điển kết luận
    /// </summary>
    public virtual ConclusionDictionary? ConclusionDictionary { get; set; }
}