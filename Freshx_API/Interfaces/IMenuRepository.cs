using Freshx_API.Dtos.Menu;

namespace Freshx_API.Repository.Menu
{
    public interface IMenuRepository
    {
        Task<List<MenuDto>> GetMenusAsync(string? searchKey = null);
        Task<MenuDto> GetMenuByIdAsync(int menuId);
    }

}
