namespace Freshx_API.Dtos.Country
{
    public class CountryDto
    {
        public int CountryId { get; set; } // ID quốc gia
        public string? Code { get; set; } // Mã quốc gia
        public string? Name { get; set; } // Tên quốc gia
        public string? NameEnglish { get; set; } // Tên quốc gia bằng tiếng Anh
        public string? NameLatin { get; set; } // Tên quốc gia bằng tiếng Latin
        public string? ShortName { get; set; } // Tên viết tắt của quốc gia
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
        public int? IsDeleted { get; set; } // Trạng thái đã xóa
        public DateTime? CreatedDate { get; set; } // Ngày tạo
        public string? CreatedBy { get; set; } // Người tạo
        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật
        public string? UpdatedBy { get; set; } // Người cập nhật
    }
}
