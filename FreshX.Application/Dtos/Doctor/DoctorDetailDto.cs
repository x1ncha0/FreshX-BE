namespace FreshX.Application.Dtos.Doctor
{
    public class DoctorDetailDto
    {
        public int DoctorId { get; set; } // ID bác sĩ
        public string? Name { get; set; } // Tên bác sĩ
        public string? Specialty { get; set; } // chuyên ngành và chuyên môn
        public string? Phone { get; set; } // Số điện thoại
        public string? Email { get; set; } // Email 
        public string? Gender { get; set; } //giới tính
        public DateTime? DateOfBirth { get; set; } // Ngày sinh
        public bool IsSuspended { get; set; } // Trạng thái tạm ngưng
        public string? CreatedBy { get; set; } // Người tạo
        public DateTime? CreatedDate { get; set; } // Ngày tạo
        public string? UpdatedBy { get; set; } // Người cập nhật
        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật
        public bool IsDeleted { get; set; } // Trạng thái đã xóa
    }
}

