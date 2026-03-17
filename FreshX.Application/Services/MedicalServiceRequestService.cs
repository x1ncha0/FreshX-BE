using AutoMapper;
using FreshX.Application.Dtos;
using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class MedicalServiceRequestService(IMedicalServiceRequestRepository repository, IMapper mapper) : IMedicalServiceRequestService
{
    public async Task<MedicalServiceRequestDto> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return ToDto(entity);
    }

    public async Task<IEnumerable<MedicalServiceRequestDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(ToDto).ToList();
    }

    public async Task<MedicalServiceRequestDto> AddAsync(CreateMedicalServiceRequestDto medicalServiceRequestDto)
    {
        var entity = mapper.Map<MedicalServiceRequest>(medicalServiceRequestDto);
        entity.CreatedDate = DateTime.UtcNow;
        var created = await repository.AddAsync(entity);
        return ToDto(created);
    }

    public async Task<MedicalServiceRequestDto> UpdateAsync(int id, UpdateMedicalServiceRequestDto medicalServiceRequestDto)
    {
        var entity = await repository.GetByIdAsync(id);
        entity.RequestTime = medicalServiceRequestDto.RequestTime;
        entity.ServiceId = medicalServiceRequestDto.ServiceId;
        entity.Results = medicalServiceRequestDto.Results;
        entity.ReceptionId = medicalServiceRequestDto.ReceptionId;
        entity.Quantity = medicalServiceRequestDto.Quantity;
        entity.Discount = medicalServiceRequestDto.discount;
        entity.ServiceTotalAmount = medicalServiceRequestDto.ServiceTotalAmount;
        entity.DepartmentId = medicalServiceRequestDto.DepartmentId;
        entity.IsApproved = medicalServiceRequestDto.IsApproved;
        entity.Status = medicalServiceRequestDto.Status;
        entity.AssignedById = medicalServiceRequestDto.AssignedById;
        entity.UpdatedDate = DateTime.UtcNow;

        var updated = await repository.UpdateAsync(entity);
        return ToDto(updated);
    }

    public Task DeleteAsync(int id) => repository.DeleteAsync(id);

    private static MedicalServiceRequestDto ToDto(MedicalServiceRequest entity) => new()
    {
        MedicalServiceRequestId = entity.Id,
        RequestTime = entity.RequestTime,
        ReceptionId = entity.ReceptionId,
        Quantity = entity.Quantity,
        ServiceId = entity.ServiceId,
        Results = entity.Results,
        ServiceTotalAmount = entity.ServiceTotalAmount,
        discount = entity.Discount,
        IsApproved = entity.IsApproved,
        DepartmentId = entity.DepartmentId,
        Status = entity.Status,
        AssignedById = entity.AssignedById,
        CreatedBy = int.TryParse(entity.CreatedBy, out var createdBy) ? createdBy : null,
        CreatedDate = entity.CreatedDate,
        UpdatedBy = entity.UpdatedBy,
        UpdatedDate = entity.UpdatedDate
    };
}