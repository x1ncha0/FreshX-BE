namespace Freshx_API.Dtos.Pharmacy
{
    // DTO để trả dữ liệu cơ bản về nhà thuốc (danh sách nhà thuốc)
    public class PharmacyDto
    {
        public int PharmacyId { get; set; } // ID nhà thuốc
        public string? Code { get; set; } // Mã nhà thuốc
        public string? Name { get; set; } // Tên nhà thuốc
    }
}
