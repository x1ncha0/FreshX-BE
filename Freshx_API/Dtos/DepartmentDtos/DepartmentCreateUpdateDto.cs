using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.DepartmentDtos
{
    // DTO để nhận dữ liệu từ client khi tạo hoặc cập nhật phòng ban
    public class DepartmentCreateUpdateDto   
    {
        [Required(ErrorMessage = "Loại phòng ban là bắt buộc")]
        public int DepartmentTypeId { get; set; } // ID loại phòng ban
        public int? IsSuspended { get; set; } // Trạng thái tạm ngưng
    }
}
