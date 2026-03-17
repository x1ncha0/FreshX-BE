using FreshX.Application.Dtos.Prescription;
using FreshX.Application.Interfaces.IPrescription;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class PrescriptionService(IPrescriptionRepository repository, IPrescriptionDetailService detailService) : IPrescriptionService
{
    public async Task<List<PrescriptionDto>> GetAllAsync(string? searchKey)
    {
        var prescriptions = await repository.GetAllAsync(searchKey);
        return prescriptions.Select(ToDto).ToList();
    }

    public async Task<PrescriptionDto?> GetByIdAsync(int id)
    {
        var prescription = await repository.GetByIdAsync(id);
        return prescription is null ? null : ToDto(prescription);
    }

    public async Task<PrescriptionDto> AddAsync(CreatePrescriptionDto prescriptionDto)
    {
        var prescription = new Prescription
        {
            MedicalExaminationId = prescriptionDto.MedicalExaminationId,
            TotalAmount = prescriptionDto.TotalAmount,
            IsPaid = prescriptionDto.IsPaid,
            Note = prescriptionDto.Note,
            CreatedDate = DateTime.UtcNow
        };

        var created = await repository.AddAsync(prescription);

        foreach (var detailDto in prescriptionDto.Details ?? [])
        {
            detailDto.PrescriptionId = created.Id;
            await detailService.AddAsync(detailDto);
        }

        var reloaded = await repository.GetByIdAsync(created.Id) ?? created;
        return ToDto(reloaded);
    }

    public async Task<PrescriptionDto> UpdateAsync(UpdatePrescriptionDto prescriptionDto)
    {
        var existing = await repository.GetByIdAsync(prescriptionDto.PrescriptionId) ?? throw new KeyNotFoundException($"Prescription {prescriptionDto.PrescriptionId} was not found.");
        existing.MedicalExaminationId = prescriptionDto.MedicalExaminationId;
        existing.TotalAmount = prescriptionDto.TotalAmount;
        existing.IsPaid = prescriptionDto.IsPaid;
        existing.Note = prescriptionDto.Note;
        existing.UpdatedDate = DateTime.UtcNow;

        var updated = await repository.UpdateAsync(existing);

        foreach (var detailDto in prescriptionDto.Details ?? [])
        {
            detailDto.PrescriptionId = updated.Id;
            await detailService.UpdateAsync(detailDto);
        }

        var reloaded = await repository.GetByIdAsync(updated.Id) ?? updated;
        return ToDto(reloaded);
    }

    public Task DeleteAsync(int id) => repository.DeleteAsync(id);

    private static PrescriptionDto ToDto(Prescription prescription) => new()
    {
        PrescriptionId = prescription.Id,
        MedicalExaminationId = prescription.MedicalExaminationId,
        TotalAmount = prescription.TotalAmount,
        IsPaid = prescription.IsPaid,
        Note = prescription.Note,
        Details = prescription.PrescriptionDetails?.Select(static detail => new DetailDto
        {
            PrescriptionDetailId = detail.Id,
            PrescriptionId = detail.PrescriptionId,
            DrugCatalogId = detail.DrugCatalogId,
            MorningDose = detail.MorningDose,
            NoonDose = detail.NoonDose,
            AfternoonDose = detail.AfternoonDose,
            EveningDose = detail.EveningDose,
            DaysOfSupply = detail.DaysOfSupply,
            Quantity = detail.Quantity,
            TotalAmount = detail.TotalAmount,
            Note = detail.Note
        }).ToList()
    };
}