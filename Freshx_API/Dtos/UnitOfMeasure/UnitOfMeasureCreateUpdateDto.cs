using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.UnitOfMeasure
{
    // DTO để nhận dữ liệu từ client khi tạo hoặc cập nhật đơn vị đo lường
    public class UnitOfMeasureCreateUpdateDto
    {
        [Required(ErrorMessage = "Tên đơn vị đo lường là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên đơn vị đo lường không được vượt quá 100 ký tự.")]
        public string? Name { get; set; } // Tên đơn vị đo lường

        [StringLength(50, ErrorMessage = "Loại thuốc không được vượt quá 50 ký tự.")]
        public string? DrugType { get; set; } // Loại thuốc

        [Range(0.01, 10000, ErrorMessage = "Giá trị chuyển đổi phải trong phạm vi từ 0.01 đến 10,000.")]
        public decimal? ConversionValue { get; set; } // Giá trị chuyển đổi

        [Range(0, 1, ErrorMessage = "Trạng thái tạm ngưng chỉ nhận giá trị 0 hoặc 1.")]
        public int? IsSuspended { get; set; } = 0;// Trạng thái tạm ngưng


    }

    
}
