using AutoMapper;
using Freshx_API.Dtos.Drugs;
using Freshx_API.Models;
using Freshx_API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DrugTypeService : IDrugTypeService
{
    private readonly IDrugTypeRepository _drugTypeRepository;
    private readonly IMapper _mapper;

    public DrugTypeService(IDrugTypeRepository drugTypeRepository, IMapper mapper)
    {
        _drugTypeRepository = drugTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<DrugTypeDto?>> GetDrugTypeAsync(string? searchKeyword,
      DateTime? CreatetDate,
      DateTime? UpdatedDate,
      int? status)
    {
        var drugType = await _drugTypeRepository.GetDrugTypeAsync(searchKeyword, CreatetDate, UpdatedDate, status);
        return drugType == null ? null : _mapper.Map<List<DrugTypeDto>>(drugType);
    }
    public async Task<DrugTypeDto?> GetDrugTypeByIdAsync(int id)
    {
        var drugType = await _drugTypeRepository.GetDrugTypeByIdAsync(id);
        return drugType == null ? null : _mapper.Map<DrugTypeDto>(drugType);
    }

    public async Task<DrugTypeDto> CreateDrugTypeAsync(DrugTypeCreateDto createDto)
    {
        var drugType = _mapper.Map<DrugType>(createDto);
        var createdDrugType = await _drugTypeRepository.CreateDrugTypeAsync(drugType);
        return _mapper.Map<DrugTypeDto>(createdDrugType);
    }

    public async Task<DrugTypeDto?> UpdateDrugTypeAsync(int id, DrugTypeUpdateDto updateDto)
    {
        var drugType = await _drugTypeRepository.GetDrugTypeByIdAsync(id);
        if (drugType == null)
        {
            return null;
        }
        _mapper.Map(updateDto, drugType); // Update existing entity with new data
        var updatedDrugType = await _drugTypeRepository.UpdateDrugTypeAsync(drugType);
        return _mapper.Map<DrugTypeDto>(updatedDrugType);
    }
    public async Task<bool> SoftDeleteDrugTypeAsync(int id)
    {
        var drugType = await _drugTypeRepository.GetDrugTypeByIdAsync(id);
        if (drugType == null)
        {
            return false;
        }
        return await _drugTypeRepository.SoftDeleteDrugTypeAsync(id);
    }
    public async Task<bool> DeleteDrugTypeAsync(int id)
    {
        var drugType = await _drugTypeRepository.GetDrugTypeByIdAsync(id);
        if (drugType == null)
        {
            return false;
        }
        return await _drugTypeRepository.DeleteDrugTypeAsync(id);
    }
}
