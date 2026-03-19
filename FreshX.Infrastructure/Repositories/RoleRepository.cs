using FreshX.Application.Dtos.Auth.Role;
using FreshX.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreshX.Infrastructure.Repositories
{
    public class RoleRepository(RoleManager<IdentityRole> roleManager) : IRoleRepository
    {
        public async Task<IdentityRole?> CreateRole(AddingRole addingRoleDto)
        {
            var existingRole = await roleManager.FindByNameAsync(addingRoleDto.Name);
            if (existingRole is not null)
            {
                return null;
            }

            var role = new IdentityRole
            {
                Name = addingRoleDto.Name
            };

            var result = await roleManager.CreateAsync(role);
            return result.Succeeded ? role : null;
        }

        public Task<List<IdentityRole>> GetAllRoles() => roleManager.Roles.AsNoTracking().ToListAsync();

        public Task<IdentityRole?> GetRoleById(string roleId) => roleManager.FindByIdAsync(roleId);
    }
}
