using FreshX.Application.Dtos.Auth.Role;

namespace FreshX.Application.Interfaces.Auth
{
    public interface IRoleService
    {
        Task<RoleResponse> CreateAsync(AddingRole request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RoleResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RoleResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}
