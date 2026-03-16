using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.UserAccount;
using Freshx_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Interfaces.UserAccount
{
    public interface IUserAccountRepository
    {

        public Task<AppUser?> CreateUserAsync(AddingUserRequest request);
        public Task<AppUser?> GetUserByIdAsync(string id);
        public Task<CustomPageResponse<IEnumerable<UserResponse?>>> GetUsersAsync(Parameters parameters);
        public Task<AppUser?> DeleteUserByIdAsync(string id);      
        public Task<AppUser?> UpdateUserByIdAsync(string id, UpdatingUserRequest request);
    }
}
