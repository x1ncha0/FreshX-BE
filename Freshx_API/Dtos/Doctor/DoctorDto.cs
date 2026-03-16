namespace Freshx_API.Dtos.Doctor
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string? IdentityCardNumber { get; set; } // Số CMND/CCCD ktra trùng lặp
        public string? Name { get; set; } // Tên bác sĩ
        public string? Specialty { get; set; } // chuyên ngành và chuyên môn
        public string? Phone { get; set; } // Số điện thoại
        public string? Email { get; set; } // Email 
        public string? Gender { get; set; } //giới tính
        public DateTime? DateOfBirth { get; set; } // Ngày sinh
        public int? AvataId { get; set; } // ảnh
        public string? Address { get; set; }
        public string? PositionName { get; set; }
        public string? DepartmentName { get; set; }    
    }
}
