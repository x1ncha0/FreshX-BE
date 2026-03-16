using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestFileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public TestFileController(IFileService fileService)
        {
            _fileService = fileService;
        }


        // API để lưu tệp
        [HttpPost("upload")] // POST: api/file/upload
        public async Task<IActionResult> UploadFiles(string? userId, string? folderName, [FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("Không có tệp nào được tải lên.");
            }

            try
            {
                var file = await _fileService.SaveFileAsync( userId, folderName, files);
                return Ok(new { message = "Tệp đã được lưu thành công.", file });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lưu tệp.", error = ex.Message });
            }
        }

        // API để cập nhật tệp
        [HttpPut("update/{fileId}")] // PUT: api/file/update/{fileId}
        public async Task<IActionResult> UpdateFile(int fileId, [FromForm] IFormFile newFile)
        {
            if (newFile == null)
            {
                return BadRequest("Không có tệp mới nào được tải lên.");
            }

            try
            {
                var isUpdated = await _fileService.UpdateFileAsync(fileId, newFile);
                if (isUpdated)
                {
                    return Ok(new { message = "Cập nhật tệp thành công." });
                }
                return NotFound(new { message = "Tệp không tồn tại." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật tệp.", error = ex.Message });
            }
        }

        //API để xóa tệp
       //[HttpDelete("delete/{fileId}")] // DELETE: api/file/delete/{fileId}
       // public async Task<IActionResult> DeleteFile(int fileId)
       // {
       //     try
       //     {
       //         var isDeleted = await _fileService.DeleteFileAsync(fileId);
       //         if (isDeleted)
       //         {
       //             return Ok(new { message = "Tệp đã được xóa thành công." });
       //         }
       //         return NotFound(new { message = "Tệp không tồn tại." });
       //     }
       //     catch (Exception ex)
       //     {
       //         return StatusCode(500, new { message = "Lỗi khi xóa tệp.", error = ex.Message });
       //     }
       // }

        //API để liệt kê tất cả các tệp
       [HttpGet("list")] // GET: api/file/list
        public async Task<IActionResult> ListFiles()
        {
            try
            {
                var files = await _fileService.ListFilesAsync();
                return Ok(files);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách tệp.", error = ex.Message });
            }
        }
    }
}