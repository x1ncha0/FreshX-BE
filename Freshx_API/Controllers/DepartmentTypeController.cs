using AutoMapper;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DepartmenTypeDtos;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Freshx_API.Services;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentTypeController : ControllerBase
    {
          private readonly IFixDepartmentTypeRepository _fixDepartmentTypeRepository;
          private readonly ILogger<DepartmentTypeController> _logger;
          private readonly IMapper _mapper;
  
          public DepartmentTypeController(IFixDepartmentTypeRepository fixDepartmentTypeRepository, ILogger<DepartmentTypeController> logger,IMapper mapper)
        {
            _fixDepartmentTypeRepository = fixDepartmentTypeRepository;
            _logger = logger;
            _mapper = mapper;
         
        }
        [HttpPost("Create-DepartType")]
        public async Task<ActionResult<ApiResponse<DepartmentTypeDto?>>> CreateNewDepartmentType(DepartmentTypeCreateUpdateDto request)
        {
            try
            {
                var departmentType = await _fixDepartmentTypeRepository.CreateDepartmentTypeAsync(request);
                if(departmentType == null)
                {
                    return BadRequest(ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Tên loại phòng ban đã thực sự tồn tại"));
                }
                var data = _mapper.Map<DepartmentTypeDto>(departmentType);
                return Ok(ResponseFactory.Success(Request.Path, data));
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,ResponseFactory.Error<DepartmentTypeDto>(Request.Path,"Lỗi đã xảy ra khi tạo mới loại phòng ban",StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet("Get-DepartmentTypes")]
        public async Task<ActionResult<ApiResponse<List<DepartmentTypeDto>>>> GetAllDepartmentType([FromQuery]Parameters parameters)
        {
            try
            {
                var departmentTypes = await _fixDepartmentTypeRepository.GetAllDepartmentTypeAsync(parameters);
                var data = _mapper.Map<List<DepartmentTypeDto>>(departmentTypes);
                return Ok(ResponseFactory.Success(Request.Path, data, "Lấy danh sách loại phòng ban thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lỗi đã xảy ra khi lấy danh sách loại phòng ban", StatusCodes.Status500InternalServerError));

            }
        }
        [HttpGet("Get-DepartmentDetail/{id:int}")]
        public async Task<ActionResult<ApiResponse<DepartmentTypeDto?>>> GetDepartmentTypeById(int id)
        {
            try
            {
                var departmentType = await _fixDepartmentTypeRepository.GetDepartmentTypeByIdAsync(id);
                if(departmentType == null)
                {
                    return BadRequest(ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lấy chi tiết loại phòng ban thất bại"));
                }
                var data = _mapper.Map<DepartmentTypeDto>(departmentType);
                return Ok(ResponseFactory.Success(Request.Path,data ,"Lấy chi tiết loại phòng ban thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lỗi đã xảy ra khi lấy chi tiết loại phòng ban", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<DepartmentTypeDto?>>> DeleteDepartmentTypeById(int id)
        {
            try
            {
                var departmentType = await _fixDepartmentTypeRepository.DeleteDepartmentTypeByIdAsync(id);
                if(departmentType == null)
                {
                    return BadRequest(ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Xóa phòng ban thất bại"));
                }
                var data = _mapper.Map<DepartmentTypeDto>(departmentType) ;
                return Ok(ResponseFactory.Success(Request.Path, data, "Xóa loại phòng ban thành công"));
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lỗi đã xảy ra khi xóa loại phòng ban", StatusCodes.Status500InternalServerError));

            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<DepartmentTypeDto?>>> UpdateDepartmentTypeById(int id, DepartmentTypeCreateUpdateDto request)
        {
            try
            {
                var departmentType = await _fixDepartmentTypeRepository.UpdateDepartmentTypeByIdAsync(id, request);
                if(departmentType == null)
                {
                    return BadRequest(ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Tên loại phòng ban đã thực sự tồn tại"));
                }
                var data = _mapper.Map<DepartmentTypeDto>(departmentType);
                return Ok(ResponseFactory.Success<DepartmentTypeDto>(Request.Path, data, "Cập nhật loại phòng ban thành công"));
            }
            catch(Exception e )
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<DepartmentTypeDto>(Request.Path, "Lỗi đã xảy ra khi cập nhật loại phòng ban", StatusCodes.Status500InternalServerError));

            }
        }


    }
}
