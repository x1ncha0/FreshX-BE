using FreshX.Application.Dtos.Auth.Account;
using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces.Auth
{
    public interface IAccountRepository
    {
        public Task<AppUser?> RegisterAccount(AddingRegister registerDto);
        public Task<LoginResponse> LoginAccount(LoginRequest loginRequest);
        public Task<bool> EmailExist(string email);
        public Task<AccountDto?> GetAccountInformationByIdAsync(string id);
        public Task<AccountDto?> UpdateAccountAsync(string id,UpdatingAccountRequest request);
        public Task<List<AccountDto>> GetAllAccountsAsync();
        public Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request);
        public Task VerifyResetCodeAsync(VerifyCodeRequest request);
        public Task ResetPasswordAsync(string code, ResetPasswordRequest request);
        public Task SetPasswordAsync(SetPasswordRequest request);
    }
}

