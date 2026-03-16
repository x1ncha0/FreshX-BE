using Freshx_API.Dtos.Auth.Role;
using Freshx_API.Interfaces.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Repository.Auth.RoleRepositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public async Task<IdentityRole?> CreateRole(AddingRole addingRole)
        {
            var role = new IdentityRole
            {
                Name = addingRole.Name
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) { return role; }
            return null;
        }
        public async Task<List<IdentityRole>> GetAllRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<IdentityRole?> GetRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }
    }
}
