using FreshX.Domain.Entities;

namespace FreshX.Application.Interfaces
{
    public interface IFileService
    {
        Task<List<Savefile>> SaveFileAsync(string? userId, string? folderName, List<IFormFile> files);
        Task<bool> UpdateFileAsync(int? fileId, IFormFile files);
        Task<List<Savefile>> ListFilesAsync();
    }
}

