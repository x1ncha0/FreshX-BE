using FreshX.Application.Dtos;
using FreshX.Application.Dtos.ExamineDtos;
using FreshX.Application.Dtos.Patient;
using FreshX.Domain.Entities;
namespace FreshX.Application.Dtos
{
    public class CreateReceptionDto
    {
        public AddingPatientRequest AddingPatient { get; set; } = null!;
        public int? SequenceNumber { get; set; }
        public bool? IsPriority { get; set; }
        public int? PatientId { get; set; }
        public int? ReceptionLocationId { get; set; }
        public int? ReceptionistId { get; set; }
        public string? Note { get; set; }
        public int? AssignedDoctorId { get; set; }
        public List<int>? MedicalServiceRequestId { get; set; }
        public string? ReasonForVisit { get; set; }
        public List<CreateMedicalServiceRequestDto>? MedicalServiceRequest { get; set; } = new List<CreateMedicalServiceRequestDto>();
    }

    public class ReceptionDto
    {
        public int ReceptionId { get; set; }
        public int? SequenceNumber { get; set; }
        public bool? IsPriority { get; set; }
        public int? PatientId { get; set; }
        public int? ReceptionLocationId { get; set; }
        public int? ReceptionistId { get; set; }
        public DateTime? ReceptionDate { get; set; }
        public string? Note { get; set; }
        public int? AssignedDoctorId { get; set; }
        public int? MedicalServiceRequestId { get; set; }
        public int? ServiceTotalAmount { get; set; }
        public string? ReasonForVisit { get; set; }
        public List<MedicalServiceRequestDto>? MedicalServiceRequest { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ExamineOnly Examine { get; set; } = null!;
        public LabResult LabResult { get; set; } = null!;
    }
}
