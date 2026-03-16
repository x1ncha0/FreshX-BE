using Freshx_API.Dtos.Auth.Role;
using Microsoft.AspNetCore.Identity;

namespace Freshx_API.Interfaces.Auth
{
    public interface IRoleRepository
    {
        public Task<IdentityRole?> CreateRole(AddingRole addingRoleDto);
        public Task<List<IdentityRole>> GetAllRoles();
        public Task<IdentityRole?> GetRoleById(string roleId);
    }
}
