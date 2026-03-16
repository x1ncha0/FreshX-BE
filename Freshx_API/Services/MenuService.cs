using Freshx_API.Dtos.Menu;
using Freshx_API.Interfaces;
using Freshx_API.Repository.Menu;

namespace Freshx_API.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<MenuDto>> GetMenusAsync(string? searchKey = null)
        {
            return await _menuRepository.GetMenusAsync(searchKey);
        }

        public async Task<MenuDto> GetMenuByIdAsync(int menuId)
        {
            return await _menuRepository.GetMenuByIdAsync(menuId);
        }
    }

}
