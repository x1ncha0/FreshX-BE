namespace Freshx_API.Dtos.ServiceGroup
{
    // DTO để nhận dữ liệu từ client khi tạo hoặc cập nhật nhóm dịch vụ
    public class ServiceGroupCreateUpdateDto
    {
        public string? Code { get; set; } // Mã nhóm dịch vụ
        public string? Name { get; set; } // Tên nhóm dịch vụ
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
    }
}
