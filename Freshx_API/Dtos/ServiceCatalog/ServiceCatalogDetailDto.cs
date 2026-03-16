using Freshx_API.Models;

namespace Freshx_API.Dtos.ServiceCatalog
{
    // DTO để trả dữ liệu chi tiết về danh mục dịch vụ
    public class ServiceCatalogDetailDto
    {
        public int ServiceCatalogId { get; set; } // ID danh mục dịch vụ
        public string? Code { get; set; } // Mã danh mục dịch vụ
        public string? Name { get; set; } // Tên danh mục dịch vụ
        public decimal? Price { get; set; } // Giá dịch vụ
        public string? UnitOfMeasure { get; set; } // Đơn vị đo lường
        public bool? HasStandardValue { get; set; } // Có giá trị tiêu chuẩn không
        public int? Level { get; set; } // Cấp độ dịch vụ
        public bool? IsParentService { get; set; } // Trạng thái dịch vụ cha
        public int? ParentServiceId { get; set; } // ID dịch vụ cha
        public int? ServiceGroupId { get; set; } // ID nhóm dịch vụ
        public int? serviceTypeId { get; set; }
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
        public string? CreatedBy { get; set; } // Người tạo
        public DateTime? CreatedDate { get; set; } // Ngày tạo
        public string? UpdatedBy { get; set; } // Người cập nhật
        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật
        public int? IsDeleted { get; set; } // Trạng thái đã xóa
        //public int? PriceTypeId { get; set; } // ID loại giá
        public string? ParentServiceName { get; set; } // Tên dịch vụ cha
        public string? ServiceGroupName { get; set; } // Tên nhóm dịch vụ
        public virtual ServiceTypes? ServiceTypes { get; set; }
    }
}
