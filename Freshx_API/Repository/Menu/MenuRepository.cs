using AutoMapper;
using Freshx_API.Dtos.Menu;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository.Menu
{
    public class MenuRepository : IMenuRepository
    {
        private readonly FreshxDBContext _context;
        private readonly IMapper _mapper;

        public MenuRepository(FreshxDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MenuDto>> GetMenusAsync(string? searchKey = null)
        {
            var query = _context.Menus.AsQueryable();

         
            var menus = await query.ToListAsync();
            return _mapper.Map<List<MenuDto>>(menus);
        }

        public async Task<MenuDto> GetMenuByIdAsync(int menuId)
        {
            var menu = await _context.Menus.FindAsync(menuId);
            return _mapper.Map<MenuDto>(menu);
        }
    }

}
