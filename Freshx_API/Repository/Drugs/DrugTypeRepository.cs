using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Freshx_API.Models;

public class DrugTypeRepository : IDrugTypeRepository
{
    private readonly FreshxDBContext _context;

    public DrugTypeRepository(FreshxDBContext context)
    {
        _context = context;
    }

    public async Task<List<DrugType?>> GetDrugTypeAsync(
        string? searchKeyword,
        DateTime? createdDate,
        DateTime? updatedDate,
        int? status)
    {
        // Khởi tạo truy vấn lấy danh sách loại thuốc
        var query = _context.DrugTypes.AsQueryable();

        // Lọc theo từ khóa tìm kiếm (nếu có)
        if (!string.IsNullOrWhiteSpace(searchKeyword))
        {
            query = query.Where(d => d.Name.Contains(searchKeyword) || d.Code.Contains(searchKeyword));
        }

        // Lọc theo ngày tạo (nếu có)
        if (createdDate.HasValue)
        {
            query = query.Where(d => d.CreatedDate >= createdDate.Value);
        }

        // Lọc theo ngày cập nhật (nếu có)
        if (updatedDate.HasValue)
        {
            query = query.Where(d => d.UpdatedDate <= updatedDate.Value);
        }

        // Lọc theo trạng thái (nếu có)
        if (status.HasValue)
        {
            // Giả định: `status` 0 - Hoạt động, 1 - Tạm ngưng, 2 - Đã xóa
            switch (status.Value)
            {
                case 0: // Hoạt động
                    query = query.Where(d => d.IsSuspended == 0 && (d.IsDeleted == 0 || d.IsDeleted == null));
                    break;
                case 1: // Tạm ngưng
                    query = query.Where(d => d.IsSuspended == 1);
                    break;
                case 2: // Đã xóa
                    query = query.Where(d => d.IsDeleted == 1);
                    break;
            }
        }

        // Thực hiện truy vấn và trả về danh sách
        return await query.ToListAsync();
    }

    public async Task<DrugType?> GetDrugTypeByIdAsync(int id)
    {
        return await _context.DrugTypes.FirstOrDefaultAsync(d => d.DrugTypeId == id);
    }

    public async Task<DrugType> CreateDrugTypeAsync(DrugType drugType)
    {
        _context.DrugTypes.Add(drugType);
        await _context.SaveChangesAsync();
        return drugType;
    }

    public async Task<DrugType?> UpdateDrugTypeAsync(DrugType drugType)
    {
        _context.DrugTypes.Update(drugType);
        await _context.SaveChangesAsync();
        return drugType;
    }
    public async Task<bool> SoftDeleteDrugTypeAsync(int id)
    {
        // Tìm loại thuốc theo ID
        var drugType = await GetDrugTypeByIdAsync(id);

        // Nếu không tìm thấy loại thuốc, trả về false
        if (drugType == null) return false;

        // Đánh dấu bản ghi là đã xóa
        drugType.IsDeleted = 1; // Đặt trạng thái xóa mềm (1: đã xóa)
        drugType.UpdatedDate = DateTime.UtcNow; // Cập nhật ngày sửa đổi cuối cùng

        // Lưu thay đổi vào cơ sở dữ liệu
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteDrugTypeAsync(int id)
    {
        var drugType = await GetDrugTypeByIdAsync(id);
        if (drugType == null) return false;

        _context.DrugTypes.Remove(drugType);
        await _context.SaveChangesAsync();
        return true;
    }
}
