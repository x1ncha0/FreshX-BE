using FreshX.Application.Dtos.Menu;

namespace FreshX.Application.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<MenuDto>> GetMenusAsync(string? searchKey = null);
        Task<MenuDto> GetMenuByIdAsync(int menuId);
    }

}

