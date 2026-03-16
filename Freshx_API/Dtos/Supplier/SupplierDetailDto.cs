namespace Freshx_API.Dtos.Supplier
{
    // DTO để trả dữ liệu chi tiết về client
    public class SupplierDetailDto
    {
        public int SupplierId { get; set; } // ID nhà cung cấp
        public string? Code { get; set; } // Mã nhà cung cấp
        public string? Name { get; set; } // Tên nhà cung cấp
        public string? NameEnglish { get; set; } // Tên tiếng Anh
        public string? NameRussian { get; set; } // Tên tiếng Nga
        public string? Address { get; set; } // Địa chỉ
        public string? PhoneNumber { get; set; } // Số điện thoại
        public string? Fax { get; set; } // Số fax
        public string? Email { get; set; } // Địa chỉ email
        public string? TaxCode { get; set; } // Mã số thuế
        public string? Director { get; set; } // Giám đốc
        public string? ContactPerson { get; set; } // Người liên hệ
        public bool? IsForeign { get; set; } // Trạng thái nước ngoài
        public bool IsStateOwned { get; set; } // Trạng thái nhà nước
        public bool IsSuspended { get; set; } // Trạng thái tạm ngưng
        public string? NameUnaccented { get; set; } // Tên không dấu
        public DateTime? CreatedDate { get; set; } // Ngày tạo
        public string? CreatedBy { get; set; } // Người tạo
        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật
        public string? UpdatedBy { get; set; } // Người cập nhật
        public bool IsPharmaceuticalSupplier { get; set; } // Nhà cung cấp dược phẩm
        public bool IsMedicalConsumableSupplier { get; set; } // Nhà cung cấp vật tư y tế
        public bool IsAssetSupplier { get; set; } // Nhà cung cấp tài sản
        public int? IsDeleted { get; set; } // Trạng thái đã xóa
    }
}
