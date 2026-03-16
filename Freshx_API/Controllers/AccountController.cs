using AutoMapper;
using Azure;
using Freshx_API.Dtos.Auth.Account;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;
using Freshx_API.Services.SignalR;
using Freshx_API.Services.CommonServices;
using Freshx_API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;
        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger, IMapper mapper, ITokenRepository token, UserManager<AppUser> userManager, IEmailService emailService, IHubContext<NotificationHub> hubContext)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            _mapper = mapper;
            _tokenRepository = token;
            _userManager = userManager;
            _emailService = emailService;
            _hubContext = hubContext;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ApiResponse<RegisterResponse>>> CreateAccount(AddingRegister addingRegister)
        {
            try
            {
                var account = await _accountRepository.RegisterAccount(addingRegister);
                if (account == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<RegisterResponse>(Request.Path, "Email in existed", StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<RegisterResponse>(account);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data, "A new account created successfully", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while creating a new account");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<RegisterResponse>(Request.Path, "An exception occured while creating a new account", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> LoginAccount(LoginRequest loginRequest)
        {
            try
            {
                var user = await _accountRepository.LoginAccount(loginRequest);
                if (user.Succeeded)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"có một đăng nhập mới vào tài khoản ");
                    return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, user, "Login successfully", StatusCodes.Status200OK));
                }
                if (user.IsLockedOut)
                {
                    return StatusCode(StatusCodes.Status423Locked, ResponseFactory.Error<LoginResponse>(Request.Path, user.Message, StatusCodes.Status423Locked));
                }
                return StatusCode(StatusCodes.Status403Forbidden, ResponseFactory.Error<LoginResponse>(Request.Path, user.Message, StatusCodes.Status403Forbidden));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while logining account");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<LoginResponse>(Request.Path, "An exception occured while logining account", StatusCodes.Status500InternalServerError));
            }
        }
        // co che gui phat token lien tuc theo thoi gian nhat dinh
        // refreshtoken se duoc luu trong db va khi cap phat lai token can gui len refreshtoken
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<object>> RefreshToken([FromBody] RefreshTokenRequest token)
        {
            try
            {
                var appUser = await _tokenRepository.RetrieveUserByRefreshToken(token.Token);
                if (appUser == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<object>(Request.Path, "Input is invalid", StatusCodes.Status400BadRequest));
                }
                var tokenInfo = await _tokenRepository.IssueAccessToken(appUser);
                string refreshToken = _tokenRepository.IssueRefreshToken();
                await _tokenRepository.SaveRefreshToken(appUser.UserName, refreshToken);
                var data = new
                {
                    AccessToken = tokenInfo.AccessToken,
                    ExpiredAt = tokenInfo.ExpiresAt,
                    RefreshToken = refreshToken
                };
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<object>(Request.Path, data));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost("Forgot-Password")]
        public async Task<ActionResult<ApiResponse<Object>>> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordRequest.Email);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Your email is not exist"));
                }
                var code = VerificationCodeGenerator.GenerateCode();
                user.RefreshToken = code;
                user.ExpiredTime = DateTime.UtcNow.AddMinutes(2);
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "An error occurred while processing your request.", StatusCodes.Status500InternalServerError));
                }
                await _emailService.SendEmailAsync(user.Email, "Password Reset Verification Code",
               $"Your verification code is: {code}. This code will expire in 2 minute.");
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, new ForgotPasswordResponse { Code = code }));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost("verify-reset-otp")]
        public async Task<ActionResult<ApiResponse<Object>>> VerifyCode(VerifyCodeRequest verifyCodeRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(verifyCodeRequest.Email);
                if (user == null || user.RefreshToken != verifyCodeRequest.Code || user.ExpiredTime < DateTime.UtcNow)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email or code invalid"));
                }
                var responseVerifyCode = new
                {
                    Email = verifyCodeRequest.Email,
                    Code = verifyCodeRequest.Code
                };
                return StatusCode(StatusCodes.Status200OK, responseVerifyCode);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult<ApiResponse<Object>>> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest, [FromQuery] string code)
        {
            try
            {
                var user = await _tokenRepository.RetrieveUserByRefreshToken(code);
                if (user != null)
                {
                    var tokenResetPassword = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, tokenResetPassword, resetPasswordRequest.Password);

                    if (result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<Object>(Request.Path, user));
                    }
                }
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "An exception occured while processing request"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<AccountDto?>>> GetAccountInfoById(string id)
        {
            try
            {
                var data = await _accountRepository.GetAccountInformationByIdAsync(id);
                if (data == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, $"Information by {id} not found", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<AccountDto>(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while getting accountInfo by {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, $"An exception occured while getting accountInfo by {id}", StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("Get-InformatonAccounts")]
        public async Task<ActionResult<ApiResponse<Object>>> GetAllAccountInformations()
        {
            try
            {
                var data = await _accountRepository.GetAllAccountsAsync();
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred while getting all account informations");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "An exception occurred while getting all account informations", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<AccountDto?>> UpdateAccountInfoById(string id,UpdatingAccountRequest request)
        {
            try
            {
                var data = await _accountRepository.UpdateAccountAsync(id, request);
                if(data == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, $"updating account by {id} fail"));
                }
                return StatusCode(StatusCodes.Status200OK,ResponseFactory.Success(Request.Path, data));
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"An exception occured while updating account by {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An exception occured while updating account by {id}");
            }
        }

    }
}
