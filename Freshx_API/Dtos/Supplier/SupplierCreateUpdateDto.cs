using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.Supplier
{
    // DTO để nhận dữ liệu từ client khi tạo hoặc cập nhật nhà cung cấp
    public class SupplierCreateDto
    {
        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc.")]
        public string? Name { get; set; }

        [StringLength(100, ErrorMessage = "Tên nhà cung cấp tiếng Anh không được vượt quá 100 ký tự.")]
        public string? NameEnglish { get; set; }

        [StringLength(100, ErrorMessage = "Tên nhà cung cấp tiếng Nga không được vượt quá 100 ký tự.")]
        public string? NameRussian { get; set; }

        [Required(ErrorMessage = "Địa chỉ nhà cung cấp là bắt buộc.")]
        public string? Address { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }

        [Phone(ErrorMessage = "Số fax không hợp lệ.")]
        public string? Fax { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string? Email { get; set; }

        public string? TaxCode { get; set; }
        public string? Director { get; set; }
        public string? ContactPerson { get; set; }
        public bool? IsForeign { get; set; }
        public bool IsStateOwned { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsPharmaceuticalSupplier { get; set; }
        public bool IsMedicalConsumableSupplier { get; set; }
        public bool IsAssetSupplier { get; set; }
    }

    public class SupplierUpdateDto
    {
        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc.")]
        public string? Name { get; set; }

        public string? NameEnglish { get; set; }
        public string? NameRussian { get; set; }

        [Required(ErrorMessage = "Địa chỉ nhà cung cấp là bắt buộc.")]
        public string? Address { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }

        [Phone(ErrorMessage = "Số fax không hợp lệ.")]
        public string? Fax { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string? Email { get; set; }

        public string? TaxCode { get; set; }
        public string? Director { get; set; }
        public string? ContactPerson { get; set; }
        public bool? IsForeign { get; set; }
        public bool IsStateOwned { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsPharmaceuticalSupplier { get; set; }
        public bool IsMedicalConsumableSupplier { get; set; }
        public bool IsAssetSupplier { get; set; }
    }
}
