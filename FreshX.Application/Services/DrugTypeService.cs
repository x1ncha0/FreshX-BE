using FreshX.Application.Dtos.Drugs;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class DrugTypeService(IDrugTypeRepository drugTypeRepository) : IDrugTypeService
{
    public async Task<List<DrugTypeDto?>> GetDrugTypeAsync(string? searchKeyword, DateTime? createtDate, DateTime? updatedDate, int? status)
    {
        var drugTypes = await drugTypeRepository.GetDrugTypeAsync(searchKeyword, createtDate, updatedDate, status);
        return drugTypes.Select(static x => x is null ? null : ToDto(x)).ToList();
    }

    public async Task<DrugTypeDto?> GetDrugTypeByIdAsync(int id)
    {
        var drugType = await drugTypeRepository.GetDrugTypeByIdAsync(id);
        return drugType is null ? null : ToDto(drugType);
    }

    public async Task<DrugTypeDto> CreateDrugTypeAsync(DrugTypeCreateDto createDto)
    {
        var entity = new DrugType
        {
            Code = createDto.Code,
            Name = createDto.Name,
            IsSuspended = createDto.IsSuspended == 1,
            IsDeleted = createDto.IsDeleted == 1,
            CreatedDate = createDto.CreatedDate ?? DateTime.UtcNow,
            UpdatedDate = createDto.UpdatedDate,
            CreatedBy = createDto.CreatedBy?.ToString()
        };

        var created = await drugTypeRepository.CreateDrugTypeAsync(entity);
        return ToDto(created);
    }

    public async Task<DrugTypeDto?> UpdateDrugTypeAsync(int id, DrugTypeUpdateDto updateDto)
    {
        var entity = await drugTypeRepository.GetDrugTypeByIdAsync(id);
        if (entity is null)
        {
            return null;
        }

        entity.Code = updateDto.Code;
        entity.Name = updateDto.Name;
        entity.IsSuspended = updateDto.IsSuspended == 1;
        entity.IsDeleted = updateDto.IsDeleted == 1;
        entity.UpdatedDate = updateDto.UpdatedDate ?? DateTime.UtcNow;
        entity.UpdatedBy = updateDto.UpdatedBy?.ToString();

        var updated = await drugTypeRepository.UpdateDrugTypeAsync(entity);
        return updated is null ? null : ToDto(updated);
    }

    public Task<bool> SoftDeleteDrugTypeAsync(int id) => drugTypeRepository.SoftDeleteDrugTypeAsync(id);

    public Task<bool> DeleteDrugTypeAsync(int id) => drugTypeRepository.DeleteDrugTypeAsync(id);

    private static DrugTypeDto ToDto(DrugType entity) => new()
    {
        DrugTypeId = entity.Id,
        Code = entity.Code,
        Name = entity.Name,
        IsSuspended = entity.IsSuspended ? 1 : 0,
        IsDeleted = entity.IsDeleted ? 1 : 0,
        CreatedDate = entity.CreatedDate,
        CreatedBy = int.TryParse(entity.CreatedBy, out var createdBy) ? createdBy : null,
        UpdatedDate = entity.UpdatedDate,
        UpdatedBy = int.TryParse(entity.UpdatedBy, out var updatedBy) ? updatedBy : null
    };
}