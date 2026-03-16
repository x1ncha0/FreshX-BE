using AutoMapper;
using Freshx_API.Dtos;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.UserAccountManagement;
using Freshx_API.Interfaces;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Models;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountManagementController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FreshxDBContext _context;
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<UserAccountManagementController> _logger;
        private readonly IUserAccountManagementRepository _userAccountManagementRepository;
        public UserAccountManagementController(IMapper mapper,FreshxDBContext context,ITokenRepository tokenRepository,ILogger<UserAccountManagementController> logger,IUserAccountManagementRepository userAccountManagementRepository)
        {
            _mapper = mapper;
            _context = context;
            _tokenRepository = tokenRepository;
            _logger = logger;
            _userAccountManagementRepository = userAccountManagementRepository;
        }
        [HttpGet("Infomation-Account")]
        public async Task<ActionResult<ApiResponse<UserAccountResponse?>>> GetInformationAccount()
        {
            try
            {
                string? id = _tokenRepository.GetUserIdFromToken();
                if(id == null)
                {
                    return BadRequest(ResponseFactory.Error<Object>(Request.Path, "Lỗi đã xảy ra khi lấy thông tin chi tiết người dùng"));
                }
                var account = await _userAccountManagementRepository.GetInformationAccoutUserById(id);
                var data = _mapper.Map<UserAccountResponse>(account);
                return Ok(ResponseFactory.Success<UserAccountResponse>(Request.Path,data,"Lấy thông tin chi tiết tài khoản thành công"));
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,ResponseFactory.Error<Object>(Request.Path,"Một ngoại lệ đã xảy ra khi lấy thông tin chi tiết của người dùng",StatusCodes.Status500InternalServerError));
            }
        }
        [HttpPut("Update-Account")]
        public async Task<ActionResult<ApiResponse<UserAccountResponse?>>> UpdateInformationAccount(UserAccountRequest request)
        {
            try
            {
                string? id = _tokenRepository.GetUserIdFromToken();
                if(id == null)
                {
                    return BadRequest(ResponseFactory.Error<Object>(Request.Path, "Lỗi đã xảy ra khi cập nhật thông tin chi tiết người dùng"));
                }
                var accountById = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                var accountByIdentityCard = await _context.Users.FirstOrDefaultAsync(u => u.IdentityCardNumber == request.IdentityCardNumber);
                var accountByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                var accountByPhoneNumber = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
                if(accountByIdentityCard != null && accountById?.IdentityCardNumber != request.IdentityCardNumber)
                {
                    return BadRequest(ResponseFactory.Error<Object>(Request.Path, "CCCD bạn nhập không hợp lệ"));
                }
                if (accountByPhoneNumber != null && accountById?.PhoneNumber != request.PhoneNumber)
                {
                    return BadRequest(ResponseFactory.Error<Object>(Request.Path, "Số điện thoại bạn nhập không hợp lệ"));
                }
                if (accountByEmail != null && accountById?.Email != request.Email)
                {
                    return BadRequest(ResponseFactory.Error<Object>(Request.Path, "Email bạn nhập không hợp lệ"));
                }
                var account = await _userAccountManagementRepository.UpdateInformationAccountUserById(id,request);
                if (account == null) { return BadRequest(ResponseFactory.Error<Object>(Request.Path, "Cập nhật thông tin người dùng không thành công")); }
                var data = _mapper.Map<UserAccountResponse>(account);
                return Ok(ResponseFactory.Success(Request.Path, data, "Cập nhật thông tin người dùng thành công"));
                
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi cập nhật thông tin chi tiết của người dùng", StatusCodes.Status500InternalServerError));
            }
        }
    }
}
