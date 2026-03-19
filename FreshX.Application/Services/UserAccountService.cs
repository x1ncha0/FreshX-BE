using AutoMapper;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.UserAccount;
using FreshX.Application.Interfaces.UserAccount;

namespace FreshX.Application.Services
{
    public class UserAccountService(IUserAccountRepository userAccountRepository, IMapper mapper) : IUserAccountService
    {
        public async Task<UserResponse> CreateAsync(AddingUserRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await userAccountRepository.CreateUserAsync(request)
                ?? throw new InvalidOperationException("The user could not be created.");

            return mapper.Map<UserResponse>(user);
        }

        public Task<CustomPageResponse<IEnumerable<UserResponse?>>> GetAsync(Parameters parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return userAccountRepository.GetUsersAsync(parameters);
        }

        public async Task<UserResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await userAccountRepository.GetUserByIdAsync(id);
            return user is null ? null : mapper.Map<UserResponse>(user);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await userAccountRepository.DeleteUserByIdAsync(id);
            if (user is null)
            {
                throw new KeyNotFoundException($"User {id} was not found.");
            }
        }

        public async Task<UserResponse?> UpdateAsync(string id, UpdatingUserRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await userAccountRepository.UpdateUserByIdAsync(id, request);
            return user is null ? null : mapper.Map<UserResponse>(user);
        }
    }
}
