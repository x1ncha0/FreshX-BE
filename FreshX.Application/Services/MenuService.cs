using FreshX.Application.Dtos.Menu;
using FreshX.Application.Interfaces;

namespace FreshX.Application.Services;

public class MenuService(IMenuRepository menuRepository) : IMenuService
{
    public Task<List<MenuDto>> GetMenusAsync(string? searchKey = null) => menuRepository.GetMenusAsync(searchKey);

    public Task<MenuDto> GetMenuByIdAsync(int menuId) => menuRepository.GetMenuByIdAsync(menuId);
}