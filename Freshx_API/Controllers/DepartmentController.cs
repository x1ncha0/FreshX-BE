using Azure.Core;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DepartmentDtos;
using Freshx_API.Services.CommonServices;
using Freshx_API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Freshx_API.Dtos.DepartmenTypeDtos;
using Freshx_API.Repository;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IFixDepartmentRepository _fixDepartmentRepository;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;

        public DepartmentController(IFixDepartmentRepository fixDepartmentRepository,ILogger<DepartmentController> logger,IMapper mapper)
        {
            _fixDepartmentRepository = fixDepartmentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("All-Departments")]
        public async Task<ActionResult<ApiResponse<List<Department>>>> GetAllDepartments([FromQuery]Parameters parameters)
        {

            try
            {
                var departments = await _fixDepartmentRepository.GetAllDepartmentsAsync(parameters);
                var data = _mapper.Map<List<DepartmentDto>>(departments);
                return Ok(ResponseFactory.Success(Request.Path, data, "Lấy danh sách phòng ban thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lỗi đã xảy ra khi lấy danh sách phòng ban", StatusCodes.Status500InternalServerError));

            }
        }
        [HttpPost()]
        public async Task<ActionResult<ApiResponse<DepartmentDto?>>> CreateDepartmentAsync([FromBody]DepartmentCreateUpdateDto request)
        {
            try
            {
                var department = await _fixDepartmentRepository.CreateDepartmentAsync(request);
                if (department == null)
                {
                    return BadRequest(ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Tên phòng ban không hợp lệ"));
                }
                var data = _mapper.Map<DepartmentDto>(department);
                return Ok(ResponseFactory.Success(Request.Path, data));
            }
            catch (ArgumentException aex)
            {
                _logger.LogError(aex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, aex.Message, StatusCodes.Status500InternalServerError));
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lỗi đã xảy ra khi tạo mới loại phòng ban" + e.Message, StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet("departmentdetail/{id:int}")]
        public async Task<ActionResult<ApiResponse<DepartmentDto?>>> GetDepartmentById(int id)
        {
            try
            {
                var department= await _fixDepartmentRepository.GetDepartmentByIdAsync(id);
                if (department == null)
                {
                    return BadRequest(ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lấy chi tiết phòng ban thất bại"));
                }
                var data = _mapper.Map<DepartmentDto>(department);
                return Ok(ResponseFactory.Success(Request.Path, data, "Lấy chi tiết phòng ban thành công"));
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentDto>(Request.Path, "Lỗi đã xảy ra khi lấy chi tiết phòng ban", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<DepartmentDto?>>> DeleteDepartmentById(int id)
        {
            try
            {

                var department = await _fixDepartmentRepository.DeleteDepartmentAsync(id);
                if (department == null)
                {
                    return BadRequest(ResponseFactory.Error<DepartmentDto>(Request.Path, "Xóa phòng ban thất bại"));
                }
                var data = _mapper.Map<DepartmentDto>(department);
                return Ok(ResponseFactory.Success(Request.Path, data, "Xóa phòng ban thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lỗi đã xảy ra khi xóa phòng ban", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<Department?>>> UpdateDepartmentById(int id, DepartmentCreateUpdateDto request)
        {
            try
            {
                var department = await _fixDepartmentRepository.UpdateDepartmentAsync(id,request);
                if(department == null)
                {
                    return NotFound(ResponseFactory.Error<DepartmentDto>(Request.Path, "Phòng ban không được tìm thấy"));
                }
                var data = _mapper.Map<DepartmentDto>(department);
                return Ok(ResponseFactory.Success(Request.Path, data, "Cập nhật phòng ban thành công"));
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lỗi đã xảy ra khi cập nhật phòng ban", StatusCodes.Status500InternalServerError));
            }
        }
    }
}
