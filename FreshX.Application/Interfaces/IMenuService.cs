using FreshX.Application.Dtos.Menu;

namespace FreshX.Application.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuDto>> GetMenusAsync(string? searchKey = null);
        Task<MenuDto> GetMenuByIdAsync(int menuId);
    }

}

