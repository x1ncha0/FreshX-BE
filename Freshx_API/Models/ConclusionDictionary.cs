using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models;

public partial class ConclusionDictionary
{
    [Key]
    public int ConclusionDictionaryId { get; set; } // ID từ điển kết luận

    public string? Code { get; set; } // Mã từ điển kết luận

    public string? Name { get; set; } // Tên từ điển kết luận

    public string? SequenceNumber { get; set; } // Số thứ tự

    public string? Title { get; set; } // Tiêu đề kết luận

    public int? Gender { get; set; } // Giới tính

    public string? Diagnosis { get; set; } // Chẩn đoán

    public bool? CalculateDueDate { get; set; } // Tính toán ngày đến hạn

    public string? Conclusion { get; set; } // Nội dung kết luận

    public string? Description { get; set; } // Mô tả kết luận

    public string? Note { get; set; } // Ghi chú

    public string? MedicalAdvice { get; set; } // Lời khuyên y tế

    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? UpdatedBy { get; set; } // Người cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    public int? ServiceCatalogId { get; set; } // ID danh mục dịch vụ

    public virtual ServiceCatalog? ServiceCatalog { get; set; } // Danh mục dịch vụ
}
