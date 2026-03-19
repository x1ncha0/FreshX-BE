using AutoMapper;
using FreshX.Application.Dtos;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Services;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class LabResultService(ILabResultRepository repository, IMapper mapper) : ILabResultService
{
    public async Task<IEnumerable<LabResultDto>> GetAllAsync(string? searchKey = null)
    {
        var labResults = await repository.GetAllAsync(searchKey);
        return labResults.Select(ToDto).ToList();
    }

    public async Task<LabResultDto?> GetByIdAsync(int id)
    {
        var labResult = await repository.GetByIdAsync(id);
        return labResult is null ? null : ToDto(labResult);
    }

    public async Task AddAsync(CreateLabResultDto labResultDto)
    {
        var entity = mapper.Map<LabResult>(labResultDto);
        entity.CreatedDate = DateTime.UtcNow;
        entity.IsPaid = false;
        entity.IsDeleted = false;
        entity.CreatedBy = labResultDto.CreatedBy?.ToString();
        entity.ExecutionDate = entity.ExecutionTime?.Date;
        await repository.AddAsync(entity);
    }

    public async Task UpdateAsync(UpdateLabResultDto labResultDto)
    {
        var existing = await repository.GetByIdAsync(labResultDto.LabResultId) ?? throw new KeyNotFoundException($"Lab result {labResultDto.LabResultId} was not found.");
        existing.ExecutionTime = labResultDto.ExecutionTime;
        existing.ExecutionDate = labResultDto.ExecutionTime?.Date;
        existing.ReceptionId = labResultDto.ReceptionId;
        existing.TechnicianId = labResultDto.TechnicianId;
        existing.ConcludingDoctorId = labResultDto.ConcludingDoctorId;
        existing.Conclusion = labResultDto.Conclusion;
        existing.Description = labResultDto.Description;
        existing.Note = labResultDto.Note;
        existing.SampleTypeId = labResultDto.SampleTypeId;
        existing.SampleQuality = labResultDto.SampleQuality;
        existing.SampleCollectionLocation = labResultDto.SampleCollectionLocation;
        existing.SampleReceivedTime = labResultDto.SampleReceivedTime;
        existing.SampleCollectionTime = labResultDto.SampleCollectionTime;
        existing.UpdatedDate = DateTime.UtcNow;
        existing.UpdatedBy = labResultDto.CreatedBy?.ToString();
        await repository.UpdateAsync(existing);
    }

    public Task DeleteAsync(int id) => repository.DeleteAsync(id);

    private static LabResultDto ToDto(LabResult entity) => new()
    {
        LabResultId = entity.Id,
        ExecutionDate = entity.ExecutionDate,
        ExecutionTime = entity.ExecutionTime,
        ReceptionId = entity.ReceptionId,
        TechnicianId = entity.TechnicianId,
        ConcludingDoctorId = entity.ConcludingDoctorId,
        Conclusion = entity.Conclusion,
        Description = entity.Description,
        Note = entity.Note,
        SampleTypeId = entity.SampleTypeId,
        SampleQuality = entity.SampleQuality,
        CreatedBy = int.TryParse(entity.CreatedBy, out var createdBy) ? createdBy : null,
        CreatedDate = entity.CreatedDate,
        UpdatedBy = int.TryParse(entity.UpdatedBy, out var updatedBy) ? updatedBy : null,
        UpdatedDate = entity.UpdatedDate,
        IsPaid = entity.IsPaid,
        IsDeleted = entity.IsDeleted ? 1 : 0,
        SampleCollectionLocation = entity.SampleCollectionLocation,
        SampleReceivedTime = entity.SampleReceivedTime,
        SampleCollectionTime = entity.SampleCollectionTime
    };
}
