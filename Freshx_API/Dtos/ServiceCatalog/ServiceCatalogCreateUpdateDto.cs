using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.ServiceCatalog
{
    // DTO để nhận dữ liệu từ client khi tạo hoặc cập nhật danh mục dịch vụ
    public class ServiceCatalogCreateUpdateDto
    {
        public string? Code { get; set; } // Mã danh mục dịch vụ

        [Required(ErrorMessage = "Tên danh mục dịch vụ là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự.")]
        public string? Name { get; set; } // Tên danh mục dịch vụ

        [Range(0.0, double.MaxValue, ErrorMessage = "Giá dịch vụ phải là số không âm.")]
        public decimal? Price { get; set; } // Giá dịch vụ

        [MaxLength(20, ErrorMessage = "Đơn vị đo lường không được vượt quá 20 ký tự.")]
        public string? UnitOfMeasure { get; set; } // Đơn vị đo lường

        public bool? HasStandardValue { get; set; } // Có giá trị tiêu chuẩn không

        [Range(1, 10, ErrorMessage = "Cấp độ dịch vụ phải từ 1 đến 10.")]
        public int? Level { get; set; } // Cấp độ dịch vụ

        public bool? IsParentService { get; set; } // Trạng thái dịch vụ cha

        public int? ParentServiceId { get; set; } // ID dịch vụ cha

        [Required(ErrorMessage = "Mã danh mục dịch vụ là bắt buộc.")]
        public int? ServiceTypeId { get; set; } // Id loại dịch vụ 

        [Required(ErrorMessage = "Nhóm dịch vụ là bắt buộc.")]
        public int? ServiceGroupId { get; set; } // ID nhóm dịch vụ

        [Range(0, 1, ErrorMessage = "Trạng thái tạm ngưng phải là 0 (hoạt động) hoặc 1 (tạm ngưng).")]
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
    }   
}
