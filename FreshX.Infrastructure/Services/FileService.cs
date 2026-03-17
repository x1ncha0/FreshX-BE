using FreshX.Application.Interfaces;
using FreshX.Domain.Entities;
using FreshX.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FreshX.Infrastructure.Services;

public class FileService(FreshXDbContext context, IConfiguration configuration) : IFileService
{
    private readonly string devicePath = configuration["FileSettings:DevicePath"] ?? "C:\\FreshXFiles";

    public async Task<List<Savefile>> SaveFileAsync(string? userId, string? folderName, List<IFormFile> files)
    {
        var result = new List<Savefile>();
        var userFolder = userId ?? "default";
        var folder = string.IsNullOrWhiteSpace(folderName) ? "defaultFolder" : folderName;
        var folderPath = Path.Combine(devicePath, userFolder, folder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var file in files)
        {
            var uniqueFileName = UniqueFileName(file.FileName);
            var filePath = Path.Combine(folderPath, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var saveFile = new Savefile
            {
                FileName = uniqueFileName,
                FilePath = filePath,
                UploadDate = DateTime.UtcNow
            };

            context.Savefiles.Add(saveFile);
            await context.SaveChangesAsync();
            result.Add(saveFile);
        }

        return result;
    }

    public async Task<bool> UpdateFileAsync(int? fileId, IFormFile files)
    {
        var file = await context.Savefiles.FirstOrDefaultAsync(f => f.Id == fileId)
            ?? throw new FileNotFoundException("File does not exist.");

        if (File.Exists(file.FilePath))
        {
            File.Delete(file.FilePath);
        }

        var uniqueFileName = UniqueFileName(files.FileName);
        var directoryPath = Path.GetDirectoryName(file.FilePath) ?? devicePath;
        var newFilePath = Path.Combine(directoryPath, uniqueFileName);

        await using (var stream = new FileStream(newFilePath, FileMode.Create))
        {
            await files.CopyToAsync(stream);
        }

        file.FileName = uniqueFileName;
        file.FilePath = newFilePath;
        file.UploadDate = DateTime.UtcNow;

        context.Savefiles.Update(file);
        await context.SaveChangesAsync();
        return true;
    }

    public Task<List<Savefile>> ListFilesAsync() => context.Savefiles.AsNoTracking().ToListAsync();

    private static string UniqueFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
        return $"{fileNameWithoutExtension}_{DateTime.UtcNow:yyyyMMdd_HHmmssfff}{extension}";
    }
}
