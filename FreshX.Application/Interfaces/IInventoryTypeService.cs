using FreshX.Application.Dtos.InventoryType;

namespace FreshX.Application.Interfaces
{
    public interface IInventoryTypeService
    {
        Task<IReadOnlyList<InventoryTypeDto>> GetAllAsync(string? searchKeyword = null, CancellationToken cancellationToken = default);
        Task<InventoryTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<InventoryTypeDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<InventoryTypeDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<InventoryTypeDto> CreateAsync(InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task UpdateByIdAsync(int id, InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task UpdateByCodeAsync(string code, InventoryTypeCreateUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
