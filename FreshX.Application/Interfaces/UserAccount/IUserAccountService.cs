using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.UserAccount;

namespace FreshX.Application.Interfaces.UserAccount
{
    public interface IUserAccountService
    {
        Task<UserResponse> CreateAsync(AddingUserRequest request, CancellationToken cancellationToken = default);
        Task<CustomPageResponse<IEnumerable<UserResponse?>>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default);
        Task<UserResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<UserResponse?> UpdateAsync(string id, UpdatingUserRequest request, CancellationToken cancellationToken = default);
    }
}
