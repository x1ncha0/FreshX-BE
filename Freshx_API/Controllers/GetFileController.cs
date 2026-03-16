using Freshx_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class GetFileController : ControllerBase
    {
        private readonly FreshxDBContext _context;
        public GetFileController(FreshxDBContext context)
        {
            _context = context;
        }
        // GET: api/<GetFileController>
        [HttpGet("/image/{id}")]
        public async Task<ActionResult<Savefile>> Getfile(int id)
        {
            var FileSever = await _context.Savefiles.FindAsync(id);
            if (FileSever.FilePath == null)
            { return NotFound("Chưa có file được thêm."); }
            var filePath = FileSever.FilePath;
            if (System.IO.File.Exists(filePath))
            {
                // Kiểm tra xem file có phải là ảnh hay không
                var fileExtension = Path.GetExtension(filePath).ToLower();
                if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".gif")
                {
                    // Trả về file hình ảnh để hiển thị
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "image/jpeg"); // Hoặc loại hình ảnh phù hợp
                }
                else
                {
                    // Trả về file để tải xuống
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/octet-stream", FileSever.FileName);
                }
            }
            else
            {
                return NotFound("File không tồn tại trên hệ thống.");
            }
        }
    }
}
