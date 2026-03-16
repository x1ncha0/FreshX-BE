using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.DepartmenTypeDtos
{
    // DTO để nhận dữ liệu từ client khi tạo hoặc cập nhật phòng ban
    public class DepartmentTypeCreateUpdateDto
    {
        [Required(ErrorMessage = "Tên loại phòng ban là bắt buộc")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên phải từ 2-100 ký tự")]
        [RegularExpression(@"^[a-zA-ZÀ-ỹ\s]*$", ErrorMessage = "Tên loại phòng ban chỉ được chứa chữ cái và khoảng trắng")]
        public string? Name { get; set; } // Tên phòng ban
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
    }
}
