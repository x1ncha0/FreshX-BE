namespace Freshx_API.Dtos
{
    public class FileDto
    {
        public IFormFile? file { get; set; }
        public string? urlFile { get; set; } 
        public string? fileName { get; set; }
    }
}
