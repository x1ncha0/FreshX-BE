using Freshx_API.Interfaces;
using Freshx_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Freshx_API.Services
{
    public class FileService : IFileService
    {
        private readonly FreshxDBContext _context;  // DbContext để tương tác với cơ sở dữ liệu
        private readonly string _devicePath;             // Đường dẫn thư mục lưu tệp

        public FileService( FreshxDBContext context, IConfiguration configuration)
        {
            _context = context;
            _devicePath = configuration["FileSettings:DevicePath"] ?? "C:\\DefaultPath";

        }

        public string UniqueFileName(string originalFileName)
        {
            string extension = Path.GetExtension(originalFileName);  // Lấy phần mở rộng của file
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName); // Lấy tên file không có phần mở rộng

            // Thêm ngày giờ hiện tại vào tên file
            string uniqueFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMdd_HHmmss}{extension}";

            return uniqueFileName;
        }


        //Phương thức lưu tệp và thông tin vào cơ sở dữ liệu
        //userId int thành string
        public async Task<List<Savefile>> SaveFileAsync(string? userId, string? folderName, List<IFormFile> files)
        {
            var result = new List<Savefile>();

            // Nếu userId là null, gán giá trị mặc định là "default"
            var userFolder = userId?.ToString() ?? "default";

            // Nếu folderName là null hoặc rỗng, gán tên thư mục mặc định là "defaultFolder"
            var folder = string.IsNullOrEmpty(folderName) ? "defaultFolder" : folderName;

            // Xây dựng đường dẫn thư mục
            var folderPath = Path.Combine(_devicePath, userFolder, folder);

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            
            // Lưu các tệp tin vào thư mục và lưu thông tin vào cơ sở dữ liệu
            foreach (var file in files)
            {
                var Namefile = UniqueFileName(file.FileName);
                var filePath = Path.Combine(folderPath,Namefile );

                // Lưu tệp vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                
                // Thêm thông tin tệp vào cơ sở dữ liệu
                var saveFile = new Savefile
                {
                    FileName = Namefile,
                    FilePath = filePath,
                    UploadDate = DateTime.Now // Lưu ngày tải lên tệp
                };

                // Thêm bản ghi vào cơ sở dữ liệu
                _context.Savefiles.Add(saveFile);
                await _context.SaveChangesAsync();


                // Thêm kết quả vào danh sách trả về
                result.Add(saveFile);
            }

            return result;
        }

        // Phương thức cập nhật thông tin tệp trong cơ sở dữ liệu
        public async Task<bool> UpdateFileAsync(int? fileId, IFormFile files)
        {
            // Lấy thông tin tệp từ cơ sở dữ liệu
            var file = await _context.Savefiles.FirstOrDefaultAsync(f => f.Id == fileId);
            if (file == null)
            {
                throw new FileNotFoundException("Tệp không tồn tại.");
            }

            // Đường dẫn tệp cũ
            var oldFilePath = file.FilePath;

            // Xóa tệp cũ trên hệ thống
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }

            var Namefile = UniqueFileName(file.FileName);
            // Tạo đường dẫn mới
            var directoryPath = Path.GetDirectoryName(oldFilePath);
            var newFilePath = Path.Combine(directoryPath ?? string.Empty, Namefile);

            // Lưu tệp mới vào hệ thống
            using (var stream = new FileStream(newFilePath, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }

            // Cập nhật thông tin tệp trong cơ sở dữ liệu
            file.FileName = Namefile;
            file.FilePath = newFilePath;
            file.UploadDate = DateTime.Now;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Savefiles.Update(file);
            await _context.SaveChangesAsync();
            return true;
        }

        // Phương thức xóa tệp từ thư mục và cơ sở dữ liệu
        public async Task<bool> DeleteFileAsync(int? fileId)
        {
            var file = await _context.Savefiles.FindAsync(fileId);
            if (file == null)
            {
                return false; // Nếu không tìm thấy tệp
            }

            // Xóa tệp khỏi hệ thống tệp
            if (File.Exists(file.FilePath))
            {
                File.Delete(file.FilePath);
            }

            // Xóa bản ghi tệp khỏi cơ sở dữ liệu
            _context.Savefiles.Remove(file);
            await _context.SaveChangesAsync();

            return true;
        }

        // Phương thức liệt kê tất cả tệp đã lưu trong cơ sở dữ liệu
        public async Task<List<Savefile>> ListFilesAsync()
        {
            return await _context.Savefiles.ToListAsync();
        }
    }

}
