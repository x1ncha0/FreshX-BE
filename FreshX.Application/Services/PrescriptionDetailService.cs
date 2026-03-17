using FreshX.Application.Dtos.Prescription;
using FreshX.Application.Interfaces.IPrescription;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class PrescriptionDetailService(IPrescriptionDetailRepository repository) : IPrescriptionDetailService
{
    public async Task AddAsync(CreatePrescriptionDetailDto detailDto)
    {
        var detail = new PrescriptionDetail
        {
            PrescriptionId = detailDto.PrescriptionId,
            DrugCatalogId = detailDto.DrugCatalogId,
            MorningDose = detailDto.MorningDose,
            NoonDose = detailDto.NoonDose,
            AfternoonDose = detailDto.AfternoonDose,
            EveningDose = detailDto.EveningDose,
            DaysOfSupply = detailDto.DaysOfSupply,
            Quantity = detailDto.Quantity,
            TotalAmount = detailDto.TotalAmount,
            Note = detailDto.Note
        };

        await repository.AddAsync(detail);
    }

    public async Task UpdateAsync(UpdatePrescriptionDetailDto detailDto)
    {
        var existing = await repository.GetByIdAsync(detailDto.PrescriptionDetailId) ?? throw new KeyNotFoundException($"Prescription detail {detailDto.PrescriptionDetailId} was not found.");
        existing.PrescriptionId = detailDto.PrescriptionId;
        existing.DrugCatalogId = detailDto.DrugCatalogId;
        existing.MorningDose = detailDto.MorningDose;
        existing.NoonDose = detailDto.NoonDose;
        existing.AfternoonDose = detailDto.AfternoonDose;
        existing.EveningDose = detailDto.EveningDose;
        existing.DaysOfSupply = detailDto.DaysOfSupply;
        existing.Quantity = detailDto.Quantity;
        existing.TotalAmount = detailDto.TotalAmount;
        existing.Note = detailDto.Note;
        existing.UpdatedDate = DateTime.UtcNow;

        await repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var detail = await repository.GetByIdAsync(id);
        if (detail is not null)
        {
            await repository.DeleteAsync(detail);
        }
    }

    public async Task<List<DetailDto>> GetByPrescriptionIdAsync(int prescriptionId)
    {
        var details = await repository.GetByPrescriptionIdAsync(prescriptionId);
        return details.Where(static d => d is not null).Select(static d => ToDto(d!)).ToList();
    }

    private static DetailDto ToDto(PrescriptionDetail detail) => new()
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
    };
}