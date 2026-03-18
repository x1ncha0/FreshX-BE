using FreshX.Application.Dtos;
using FreshX.Application.Dtos.UserAccountManagement;

namespace FreshX.Application.Interfaces.UserAccountManagement
{
    public interface IUserAccountManagementService
    {
        Task<UserAccountResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<UserAccountResponse?> UpdateAsync(string id, UserAccountRequest request, CancellationToken cancellationToken = default);
    }
}
