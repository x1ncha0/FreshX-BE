using Freshx_API.Dtos.Drugs;

public interface IDrugTypeService
{
    Task<List<DrugTypeDto?>> GetDrugTypeAsync(string? searchKeyword,
      DateTime? CreatetDate,
      DateTime? UpdatedDate,
      int? status);
    Task<DrugTypeDto?> GetDrugTypeByIdAsync(int id);
    Task<DrugTypeDto> CreateDrugTypeAsync(DrugTypeCreateDto createDto);
    Task<DrugTypeDto?> UpdateDrugTypeAsync(int id, DrugTypeUpdateDto updateDto);
    Task<bool> SoftDeleteDrugTypeAsync(int id);
    Task<bool> DeleteDrugTypeAsync(int id);
}
