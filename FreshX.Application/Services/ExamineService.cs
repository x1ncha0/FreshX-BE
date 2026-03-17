using AutoMapper;
using FreshX.Application.Dtos.ExamineDtos;
using FreshX.Application.Dtos.Prescription;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.IPrescription;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class ExamineService(IExamineRepository repository, IMapper mapper, IPrescriptionService prescriptionService) : IExamineService
{
    public async Task<ExamineResponseDto> AddAsync(CreateExamDto dto)
    {
        var examine = mapper.Map<Examine>(dto);
        examine.CreatedDate = DateTime.UtcNow;
        var created = await repository.AddAsync(examine);
        return ToResponse(created, mapper.Map<PrescriptionDto?>(created.Prescription));
    }

    public async Task<ExamineResponseDto?> GetByIdAsync(int id)
    {
        var examine = await repository.GetByIdAsync(id);
        return examine is null ? null : ToResponse(examine, mapper.Map<PrescriptionDto?>(examine.Prescription));
    }

    public async Task<IEnumerable<ExamineResponseDto>> GetAllAsync()
    {
        var examines = await repository.GetAllAsync();
        return examines.Select(x => ToResponse(x, mapper.Map<PrescriptionDto?>(x.Prescription))).ToList();
    }

    public async Task UpdateAsync(int id, ExamineRequestDto dto)
    {
        var examine = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Examine {id} was not found.");

        if (dto.Prescription is not null)
        {
            if (dto.PrescriptionId.HasValue)
            {
                var updatePrescription = mapper.Map<UpdatePrescriptionDto>(dto.Prescription);
                updatePrescription.PrescriptionId = dto.PrescriptionId.Value;
                await prescriptionService.UpdateAsync(updatePrescription);
            }
            else
            {
                var createdPrescription = await prescriptionService.AddAsync(dto.Prescription);
                examine.PrescriptionId = createdPrescription.PrescriptionId;
            }
        }

        mapper.Map(dto, examine);
        examine.UpdatedDate = DateTime.UtcNow;
        await repository.UpdateAsync(examine);
    }

    public Task DeleteAsync(int id) => repository.DeleteAsync(id);

    private static ExamineResponseDto ToResponse(Examine examine, PrescriptionDto? prescription) => new()
    {
        ExamineId = examine.Id,
        ReceptionId = examine.ReceptionId,
        CreatedDate = examine.CreatedDate,
        CreatedTime = examine.CreatedTime,
        RespiratoryRate = examine.RespiratoryRate,
        Bmi = examine.Bmi,
        Symptoms = examine.Symptoms,
        ICDCatalogId = examine.ICDCatalogId,
        DiagnosisDictionaryId = examine.DiagnosisDictionaryId,
        Diagnosis = examine.Diagnosis,
        Conclusion = examine.Conclusion,
        MedicalAdvice = examine.MedicalAdvice,
        PrescriptionId = examine.PrescriptionId,
        TemplatePrescriptionId = examine.TemplatePrescriptionId,
        CreatedById = examine.CreatedBy,
        UpdatedDate = examine.UpdatedDate,
        UpdatedBy = int.TryParse(examine.UpdatedBy, out var updatedBy) ? updatedBy : null,
        PrescriptionNumber = examine.PrescriptionNumber,
        FollowUpAppointment = examine.FollowUpAppointment,
        Comorbidities = examine.Comorbidities,
        ComorbidityCodes = examine.ComorbidityCodes,
        ComorbidityNames = examine.ComorbidityNames,
        MedicalHistory = examine.MedicalHistory,
        ExaminationDetails = examine.ExaminationDetails,
        LabSummary = examine.LabSummary,
        TreatmentDetails = examine.TreatmentDetails,
        FollowUpAppointmentNote = examine.FollowUpAppointmentNote,
        ReasonForVisit = examine.ReasonForVisit,
        IsPaid = examine.IsPaid,
        ExaminationNote = examine.ExaminationNote,
        IsDeleted = examine.IsDeleted ? 1 : 0,
        Temperature = examine.Temperature,
        Height = examine.Height,
        Weight = examine.Weight,
        BloodPressureSystolic = examine.BloodPressureSystolic,
        BloodPressureDiastolic = examine.BloodPressureDiastolic,
        HeartRate = examine.HeartRate,
        OxygenSaturation = examine.OxygenSaturation,
        VisionLeft = examine.VisionLeft,
        VisionRight = examine.VisionRight,
        SkinCondition = examine.SkinCondition,
        OtherPhysicalFindings = examine.OtherPhysicalFindings,
        Prescriptions = prescription
    };
}