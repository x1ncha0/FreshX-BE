using AutoMapper;
using Freshx_API.Dtos.InventoryType;
using Freshx_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryTypeController : ControllerBase
    {
        private readonly InventoryTypeService _service;

        public InventoryTypeController(InventoryTypeService service)
        {
            _service = service;
        }

        // API GET: Lấy danh sách loại tồn kho
        [HttpGet]
        public async Task<ActionResult<List<InventoryTypeDto>>> GetAll(string? searchKeyword)
        {
            var result = await _service.GetAllAsync(searchKeyword);

            if (result == null || !result.Any())
            {
                return NotFound("Chưa có dữ liệu nào.");
            }

            return Ok(result);
        }

        // API GET: Lấy thông tin loại tồn kho theo ID
        [HttpGet("id/{id}")]
        public async Task<ActionResult<InventoryTypeDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound("Loại tồn kho không tồn tại.");
            }

            return Ok(result);
        }
        [HttpGet("code/{code}")]
        public async Task<ActionResult<InventoryTypeDto>> GetByCode(string code)
        {
            var result = await _service.GetByCodeAsync(code);

            if (result == null)
            {
                return NotFound("Loại tồn kho không tồn tại.");
            }

            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<InventoryTypeDto>> GetByName(string name)
        {
            var result = await _service.GetNameAsync(name);

            if (result == null)
            {
                return NotFound("Loại tồn kho không tồn tại.");
            }

            return Ok(result);
        }

        // API POST: Tạo mới loại tồn kho
        [HttpPost]
        public async Task<ActionResult<InventoryTypeDto>> Create([FromBody] InventoryTypeCreateUpdateDto dto)
        {
            try
            {
                // Attempt to create the InventoryType using the service method
                var result = await _service.CreateAsync(dto);

                // Return a 201 Created response with the location of the newly created resource
                return CreatedAtAction(nameof(GetById), new { id = result.InventoryTypeId }, result);
            }
            catch (InvalidOperationException ex)
            {
                // If the exception is thrown due to a duplicate name, return a 409 Conflict error
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions and return a 500 Internal Server Error
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }


        // API PUT: Cập nhật thông tin loại tồn kho
        [HttpPut("id/{id}")]
        public async Task<ActionResult> UpdateId(int id, [FromBody] InventoryTypeCreateUpdateDto dto)
        {
            var isUpdated = await _service.UpdateAsync(id, dto);

            if (!isUpdated)
            {
                return NotFound("Loại tồn kho không tồn tại.");
            }

            return NoContent(); // Trả về 204 No Content khi cập nhật thành công
        }
        [HttpPut("code/{code}")]
        public async Task<ActionResult> UpdateCode(string code, [FromBody] InventoryTypeCreateUpdateDto dto)
        {
            var isUpdated = await _service.UpdateAsyncCode(code, dto);

            if (!isUpdated)
            {
                return NotFound("Loại tồn kho không tồn tại.");
            }

            return NoContent(); // Trả về 204 No Content khi cập nhật thành công
        }

        // API DELETE: Xóa mềm loại tồn kho
        [HttpDelete("id/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await _service.DeleteAsyncId(id);

            if (!isDeleted)
            {
                return NotFound("Loại tồn kho không tồn tại.");
            }

            return NoContent(); // Trả về 204 No Content khi xóa thành công
        }
        [HttpDelete("code/{code}")]
        public async Task<ActionResult> Deletecode(string code)
        {
            var isDeleted = await _service.DeleteAsyncCode(code);

            if (!isDeleted)
            {
                return NotFound("Loại tồn kho không tồn tại.");
            }

            return NoContent(); // Trả về 204 No Content khi xóa thành công
        }
    }
}
