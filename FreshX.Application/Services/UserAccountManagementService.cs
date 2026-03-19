using AutoMapper;
using FreshX.Application.Dtos;
using FreshX.Application.Dtos.UserAccountManagement;
using FreshX.Application.Interfaces;
using FreshX.Application.Interfaces.UserAccountManagement;

namespace FreshX.Application.Services
{
    public class UserAccountManagementService(
        IUserAccountManagementRepository repository,
        IMapper mapper) : IUserAccountManagementService
    {
        public async Task<UserAccountResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var account = await repository.GetInformationAccoutUserById(id);
            return account is null ? null : mapper.Map<UserAccountResponse>(account);
        }

        public async Task<UserAccountResponse?> UpdateAsync(string id, UserAccountRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var account = await repository.UpdateInformationAccountUserById(id, request);
            return account is null ? null : mapper.Map<UserAccountResponse>(account);
        }
    }
}
