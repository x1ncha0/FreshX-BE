using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class CountryRepository(
        FreshXDbContext context,
        ITokenRepository tokenRepository) : ICountryRepository
    {
        public async Task<IEnumerable<Country>> GetAllAsync(
            string? searchKeyword,
            DateTime? createdDate,
            DateTime? updatedDate,
            bool? isSuspended,
            int? isDeleted)
        {
            var query = context.Countries.AsNoTracking().AsQueryable();

            query = isDeleted.HasValue
                ? query.Where(c => c.IsDeleted == (isDeleted.Value == 1))
                : query.Where(c => !c.IsDeleted);

            if (isSuspended.HasValue)
            {
                query = query.Where(c => c.IsSuspended == isSuspended.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(c =>
                    (c.Name != null && c.Name.Contains(searchKeyword)) ||
                    (c.Code != null && c.Code.Contains(searchKeyword)) ||
                    (c.ShortName != null && c.ShortName.Contains(searchKeyword)));
            }

            if (createdDate.HasValue)
            {
                query = query.Where(c => c.CreatedAt >= createdDate.Value);
            }

            if (updatedDate.HasValue)
            {
                query = query.Where(c => c.UpdatedAt <= updatedDate.Value);
            }

            return await query.OrderBy(c => c.Name).ToListAsync();
        }

        public Task<Country?> GetByIdAsync(int id)
        {
            return context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<Country> CreateAsync(Country entity)
        {
            await context.Countries.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Country entity)
        {
            context.Countries.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.Countries.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
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
