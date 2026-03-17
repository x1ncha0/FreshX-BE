using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.UserAccount;
using FreshX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.Application.Interfaces.UserAccount
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

