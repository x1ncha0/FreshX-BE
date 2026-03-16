using AutoMapper;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.Patient;
using Freshx_API.Interfaces;
using Freshx_API.Models;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;
        private readonly FreshxDBContext _context;
        public PatientController(IPatientRepository patientRepository, IMapper mapper, ILogger<PatientController> logger, FreshxDBContext context)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }
        [HttpPost("Create-Patient")]
        public async Task<ActionResult<ApiResponse<PatientResponseDto?>>> CreatePatient([FromForm] AddingPatientRequest addingPatientRequest)
        {
            try
            {
                var isEmailExist = await _context.Patients.FirstOrDefaultAsync(p => p.Email == addingPatientRequest.Email);
                if (isEmailExist != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email bạn nhập đã tồn tại trong hệ thống"));
                }
                var isCardNumber = await _context.Patients.FirstOrDefaultAsync(p => p.IdentityCardNumber == addingPatientRequest.IdentityCardNumber);
                if (isCardNumber != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "CCCD bạn đã nhập đã tồn tại trong hệ thống"));
                }
                var isNumberPhone = await _context.Patients.FirstOrDefaultAsync(p => p.PhoneNumber == addingPatientRequest.PhoneNumber);
                if (isNumberPhone != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Số điện thoại bạn đã nhập đã tồn tại trong hệ thống"));
                }
                var patient = await _patientRepository.CreatePatientAsync(addingPatientRequest);
                var data = _mapper.Map<PatientResponseDto>(patient);
                if (patient == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Dữ liệu nhập vào không hợp lệ"));
                }
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<PatientResponseDto>(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exceptional occured while creating a patient");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "An exceptional occured while creating a patient", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet("Get-AllPatients")]
        public async Task<ActionResult<ApiResponse<List<PatientResponseDto?>>>> GetAllPatients([FromQuery] Parameters parameters)
        {
            try
            {
                var patients = await _patientRepository.GetPatientsAsync(parameters);
                var data = _mapper.Map<List<PatientResponseDto?>>(patients);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<Object>(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Một ngoại lệ đã xảy ra khi lấy danh sách bệnh nhân");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Lỗi đã xảy ra khi lấy danh sách bệnh nhân"));
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<PatientResponseDto?>>> UpdatePatientById(int id, UpdatingPatientRequest request)
        {
            try
            {
                var patientById = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
                var patientByEmail = await _context.Patients.FirstOrDefaultAsync(p => p.Email == request.Email);
                var patientByIdentityCard = await _context.Patients.FirstOrDefaultAsync(p => p.IdentityCardNumber == request.IdentityCardNumber);
                var patientByPhoneNumber = await _context.Patients.FirstOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);
                if (patientByEmail != null)
                {
                    if (!patientByEmail.Email.ToLower().Equals(patientById.Email.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Email đã thực sự tồn tại trong hệ thống"));
                    }
                }
                if (patientByIdentityCard != null)
                {
                    if (!patientByIdentityCard.IdentityCardNumber.ToLower().Equals(patientById.IdentityCardNumber.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "CCCD đã thực sự tồn tại trong hệ thống"));
                    }
                }
                if (patientByPhoneNumber != null)
                {
                    if (!patientByPhoneNumber.PhoneNumber.ToLower().Equals(patientById.PhoneNumber.ToLower()))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Số điện thoại đã tồn tại trong hệ thống"));
                    }
                }
                var patient = await _patientRepository.UpdatePatientByIdAsync(id, request);
                if (patient == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.Error<Object>(Request.Path, "Dữ liệu nhập vào không hợp lệ", StatusCodes.Status400BadRequest));
                }
                var data = _mapper.Map<PatientResponseDto>(patient);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<PatientResponseDto>(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while updating patient by {id} failed");
                return StatusCode(StatusCodes.Status500InternalServerError, "Một ngoại lệ đã xảy ra khi cập nhật bênh nhân theo id");
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<PatientResponseDto>>> GetPatatientById(int id)
        {
            try
            {
                var patient = await _patientRepository.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, null, StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<PatientResponseDto>(patient);
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while getting by {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Một ngoại lệ đã xảy ra khi get patient theo id");
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse<PatientResponseDto?>>> DeletePatientById(int id)
        {
            try
            {
                var patient = await _patientRepository.DeletePatientByIdAsync(id);
                if (patient == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseFactory.Error<Object>(Request.Path, "Bệnh nhân không tồn tại", StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<PatientResponseDto>(patient);
                return StatusCode(StatusCodes.Status200OK, ResponseFactory.Success<PatientResponseDto>(Request.Path, data,$"Bệnh nhân {data.Name}  đã được xóa thành công"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An exception occured while deleting a patient by {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<Object>(Request.Path, "Một ngoại lệ đã xảy ra khi hiển thị thông tin chi tiết của bệnh nhân"));
            }
        }
       
       
    }
}
