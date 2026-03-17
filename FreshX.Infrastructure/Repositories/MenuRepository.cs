using FreshX.Application.Dtos.Menu;
using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories;

public class MenuRepository(FreshXDbContext context) : IMenuRepository
{
    public async Task<List<MenuDto>> GetMenusAsync(string? searchKey = null)
    {
        var query = context.Menus.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchKey))
        {
            query = query.Where(x =>
                (x.Name != null && x.Name.Contains(searchKey)) ||
                (x.Path != null && x.Path.Contains(searchKey)));
        }

        var menus = await query.OrderBy(x => x.Name).ToListAsync();
        return menus.Select(ToDto).ToList();
    }

    public async Task<MenuDto> GetMenuByIdAsync(int menuId)
    {
        var menu = await context.Menus.AsNoTracking().FirstOrDefaultAsync(x => x.Id == menuId)
            ?? throw new KeyNotFoundException($"Menu {menuId} was not found.");

        return ToDto(menu);
    }

    private static MenuDto ToDto(Menu menu) => new()
    {
        MenuId = menu.Id,
        Code = menu.Path ?? string.Empty,
        Name = menu.Name ?? string.Empty,
        ParentMenuId = menu.ParentMenuId
    };
}