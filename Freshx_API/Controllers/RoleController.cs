using AutoMapper;
using Freshx_API.Dtos.Auth.Role;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Interfaces.Auth;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleController> _logger;
        private readonly IMapper _mapper;
        public RoleController(IRoleRepository roleRepository, ILogger<RoleController> logger,IMapper mapper)
        {
            _roleRepository = roleRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<RoleResponse>>> CreateRole(AddingRole addingRole)
        {
            try
            {
                var role = await _roleRepository.CreateRole(addingRole);
                if (role == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,ResponseFactory.Error<RoleResponse>(Request.Path,"Role already existed",StatusCodes.Status400BadRequest));
                }
                return StatusCode(StatusCodes.Status201Created,ResponseFactory.Success(Request.Path,role,"A new role created successfully",StatusCodes.Status201Created));
            }
            catch (Exception e)
            {
                _logger.LogError(e,"An exception occured while creating new role");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<RoleResponse>(Request.Path, "An exception occured while creating new role", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<RoleResponse>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleRepository.GetAllRoles();
                var data = _mapper.Map<List<RoleResponse>>(roles);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data, "Get roles successfully", StatusCodes.Status200OK));
            }
            catch(Exception e)
            {
                _logger.LogError(e, "An exception occured while getting roles");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<RoleResponse>(Request.Path, "An exception occured while getting roles", StatusCodes.Status500InternalServerError));  
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<RoleResponse>>> GetRoleById(string id)
        {
            try
            {
                var role = await _roleRepository.GetRoleById(id);
                if (role == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<RoleResponse>(Request.Path, $"Role by {id} not existed"));
                }
                var data = _mapper.Map<RoleResponse>(role);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data, $"Get role by id: {id} successfully", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while getting role by id: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<RoleResponse>(Request.Path, $"An exception occured while getting role by {id}", StatusCodes.Status500InternalServerError));
            }
        }
    }
}
