using Freshx_API.Dtos;
using Freshx_API.Models;

namespace Freshx_API.Interfaces
{
    public interface IUserAccountManagementRepository
    {
        public Task<AppUser?> GetInformationAccoutUserById(string id);
        public Task<AppUser?> UpdateInformationAccountUserById(string id,UserAccountRequest request);
    }
}
