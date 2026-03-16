using Freshx_API.Dtos.Menu;
using Freshx_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetMenus([FromQuery] string? searchKey = null)
        {
            var menus = await _menuService.GetMenusAsync(searchKey);
            return Ok(menus);
        }

        // GET: api/Menu/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDto>> GetMenuById(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }
    }

}
