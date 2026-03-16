using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DepartmenTypeDtos;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;
using Sprache;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ApiResponse<List<object>>> GetStatuses()
        {
            try
            {
                // Danh sách trạng thái
                var statuses = new List<object>
                {
                    new { valueId = 0, name = "Hoạt động" },
                    new { valueId = 1, name = "Tạm ngưng" }
                };

                // Trả về phản hồi thành công
                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, statuses));
            }
            catch (Exception e)
            {
                // Xử lý lỗi và trả về phản hồi lỗi
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<List<object>>(Request.Path, e.Message, StatusCodes.Status500InternalServerError));
            }
        }
    }
}
