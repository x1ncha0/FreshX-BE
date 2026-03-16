using AutoMapper;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.UserAccount;
using Freshx_API.Interfaces.UserAccount;
using Freshx_API.Models;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ILogger<UserAccountController> _logger;
        private readonly IMapper _mapper;
        public UserAccountController(IUserAccountRepository userAccountRepository, IMapper mapper, ILogger<UserAccountController> logger)
        {
            _userAccountRepository = userAccountRepository;
            _mapper = mapper;
            _logger = logger;
        }
        //quản lý thông tin tài khoản người dùng hệ thống
        [HttpPost("Create-NewUser")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> CreateNewUser([FromForm] AddingUserRequest request)
        {
            try
            {
                var newUser = await _userAccountRepository.CreateUserAsync(request);
                if (newUser == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email is existed", StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<AppUser, UserResponse>(newUser);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An inception occured while creating a new User");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "An exception occured while creating a new user", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet("Get-AllUsers")]
        public async Task<ActionResult<ApiResponse<Object>>> GetAllUsers([FromQuery] Parameters parameters)
        {
            try
            {
                var users = await _userAccountRepository.GetUsersAsync(parameters);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, users));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while getting all users");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "An exception occured while getting all users", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _userAccountRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, $"Get information by {id} not found", StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<AppUser, UserResponse>(user);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> DeleteUserByid(string id)
        {
            try
            {
                var user = await _userAccountRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, $"Delete user by {id} fail", StatusCodes.Status404NotFound));
                }
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<Object>(Request.Path, null, "User deleted success", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while deleting user");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "an exception occured while deleting user"));
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> UpdateUserById(string id, UpdatingUserRequest request)
        {
            try
            {
                var user = await _userAccountRepository.UpdateUserByIdAsync(id, request);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<UserResponse>(Request.Path, $"Updating user by {id} not found", StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<AppUser, UserResponse>(user);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while updating user by {id} fail");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, $"An exception occured while updating user by {id}", StatusCodes.Status500InternalServerError));
            }
        }
    }
}