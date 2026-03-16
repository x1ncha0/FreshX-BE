using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models;

public partial class DiagnosisDictionary
{
    [Key]
    public int DiagnosisDictionaryId { get; set; } // ID của từ điển chẩn đoán

    public string? Code { get; set; } // Mã chẩn đoán

    public string? Name { get; set; } // Tên chẩn đoán

    public string? SequenceNumber { get; set; } // Số thứ tự

    public string? Title { get; set; } // Tiêu đề chẩn đoán

    public string? Diagnosis { get; set; } // Nội dung chẩn đoán

    public bool? CalculateDueDate { get; set; } // Tính toán ngày đến hạn

    public string? MedicalAdvice { get; set; } // Lời khuyên y tế

    public int? IsSuspended { get; set; } // Trạng thái tạm ngưng

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? UpdatedBy { get; set; } // Người cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa
}
