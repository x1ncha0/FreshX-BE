using Freshx_API.Dtos.ServiceCatalog;
namespace Freshx_API.Dtos.ServiceGroup
{
    // DTO để trả dữ liệu chi tiết về nhóm dịch vụ
    public class ServiceGroupDetailDto
    {
        public int ServiceGroupId { get; set; } // ID nhóm dịch vụ
        public string? Code { get; set; } // Mã nhóm dịch vụ
        public string? Name { get; set; } // Tên nhóm dịch vụ
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
        public string? CreatedBy { get; set; } // Người tạo
        public DateTime? CreatedDate { get; set; } // Thời gian tạo
        public string? UpdatedBy { get; set; } // Người cập nhật
        public DateTime? UpdatedDate { get; set; } // Thời gian cập nhật
        public int? IsDeleted { get; set; } // Trạng thái đã xóa
        public List<ServiceCatalogDto>? ServiceCatalogs { get; set; }
    }
}
