using AutoMapper;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Freshx_API.Dtos.Technician;
using Freshx_API.Dtos.Doctor;
using Freshx_API.Repository;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly ITechnicianRepository _technicianRepository;
        private readonly FreshxDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TechnicianController> _logger;
        public TechnicianController(ITechnicianRepository service, ILogger<TechnicianController> logger, IMapper mapper, FreshxDBContext context)
        {
            _technicianRepository = service;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost("Create-Technician")]
        public async Task<ActionResult<ApiResponse<TechnicianDto?>>> CreateDoctor(TechnicianRequest request)
        {
            try
            {
                var isEmailExist = await _context.Technicians.FirstOrDefaultAsync(d => d.Email == request.Email);
                if (isEmailExist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email bạn nhập đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var isIdentityCardExist = await _context.Technicians.FirstOrDefaultAsync(d => d.IdentityCardNumber == request.IdentityCardNumber);
                if (isIdentityCardExist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "CCCD bạn nhập đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var isPhonenumberIsxist = await _context.Technicians.FirstOrDefaultAsync(d => d.PhoneNumber == request.PhoneNumber);
                if (isPhonenumberIsxist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Số điện thoại bạn nhập đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var position = await _context.Positions.FirstOrDefaultAsync(p => p.Id == request.PositionId);
                if (position == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Vai trò không hợp lệ", StatusCodes.Status400BadRequest));
                }
                var department = await _context.Departments.FirstOrDefaultAsync(p => p.DepartmentId == request.DepartmentId);
                if (department == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Phòng ban không hợp lệ", StatusCodes.Status400BadRequest));
                }

                if (!position.Name.StartsWith("Kỹ Thuật"))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Vai trò không đúng hợp lệ", StatusCodes.Status400BadRequest));
                }
                if (!department.Name.StartsWith("Phòng xét nghiệm"))
                {
                    return BadRequest(
               ResponseFactory.Error<object>(
                   Request.Path,
                   "Phòng khám và vai trò không hợp lệ",
                   StatusCodes.Status400BadRequest));
                }
              
                var technician = await _technicianRepository.CreateTechnicianAsync(request);
                if (technician == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email của bạn đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<TechnicianDto>(technician);

                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while creating a new doctor");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi tạo một nhân viên kỹ thuật mới", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet("Get-AllTechnicians")]
        public async Task<ActionResult<ApiResponse<List<TechnicianDto?>>>> GetTechnicians([FromQuery] Parameters parameters)
        {
            try
            {
                var technicians = await _technicianRepository.GetAllTechnicianAsync(parameters);

                var data = _mapper.Map<List<TechnicianDto?>>(technicians);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<Object>(Request.Path, data, "Lấy danh sách nhân viên kỹ thuật xét nghiệm thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while creating doctor: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi lấy danh sách nhân viên kỹ thuật xét nghiệm"));
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<TechnicianDto?>>> GetTechnicianById(int id)
        {
            try
            {
                var technician = await _technicianRepository.GetTechnicianByIdAsyn(id);
                if (technician == null)
                {
                    return NotFound(ResponseFactory.Error<Object>(Request.Path, "Thông tin chi tiết của nhân viên rỗng"));
                }
                var data = _mapper.Map<TechnicianDto>(technician);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data, "Lấy thông tin chi tiết của nhân viên thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while getting :technician {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi lấy chi tiết thông tin nhân viên"));
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<TechnicianDto?>>> DeleteTechnicianById(int id)
        {
            try
            {
                var technician = await _technicianRepository.DeleteTechnicianByIdAsyn(id);
                if (technician == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, "Xóa nhân viên thất bại", StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<TechnicianDto>(technician);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data, "Nhân viên đã được xóa thành công", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while deleting technician: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi xóa nhân viên"));
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<TechnicianDto?>>> UpdateTechnicianById(int id,[FromForm]TechnicianRequest request)
        {
            try
            {
                var technicianById = await _context.Technicians.FirstOrDefaultAsync(p => p.TechnicianId == id);
                var technicianByEmail = await _context.Technicians.FirstOrDefaultAsync(p => p.Email == request.Email);
                var technicianByIdentityCard = await _context.Technicians.FirstOrDefaultAsync(p => p.IdentityCardNumber == request.IdentityCardNumber);
                var technicianByPhoneNumber = await _context.Technicians.FirstOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);
                if (technicianByEmail != null)
                {
                    if (!technicianByEmail.Email.ToLower().Equals(technicianById.Email.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email đã thực sự tồn tại trong hệ thống"));
                    }
                }
                if (technicianByIdentityCard != null)
                {
                    if (!technicianByIdentityCard.IdentityCardNumber.ToLower().Equals(technicianById.IdentityCardNumber.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "CCCD đã thực sự tồn tại trong hệ thống"));
                    }
                }
                if (technicianByPhoneNumber != null)
                {
                    if (!technicianByPhoneNumber.PhoneNumber.ToLower().Equals(technicianById.PhoneNumber.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Số điện thoại đã tồn tại trong hệ thống"));
                    }
                }
                var position = await _context.Positions.FirstOrDefaultAsync(p => p.Id == request.PositionId);
                if (position == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Vai trò không hợp lệ", StatusCodes.Status400BadRequest));
                }
                var department = await _context.Departments.FirstOrDefaultAsync(p => p.DepartmentId == request.DepartmentId);
                if (department == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Phòng ban không hợp lệ", StatusCodes.Status400BadRequest));
                }

                if (!position.Name.StartsWith("Kỹ Thuật"))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Vai trò không đúng hợp lệ", StatusCodes.Status400BadRequest));
                }
                if (!department.Name.StartsWith("Phòng xét nghiệm"))
                {
                    return BadRequest(
               ResponseFactory.Error<object>(
                   Request.Path,
                   "Phòng khám và vai trò không hợp lệ",
                   StatusCodes.Status400BadRequest));
                }

                var technician = await _technicianRepository.UpdateTechnicianByIdAsyn(id, request);
                if (technician == null)
                {
                    return BadRequest(ResponseFactory.Error<Object>(Request.Path, "Cập nhật nhân viên thất bại", StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<TechnicianDto>(technician);
                return Ok(ResponseFactory.Success(Request.Path, data, "Nhân viên đã được cập nhật thành công"));
            }
            catch (Exception e)
            {

                _logger.LogError(e, $"Error occurred while updating technician: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi cập nhật nhân viên"));
            }
        }
    }
}
