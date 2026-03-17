using FreshX.Application.Dtos;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface IUserAccountManagementRepository
    {
        public Task<AppUser?> GetInformationAccoutUserById(string id);
        public Task<AppUser?> UpdateInformationAccountUserById(string id,UserAccountRequest request);
    }
}

