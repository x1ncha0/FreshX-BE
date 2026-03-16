using Freshx_API.Dtos;

namespace Freshx_API.Interfaces.IReception
{
    public interface IReceptionService
    {
        Task<ReceptionDto?> GetByIdAsync(int id);
        Task<IEnumerable<ReceptionDto>> GetAllAsync();
        Task<ReceptionDto> AddAsync (CreateReceptionDto dto);
        Task UpdateAsync(CreateReceptionDto dto);
        Task DeleteAsync(int id);
    }

}
