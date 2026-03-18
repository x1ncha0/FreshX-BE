using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class PositionRepository(FreshXDbContext context) : IPositionRepository
    {
        public async Task<IReadOnlyList<Position>> GetAllAsync()
        {
            return await context.Positions
                .AsNoTracking()
                .OrderBy(position => position.Name)
                .ToListAsync();
        }

        public Task<Position?> GetByIdAsync(int id)
        {
            return context.Positions
                .AsNoTracking()
                .FirstOrDefaultAsync(position => position.Id == id);
        }
    }
}
