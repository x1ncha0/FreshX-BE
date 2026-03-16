namespace Freshx_API.Dtos.ServiceGroup
{
    // DTO để trả dữ liệu cơ bản về nhóm dịch vụ (danh sách nhóm dịch vụ)
    public class ServiceGroupDto
    {
        public int ServiceGroupId { get; set; } // ID nhóm dịch vụ
        public string? Code { get; set; } // Mã nhóm dịch vụ
        public string? Name { get; set; } // Tên nhóm dịch vụ
    }
}
