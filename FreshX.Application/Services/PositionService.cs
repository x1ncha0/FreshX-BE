using AutoMapper;
using FreshX.Application.Dtos.Position;
using FreshX.Application.Interfaces;

namespace FreshX.Application.Services
{
    public class PositionService(IPositionRepository repository, IMapper mapper) : IPositionService
    {
        public async Task<IReadOnlyList<PositionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var positions = await repository.GetAllAsync();
            return mapper.Map<IReadOnlyList<PositionDto>>(positions);
        }
    }
}
