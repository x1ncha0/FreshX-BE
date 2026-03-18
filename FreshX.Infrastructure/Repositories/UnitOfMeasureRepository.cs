using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class UnitOfMeasureRepository(
        FreshXDbContext context,
        ITokenRepository tokenRepository) : IUnitOfMeasureRepository
    {
        public async Task<IEnumerable<UnitOfMeasure>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            int? isSuspended,
            int? isDeleted)
        {
            var query = context.UnitOfMeasures.AsNoTracking().AsQueryable();

            query = isDeleted.HasValue
                ? query.Where(u => u.IsDeleted == (isDeleted.Value == 1))
                : query.Where(u => !u.IsDeleted);

            if (isSuspended.HasValue)
            {
                query = query.Where(u => u.IsSuspended == (isSuspended.Value == 1));
            }

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(u =>
                    (u.Name != null && u.Name.Contains(searchKeyword)) ||
                    (u.Code != null && u.Code.Contains(searchKeyword)));
            }

            if (createdDate.HasValue)
            {
                query = query.Where(u => u.CreatedAt >= createdDate.Value);
            }

            if (updatedDate.HasValue)
            {
                query = query.Where(u => u.UpdatedAt <= updatedDate.Value);
            }

            return await query.OrderBy(u => u.Name).ToListAsync();
        }

        public Task<UnitOfMeasure?> GetByIdAsync(int id)
        {
            return context.UnitOfMeasures
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public Task<UnitOfMeasure?> GetByCodeAsync(string code)
        {
            return context.UnitOfMeasures
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Code == code && !u.IsDeleted);
        }

        public Task<UnitOfMeasure?> GetNameAsync(string name)
        {
            var normalizedName = name.Trim().ToUpperInvariant();
            return context.UnitOfMeasures
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name != null && u.Name.ToUpper() == normalizedName && !u.IsDeleted);
        }

        public async Task<UnitOfMeasure> CreateAsync(UnitOfMeasure entity)
        {
            await context.UnitOfMeasures.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(UnitOfMeasure entity)
        {
            context.UnitOfMeasures.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.UnitOfMeasures.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (entity is null)
            {
                return;
            }

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = tokenRepository.GetUserIdFromToken();
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsyncCode(string code)
        {
            var entity = await context.UnitOfMeasures.FirstOrDefaultAsync(u => u.Code == code && !u.IsDeleted);
            if (entity is null)
            {
                return;
            }

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = tokenRepository.GetUserIdFromToken();
            await context.SaveChangesAsync();
        }
    }
}
