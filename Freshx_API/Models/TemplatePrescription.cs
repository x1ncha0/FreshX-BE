using NuGet.LibraryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Models;

public partial class TemplatePrescription
{
    public int TemplatePrescriptionId { get; set; } // ID mẫu đơn thuốc

    public string? Code { get; set; } // Mã mẫu đơn thuốc

    public string? Name { get; set; } // Tên mẫu đơn thuốc

    public int? DiagnosisDictionaryId { get; set; } // từ điển chẩn đoán

    [Column(TypeName = "decimal(18, 2)")]
    public int? CreatedBy { get; set; } // Người tạo

    public DateTime? CreatedDate { get; set; } // Ngày tạo

    public int? UpdatedBy { get; set; } // Người cập nhật

    public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

    public int? IsDeleted { get; set; } // Trạng thái đã xóa

    [StringLength(500)]
    public string? Note { get; set; } // Ghi chú chung

    // Quan hệ
    public virtual ICollection<TemplatePrescriptionDetail> TemplatePrescriptionDetails { get; set; }
        = new List<TemplatePrescriptionDetail>(); // Chi tiết toa thuốc

    public virtual ICollection<DiagnosisDictionary> DiagnosisDictionary { get; set; } // từ điển chẩn đoán
    //public virtual ICollection<TemplatePrescriptionDrugMapping> TemplatePrescriptionDrugMappings { get; set; } = new List<TemplatePrescriptionDrugMapping>(); // Danh sách ánh xạ thuốc trong mẫu đơn
}
