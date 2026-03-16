using AutoMapper;
using Azure.Core;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Doctor;
using Freshx_API.Dtos.Employee;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Freshx_API.Repository;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly FreshxDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeRepository service, ILogger<EmployeeController> logger, IMapper mapper, FreshxDBContext context)
        {
            _employeeRepository = service;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        [HttpPost("Create-Employee")]
        public async Task<ActionResult<ApiResponse<EmployeeDto?>>> CreatEmployee(EmployeeRequest request)
        {
            try
            {

                var isEmailExist = await _context.Employees.FirstOrDefaultAsync(d => d.Email == request.Email);
                if (isEmailExist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email bạn nhập đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var isIdentityCardExist = await _context.Employees.FirstOrDefaultAsync(d => d.IdentityCardNumber == request.IdentityCardNumber);
                if (isIdentityCardExist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "CCCD bạn nhập đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var isPhonenumberIsxist = await _context.Employees.FirstOrDefaultAsync(d => d.PhoneNumber == request.PhoneNumber);
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

                if (!position.Name.StartsWith("Tiếp Nhận")&&!position.Name.StartsWith("Thu Ngân"))
                {
                    return BadRequest(
                ResponseFactory.Error<object>(
                    Request.Path,
                    "Phòng khám hoặc vai trò không hợp lệ",
                    StatusCodes.Status400BadRequest));
                }
               
                // Kiểm tra position và department có phù hợp với nhau không
                if (position.Name == "Tiếp Nhận" && !department.Name.StartsWith("Phòng tiếp nhận"))
                {
                    return BadRequest(
                        ResponseFactory.Error<object>(
                            Request.Path,
                            "Nhân viên tiếp nhận chỉ có thể được phân vào phòng tiếp nhận",
                            StatusCodes.Status400BadRequest));
                }

                if (position.Name == "Thu Ngân" && !department.Name.StartsWith("Phòng kế toán"))
                {
                    return BadRequest(
                        ResponseFactory.Error<object>(
                            Request.Path,
                            "Nhân viên thu ngân chỉ có thể được phân vào phòng kế toán",
                            StatusCodes.Status400BadRequest));
                }
                var employee = await _employeeRepository.CreateEmployeeAsync(request);
                if (employee == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email của bạn đã tồn tại trong hệ thống", StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<EmployeeDto>(employee);

                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occured while creating a new employee");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi tạo một nhân viên mới", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet("Get-AllEmployees")]
        public async Task<ActionResult<ApiResponse<List<DoctorDto?>>>> GetEmployees([FromQuery] Parameters parameters)
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync(parameters);

                var data = _mapper.Map<List<EmployeeDto?>>(employees);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<Object>(Request.Path, data, "Lấy danh sách nhân viên thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while getting doctors: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi lấy danh sách nhân viên"));
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto?>>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound(ResponseFactory.Error<Object>(Request.Path, "Thông tin chi tiết của nhân viên rỗng"));
                }
                var data = _mapper.Map<EmployeeDto>(employee);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data, "Lấy thông tin chi tiết của nhân viên thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while getting employee: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi lấy chi tiết thông tin nhân viên"));
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto?>>> DeleteDoctorById(int id)
        {
            try
            {
                var employee = await _employeeRepository.DeleteEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, "Xóa nhân viên thất bại", StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<EmployeeDto>(employee);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success(Request.Path, data, "Nhân viên đã được xóa thành công", StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while getting employee: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi xóa nhân viên"));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto?>>> UpdateEmployeeById(int id, [FromForm]EmployeeRequest request)
        {
            try
            {
                var employeeById = await _context.Employees.FirstOrDefaultAsync(p => p.EmployeeId == id);
                var employeeByEmail = await _context.Employees.FirstOrDefaultAsync(p => p.Email == request.Email);
                var employeeByIdentityCard = await _context.Employees.FirstOrDefaultAsync(p => p.IdentityCardNumber == request.IdentityCardNumber);
                var employeeByPhoneNumber = await _context.Employees.FirstOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);
                if (employeeByEmail != null)
                {
                    if (!employeeByEmail.Email.ToLower().Equals(employeeById.Email.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email đã thực sự tồn tại trong hệ thống"));
                    }
                }
                if (employeeByIdentityCard != null)
                {
                    if (!employeeByIdentityCard.IdentityCardNumber.ToLower().Equals(employeeById.IdentityCardNumber.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "CCCD đã thực sự tồn tại trong hệ thống"));
                    }
                }
                if (employeeByPhoneNumber != null)
                {
                    if (!employeeByPhoneNumber.PhoneNumber.ToLower().Equals(employeeById.PhoneNumber.ToLower()))
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

                if (!position.Name.StartsWith("Tiếp Nhận") && !position.Name.StartsWith("Thu Ngân"))
                {
                    return BadRequest(
                ResponseFactory.Error<object>(
                    Request.Path,
                    "Phòng khám hoặc vai trò không hợp lệ",
                    StatusCodes.Status400BadRequest));
                }

                // Kiểm tra position và department có phù hợp với nhau không
                if (position.Name == "Tiếp Nhận" && !department.Name.StartsWith("Phòng tiếp nhận"))
                {
                    return BadRequest(
                        ResponseFactory.Error<object>(
                            Request.Path,
                            "Nhân viên tiếp nhận chỉ có thể được phân vào phòng tiếp nhận",
                            StatusCodes.Status400BadRequest));
                }

                if (position.Name == "Thu Ngân" && !department.Name.StartsWith("Phòng kế toán"))
                {
                    return BadRequest(
                        ResponseFactory.Error<object>(
                            Request.Path,
                            "Nhân viên thu ngân chỉ có thể được phân vào phòng kế toán",
                            StatusCodes.Status400BadRequest));
                }
                var employee = await _employeeRepository.UpdateEmployeeByIdAsync(id, request);
                if (employee == null)
                {
                    return BadRequest(ResponseFactory.Error<Object>(Request.Path, "Cập nhật nhân viên thất bại", StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<EmployeeDto>(employee);
                return Ok(ResponseFactory.Success(Request.Path, data, "Nhân viên đã được cập nhật thành công"));
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error occurred while getting employee: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi cập nhật nhân viên"));
            }
        }
    }
}
