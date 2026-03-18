using FreshX.Application.Dtos.Auth.Account;

namespace FreshX.Application.Interfaces.Auth;

public interface IAccountService
{
    Task<RegisterResponse> RegisterAsync(AddingRegister request, CancellationToken cancellationToken = default);
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default);
    Task VerifyResetCodeAsync(VerifyCodeRequest request, CancellationToken cancellationToken = default);
    Task ResetPasswordAsync(string code, ResetPasswordRequest request, CancellationToken cancellationToken = default);
    Task SetPasswordAsync(SetPasswordRequest request, CancellationToken cancellationToken = default);
    Task<AccountDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AccountDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AccountDto?> UpdateAsync(string id, UpdatingAccountRequest request, CancellationToken cancellationToken = default);
}
