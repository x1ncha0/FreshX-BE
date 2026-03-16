using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Humanizer;

namespace Freshx_API.Models;
public partial class Prescription
{
    [Key]
    public int PrescriptionId { get; set; } // ID đơn thuốc

    public int? MedicalExaminationId { get; set; } // ID khám bệnh

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; } // Tổng số tiền

    public bool? IsPaid { get; set; } // Trạng thái đã thanh toán

    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? UpdatedBy { get; set; } // Người cập nhật

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    [StringLength(500)]
    public string? Note { get; set; } // Ghi chú chung

    // Quan hệ
    public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; }
        = new List<PrescriptionDetail>(); // Chi tiết toa thuốc
    // tách phần danh mục thuốc và chi tiết toa thuốc ra
    //Truy vấn toa thuốc:
    //Sử dụng Include để lấy chi tiết toa thuốc kèm danh sách thuốc.
    //var prescription = context.Prescriptions
    //.Include(p => p.PrescriptionDetails)
    //.ThenInclude(d => d.DrugCatalog)
    //.FirstOrDefault(p => p.PrescriptionId == id);

}
