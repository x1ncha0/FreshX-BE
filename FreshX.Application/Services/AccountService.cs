using AutoMapper;
using FreshX.Application.Dtos.Auth.Account;
using FreshX.Application.Interfaces.Auth;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class AccountService(
    IAccountRepository accountRepository,
    ITokenRepository tokenRepository,
    IMapper mapper) : IAccountService
{
    public async Task<RegisterResponse> RegisterAsync(AddingRegister request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var account = await accountRepository.RegisterAccount(request)
            ?? throw new InvalidOperationException("Email already exists or the account could not be created.");

        return mapper.Map<RegisterResponse>(account);
    }

    public Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return accountRepository.LoginAccount(request);
    }

    public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var appUser = await tokenRepository.RetrieveUserByRefreshToken(request.Token)
            ?? throw new ArgumentException("Input is invalid.", nameof(request.Token));

        var tokenInfo = await tokenRepository.IssueAccessToken(appUser);
        var refreshToken = tokenRepository.IssueRefreshToken();

        if (!await tokenRepository.SaveRefreshToken(appUser.UserName ?? appUser.Email ?? appUser.Id, refreshToken))
        {
            throw new InvalidOperationException("Unable to persist the refresh token.");
        }

        return new RefreshTokenResponse
        {
            AccessToken = tokenInfo.AccessToken,
            ExpiresAt = tokenInfo.ExpiresAt,
            RefreshToken = refreshToken
        };
    }

    public Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return accountRepository.ForgotPasswordAsync(request);
    }

    public Task VerifyResetCodeAsync(VerifyCodeRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return accountRepository.VerifyResetCodeAsync(request);
    }

    public Task ResetPasswordAsync(string code, ResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return accountRepository.ResetPasswordAsync(code, request);
    }

    public Task SetPasswordAsync(SetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return accountRepository.SetPasswordAsync(request);
    }

    public Task<AccountDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return accountRepository.GetAccountInformationByIdAsync(id);
    }

    public async Task<IReadOnlyList<AccountDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await accountRepository.GetAllAccountsAsync();
    }

    public Task<AccountDto?> UpdateAsync(string id, UpdatingAccountRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return accountRepository.UpdateAccountAsync(id, request);
    }
}
