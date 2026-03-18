using FreshX.Application.Dtos.Position;

namespace FreshX.Application.Interfaces
{
    public interface IPositionService
    {
        Task<IReadOnlyList<PositionDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
