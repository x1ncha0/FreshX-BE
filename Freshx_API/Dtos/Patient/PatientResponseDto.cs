using Freshx_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Dtos.Patient
{
    public class PatientResponseDto
    {
        public int PatientId { get; set; } // ID bệnh nhân
        public string? MedicalRecordNumber { get; set; } // Số hồ sơ bệnh án {tự sinh} vd: bn001}
        public string? IdentityCardNumber { get; set; } // Số CMND/CCCD ktra trùng lặp

        public string? AdmissionNumber { get; set; } // Số nhập viện {tự sinh vd: nv171224001 kết hợp giữa ngày nhập viện và số thứ tuwh trong ngày}

        public string? Name { get; set; } // Tên bệnh nhân

        public string? Gender { get; set; } // Giới tính bệnh nhân
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; } // Ngày sinh bệnh nhân

        public string? PhoneNumber { get; set; } // Số điện thoại bệnh nhân
        public string? Email { get; set; }

        // Địa chỉ chi tiết bệnh nhân
        public string? Address {  get; set; }        
        public string? WardId { get; set; } // ID phường/xã
        public string? DistrictId { get; set; } // ID quận/huyện
        public string? ProvinceId { get; set; } // ID tỉnh/thành phố
        public DateTime? CreatedDate { get; set; } // Ngày tạo
        public int? ImageId { get; set; }
        public int? IsDeleted { get; set; } // Trạng thái đã xóa
        public string? Ethnicity { get; set; } // Dân tộc của bệnh nhân
    }
}
