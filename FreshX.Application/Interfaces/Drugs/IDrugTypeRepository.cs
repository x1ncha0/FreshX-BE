using System.Threading.Tasks;
using FreshX.Domain.Entities;

public interface IDrugTypeRepository
{
    Task<List<DrugType?>> GetDrugTypeAsync(string? searchKeyword,
      DateTime? CreatetDate,
      DateTime? UpdatedDate,
      int? status);
    Task<DrugType?> GetDrugTypeByIdAsync(int id);
    Task<DrugType> CreateDrugTypeAsync(DrugType drugType);
    Task<DrugType?> UpdateDrugTypeAsync(DrugType drugType);
    Task<bool> SoftDeleteDrugTypeAsync(int id);
    Task<bool> DeleteDrugTypeAsync(int id);
}

