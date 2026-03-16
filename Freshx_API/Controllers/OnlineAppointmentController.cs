using AutoMapper;
using Freshx_API.Dtos;
using Freshx_API.Dtos.CommonDtos;
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
    public class OnlineAppointmentController : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<OnlineAppointmentController> _logger;
        private readonly IMapper _mapper;
        private readonly IOnlineAppointmentRepository _onlineAppointmentRepository;
        private readonly FreshxDBContext _context;
        public OnlineAppointmentController(ITokenRepository tokenRepository, ILogger<OnlineAppointmentController> logger, IMapper mapper, IOnlineAppointmentRepository onlineAppointmentRepository,FreshxDBContext context)
        {
            _tokenRepository = tokenRepository;
            _logger = logger;
            _mapper = mapper;
            _onlineAppointmentRepository = onlineAppointmentRepository;
            _context = context;
        }
        [HttpPost("Create-OnlineAppointment")]
        public async Task<ActionResult<ApiResponse<OnlineAppointmentDto?>>> CreateOnlineAppointment([FromForm]CreateUpdateOnlineAppointment request)
        {
            try
            {
                string accountId = _tokenRepository.GetUserIdFromToken();
                if(accountId == null)
                {
                    return BadRequest(ResponseFactory.Error<OnlineAppointmentDto>(Request.Path, "Vui lòng đăng nhập vào hệ thống để tiến hành đặt lịch",StatusCodes.Status400BadRequest));
                }
                if(request.Date < DateTime.Today) { return BadRequest(ResponseFactory.Error<OnlineAppointment>(Request.Path, "Ngày khám phải lớn hơn hoặc bằng ngày hiện tại",StatusCodes.Status400BadRequest)); }
                // Kiểm tra lịch hẹn hiện có
                var existingAppointment = await _context.OnlineAppointments
                    .FirstOrDefaultAsync(o => o.AccountId == accountId
                        && o.Date >= DateTime.Today && o.IsDeleted == false);
                if (existingAppointment != null)
                {
                    return BadRequest(ResponseFactory.Error<OnlineAppointment>(Request.Path, "Lịch hẹn của bạn đã thực sự tồn tại",StatusCodes.Status400BadRequest));
                }
                var onlineAppointment = await _onlineAppointmentRepository.CreateOnlineAppointment(request, accountId);
                if(onlineAppointment == null)
                {
                    return BadRequest(ResponseFactory.Error<OnlineAppointmentDto>(Request.Path, "Lịch hẹn của bạn đã có người, vui lòng chọn lịch khác",StatusCodes.Status400BadRequest));
                }
                
                var data = _mapper.Map<OnlineAppointmentDto>(onlineAppointment);
                return Ok(ResponseFactory.Success<OnlineAppointmentDto>(Request.Path, data, "Lịch hẹn đã được đặt thành công",StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<OnlineAppointmentDto>(Request.Path,"Một ngoại lệ đã xảy ra khi tạo lịch hẹn",StatusCodes.Status500InternalServerError));
            }
        }
        [HttpGet("Get-OnlineAppointmentDetail")]
        public async Task<ActionResult<ApiResponse<OnlineAppointmentDto?>>> GetOnlineAppointmentByAccountId()
        {

            try
            {
                string accountId = _tokenRepository.GetUserIdFromToken();
                if (string.IsNullOrEmpty(accountId))
                {
                    return BadRequest(ResponseFactory.Error<OnlineAppointmentDto>(
                        Request.Path,
                        "Vui lòng đăng nhập vào hệ thống để xem đặt lịch hẹn chi tiết"
                    ));
                }

                var onlineAppointment = await _onlineAppointmentRepository.GetOnlineAppointmentById(accountId);
                if (onlineAppointment == null)
                {
                    return NotFound(ResponseFactory.Error<OnlineAppointmentDto>(
                        Request.Path,
                        "Không tìm thấy lịch hẹn"
                    ));
                }

                var data = _mapper.Map<OnlineAppointmentDto>(onlineAppointment);
                return Ok(ResponseFactory.Success<OnlineAppointmentDto>(Request.Path, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<OnlineAppointmentDto>(
                        Request.Path,
                        "Một ngoại lệ đã xảy ra khi hiện thông tin lịch hẹn chi tiết",
                        StatusCodes.Status500InternalServerError
                    )
                );
            }
        }
        [HttpPut("Update-OnlineAppointmentDetail")]
        public async Task<ActionResult<ApiResponse<OnlineAppointmentDto?>>> UpdateOnlineAppointmentById(int id,[FromForm]CreateUpdateOnlineAppointment request)
        {
            try
            {
                string? accountId = _tokenRepository.GetUserIdFromToken();
                if (accountId == null) { return BadRequest(ResponseFactory.Error<OnlineAppointmentDto>(Request.Path, "Vui lòng đăng nhập tài khoản để có thể thực hiện chức năng này", StatusCodes.Status400BadRequest)); };
                if (request.Date < DateTime.Today) { return BadRequest(ResponseFactory.Error<OnlineAppointment>(Request.Path, "Ngày khám phải lớn hơn hoặc bằng ngày hiện tại", StatusCodes.Status400BadRequest)); }             
                var isTimeSlotTaken = await _context.OnlineAppointments
                .AnyAsync(o => o.TimeSlotId == request.TimeSlotId
                    && o.DoctorId == request.DoctorId
                    && o.Date == request.Date
                    &&o.IsDeleted == false && o.OnlineAppointmentId != id);
                if (isTimeSlotTaken)
                {
                    return BadRequest(ResponseFactory.Error<OnlineAppointmentDto>(Request.Path, "Không thể đặt lịch hẹn vì đã có người người đặt", StatusCodes.Status400BadRequest));
                }
                var onlineAppointment = await _onlineAppointmentRepository.UpdateOnlineOppointmentById(id,request);
                if (onlineAppointment == null)
                {
                    return NotFound(ResponseFactory.Error<OnlineAppointment>(Request.Path,"Không thể tìm thấy lịch hẹn của bạn",StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<OnlineAppointmentDto>(onlineAppointment);
                return Ok(ResponseFactory.Success<OnlineAppointmentDto>(Request.Path,data,"Lịch hẹn đã được cập nhật thành công",StatusCodes.Status200OK));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<OnlineAppointmentDto>(Request.Path, "Một ngoại lệ đã xảy ra khi cập nhật lịch hẹn", StatusCodes.Status500InternalServerError));
            }
        }
        [HttpDelete("Delete-OnlineAppointment")]
        public async Task<ActionResult<ApiResponse<OnlineAppointmentDto?>>> DeleteOnlineAppointmentById(int id)
        {
            try
            {
                string? accountId = _tokenRepository.GetUserIdFromToken();
                if(accountId == null)
                {
                    return BadRequest(ResponseFactory.Error<OnlineAppointmentDto>(Request.Path, "Vui lòng đăng nhập vào hệ thống", StatusCodes.Status400BadRequest));
                }
                var onlineAppointment = await _onlineAppointmentRepository.DeleteOnlineOppointmentById(id);
                if(onlineAppointment == null)
                {
                    return NotFound(ResponseFactory.Error<OnlineAppointment>(Request.Path, "Không thể tìm thấy lịch hẹn", StatusCodes.Status404NotFound));
                }
                var data = _mapper.Map<OnlineAppointmentDto>(onlineAppointment);
                return Ok(ResponseFactory.Success(Request.Path,data ,"Xóa lịch hẹn thành công", StatusCodes.Status200OK));

            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.Error<OnlineAppointmentDto>(Request.Path, "Một ngoại lệ đã xảy ra khi xóa lịch hẹn", StatusCodes.Status500InternalServerError));
            }
        }

    }
}
