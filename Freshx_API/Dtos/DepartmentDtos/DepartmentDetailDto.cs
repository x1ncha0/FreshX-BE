namespace Freshx_API.Dtos.DepartmentDtos
{
    // DTO để trả dữ liệu chi tiết về client
    public class DepartmentDetailDto
    {
        public int DepartmentId { get; set; } // ID phòng ban
        public string? Code { get; set; } // Mã phòng ban
        public string? Name { get; set; } // Tên phòng ban
        public int? DepartmentTypeId { get; set; } // ID loại phòng ban
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
        public int? IsDeleted { get; set; } // Trạng thái đã xóa
        public string? CreatedBy { get; set; } // Người tạo
        public DateTime? CreatedDate { get; set; } // Thời gian tạo
        public string? UpdatedBy { get; set; } // Người cập nhật
        public DateTime? UpdatedDate { get; set; } // Thời gian cập nhật
    }
}
