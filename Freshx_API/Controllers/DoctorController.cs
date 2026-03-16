using Azure.Core;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Doctor;
using Freshx_API.Services.CommonServices;
using Freshx_API.Services;
using Microsoft.AspNetCore.Mvc;
using Freshx_API.Dtos;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IFixDoctorRepository _fixDoctorRepository;
        private readonly FreshxDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorController> _logger;    
        public DoctorController(IFixDoctorRepository service, ILogger<DoctorController> logger,IMapper mapper,FreshxDBContext context)
        {
            _fixDoctorRepository = service;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost("Create-Doctor")]
        public async Task<ActionResult<ApiResponse<DoctorDto?>>> CreateDoctor(DoctorCreateUpdateDto request)
        {
            try
            {
                var isEmailExist = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == request.Email);
                if (isEmailExist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,ResponseFactory.Error<Object>(Request.Path,"Email bạn nhập đã tồn tại trong hệ thống",StatusCodes.Status400BadRequest));
                }
                var isIdentityCardExist = await _context.Doctors.FirstOrDefaultAsync(d => d.IdentityCardNumber == request.IdentityCardNumber);
                if (isIdentityCardExist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "CCCD bạn nhập đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var isPhonenumberIsxist = await _context.Doctors.FirstOrDefaultAsync(d => d.Phone == request.PhoneNumber); 
                if(isPhonenumberIsxist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Số điện thoại bạn nhập đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var position = await _context.Positions.FirstOrDefaultAsync(p => p.Id == request.PositionId);
               if(position == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Vai trò không hợp lệ", StatusCodes.Status400BadRequest));
                }
                var department = await _context.Departments.FirstOrDefaultAsync(p => p.DepartmentId == request.DepartmentId);
                if (department == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,ResponseFactory.Error<Object>(Request.Path,"Phòng ban không hợp lệ",StatusCodes.Status400BadRequest));
                }

                if ( !position.Name.StartsWith("Bác Sĩ"))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Vai trò không đúng hợp lệ", StatusCodes.Status400BadRequest));
                }
                if(!department.Name.StartsWith("Phòng khám")&&!department.Name.StartsWith("Phòng siêu âm"))
                {
                    return BadRequest(
               ResponseFactory.Error<object>(
                   Request.Path,
                   "Phòng khám và vai trò không hợp lệ",
                   StatusCodes.Status400BadRequest));
                }
                // Kiểm tra position và department có phù hợp với nhau không
                if (position.Name == "Bác Sĩ Phòng Khám" && !department.Name.StartsWith("Phòng khám"))
                {
                    return BadRequest(
                        ResponseFactory.Error<object>(
                            Request.Path,
                            "Bác sĩ phòng khám chỉ có thể được phân vào phòng khám",
                            StatusCodes.Status400BadRequest));
                }

                if (position.Name == "Bác Sĩ Siêu Âm" && !department.Name.StartsWith("Phòng siêu âm"))
                {
                    return BadRequest(
                        ResponseFactory.Error<object>(
                            Request.Path,
                            "Bác sĩ siêu âm chỉ có thể được phân vào phòng siêu âm",
                            StatusCodes.Status400BadRequest));
                }
                var doctor = await _fixDoctorRepository.CreateDoctorAsync(request);
                if (doctor == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email của bạn đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<DoctorDto>(doctor);

                return StatusCode(StatusCodes.Status200OK,ResponseFactory.Success(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while creating a new doctor");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi tạo một bác sĩ mới",StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet("Get-AllDoctors")]
        public async Task<ActionResult<ApiResponse<List<DoctorDto?>>>> GetDoctors([FromQuery]Parameters parameters)
        {
            try
            {
                var doctors = await _fixDoctorRepository.GetDoctorsAsync(parameters);
                
                var data = _mapper.Map<List<DoctorDto?>>(doctors);
                return StatusCode(StatusCodes.Status200OK,ResponseFactory.Success<Object>(Request.Path,data,"Lấy danh sách bác sĩ thành công"));
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error occurred while creating doctor: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi lấy danh sách bác sĩ"));
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<DoctorDto?>>> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _fixDoctorRepository.GetDoctorByIdAsycn(id);
                if(doctor == null)
                {
                    return NotFound(ResponseFactory.Error<Object>(Request.Path, "Thông tin chi tiết của bác sĩ rỗng"));
                }
                var data = _mapper.Map<DoctorDto>(doctor);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data, "Lấy thông tin chi tiết của bác sĩ thành công"));
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error occurred while getting doctor: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi lấy chi tiết thông tin bác sĩ"));
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<DoctorDto?>>> DeleteDoctorById(int id)
        {
            try
            {
                var doctor = await _fixDoctorRepository.DeleteDoctorByIdAsync(id);
                if(doctor == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, "Xóa bác sĩ thất bại", StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<DoctorDto>(doctor);
                return StatusCode(StatusCodes.Status200OK,ResponseFactory.Success(Request.Path, data,"Bác sĩ đã được xóa thành công",StatusCodes.Status200OK));
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error occurred while getting doctor: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi xóa bác sĩ"));
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<DoctorDto?>>> UpdateDoctorById(int id, [FromForm]DoctorCreateUpdateDto request)
        {
            try
            {
                var doctorById = await _context.Doctors.FirstOrDefaultAsync(p => p.DoctorId == id);
                var doctorByEmail = await _context.Doctors.FirstOrDefaultAsync(p => p.Email == request.Email);
                var doctorByIdentityCard = await _context.Doctors.FirstOrDefaultAsync(p => p.IdentityCardNumber == request.IdentityCardNumber);
                var doctorByPhoneNumber = await _context.Doctors.FirstOrDefaultAsync(p => p.Phone == request.PhoneNumber);
                if (doctorByEmail != null)
                {
                    if (!doctorByEmail.Email.ToLower().Equals(doctorById.Email.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email đã thực sự tồn tại trong hệ thống"));
                    }
                }
                if (doctorByIdentityCard != null)
                {
                    if (!doctorByIdentityCard.IdentityCardNumber.ToLower().Equals(doctorById.IdentityCardNumber.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "CCCD đã thực sự tồn tại trong hệ thống"));
                    }
                }
                if (doctorByPhoneNumber != null)
                {
                    if (!doctorByPhoneNumber.Phone.ToLower().Equals(doctorById.Phone.ToLower()))
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

                if (!position.Name.StartsWith("Bác Sĩ"))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Vai trò không đúng hợp lệ", StatusCodes.Status400BadRequest));
                }
                if (!department.Name.StartsWith("Phòng khám") && !department.Name.StartsWith("Phòng siêu âm"))
                {
                    return BadRequest(
               ResponseFactory.Error<object>(
                   Request.Path,
                   "Phòng khám và vai trò không hợp lệ",
                   StatusCodes.Status400BadRequest));
                }
                // Kiểm tra position và department có phù hợp với nhau không
                if (position.Name == "Bác Sĩ Phòng Khám" && !department.Name.StartsWith("Phòng khám"))
                {
                    return BadRequest(
                        ResponseFactory.Error<object>(
                            Request.Path,
                            "Bác sĩ phòng khám chỉ có thể được phân vào phòng khám",
                            StatusCodes.Status400BadRequest));
                }

                if (position.Name == "Bác Sĩ Siêu Âm" && !department.Name.StartsWith("Phòng siêu âm"))
                {
                    return BadRequest(
                        ResponseFactory.Error<object>(
                            Request.Path,
                            "Bác sĩ siêu âm chỉ có thể được phân vào phòng siêu âm",
                            StatusCodes.Status400BadRequest));
                }
                var doctor = await _fixDoctorRepository.UpdateDoctorByIdAsync(id, request);
                if (doctor == null) 
                {
                    return BadRequest(ResponseFactory.Error<Object>(Request.Path, "Cập nhật bác sĩ thất bại",StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<DoctorDto>(doctor);
                return Ok(ResponseFactory.Success(Request.Path, data, "Bác sĩ đã được cập nhật thành công"));
            }
            catch(Exception e)
            {

                _logger.LogError(e, $"Error occurred while getting doctor: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi cập nhật bác sĩ"));
            }
        }

    }
}
