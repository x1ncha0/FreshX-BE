namespace Freshx_API.Dtos.Pharmacy
{
    // DTO để trả dữ liệu chi tiết về nhà thuốc cho client
    public class PharmacyDetailDto
    {
        public int PharmacyId { get; set; } // ID nhà thuốc
        public string? Code { get; set; } // Mã nhà thuốc
        public string? Name { get; set; } // Tên nhà thuốc
        public int? DepartmentId { get; set; } // ID phòng ban
        public int? InventoryTypeId { get; set; } // ID loại tồn kho
        public bool? IsSuspended { get; set; } // Trạng thái tạm ngưng
        public string? NameUnaccented { get; set; } // Tên không dấu của nhà thuốc
        public DateTime? CreatedDate { get; set; } // Ngày tạo
        public string? CreatedBy { get; set; } // Người tạo
        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật
        public string? UpdatedBy { get; set; } // Người cập nhật
        public bool? IsSourceManagement { get; set; } // Trạng thái quản lý nguồn
        public int? SpecialtyId { get; set; } // ID chuyên khoa
        public int? CostCenterId { get; set; } // ID trung tâm chi phí
        public int? IsDeleted { get; set; } // Trạng thái đã xóa
    }
}
