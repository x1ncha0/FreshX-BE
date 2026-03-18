using AutoMapper;
using FreshX.Application.Dtos.Auth.Role;
using FreshX.Application.Interfaces.Auth;

namespace FreshX.Application.Services
{
    public class RoleService(IRoleRepository roleRepository, IMapper mapper) : IRoleService
    {
        public async Task<RoleResponse> CreateAsync(AddingRole request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var role = await roleRepository.CreateRole(request)
                ?? throw new InvalidOperationException("Role already exists or could not be created.");

            return mapper.Map<RoleResponse>(role);
        }

        public async Task<IReadOnlyList<RoleResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var roles = await roleRepository.GetAllRoles();
            return mapper.Map<IReadOnlyList<RoleResponse>>(roles);
        }

        public async Task<RoleResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var role = await roleRepository.GetRoleById(id);
            return role is null ? null : mapper.Map<RoleResponse>(role);
        }
    }
}
