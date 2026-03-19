using FreshX.Application.Dtos.Country;
using FreshX.Application.Dtos.DrugCatalog;
using FreshX.Application.Dtos.Drugs;
using FreshX.Application.Dtos.Supplier;
using FreshX.Application.Dtos.UnitOfMeasure;

namespace FreshX.Application.Interfaces
{
    public interface IDrugCatalogService
    {
        Task<IReadOnlyList<DrugCatalogDetailDto>> GetAllAsync(string? searchKeyword, DateTime? createdDate, DateTime? updatedDate, int? status, CancellationToken cancellationToken = default);
        Task<DrugCatalogDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<DrugCatalogDetailDto> CreateAsync(DrugCatalogCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, DrugCatalogCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<DrugTypeDto?> GetDrugTypeByIdAsync(int? drugTypeId, CancellationToken cancellationToken = default);
        Task<SupplierDetailDto?> GetManufacturerByIdAsync(int? manufacturerId, CancellationToken cancellationToken = default);
        Task<UnitOfMeasureDetailDto?> GetUnitOfMeasureByIdAsync(int? unitOfMeasureId, CancellationToken cancellationToken = default);
        Task<CountryDto?> GetCountryByIdAsync(int? countryId, CancellationToken cancellationToken = default);
    }
}
