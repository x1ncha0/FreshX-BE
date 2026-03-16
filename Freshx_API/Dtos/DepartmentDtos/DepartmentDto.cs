using Freshx_API.Models;

namespace Freshx_API.Dtos.DepartmentDtos
{
    // DTO để trả dữ liệu cơ bản về client (danh sách phòng ban)
    public class DepartmentDto
    {
        public int DepartmentId { get; set; } // ID phòng ban
        public string? Code { get; set; } // Mã phòng ban
        public string? Name { get; set; } // Tên phòng ban
        public int? DepartmentTypeId { get; set; } // ID loại phòng ban
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
        public DepartmentType? DepartmentType { get; set; } //DepartmentType
        public DateTime? CreatedDate { get; set; } // Thời gian tạo
    }
}
