namespace Freshx_API.Dtos.Country
{
    public class CountryCreateUpdateDto
    {
        public string? Code { get; set; } // Mã quốc gia
        public string? Name { get; set; } // Tên quốc gia
        public string? NameEnglish { get; set; } // Tên quốc gia bằng tiếng Anh
        public string? NameLatin { get; set; } // Tên quốc gia bằng tiếng Latin
        public string? ShortName { get; set; } // Tên viết tắt của quốc gia
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
        public int? IsDeleted { get; set; } // Trạng thái đã xóa
    }
}
