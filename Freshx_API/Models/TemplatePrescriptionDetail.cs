using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Models
{
    public partial class TemplatePrescriptionDetail
    {
        [Key]
        public int PrescriptionDetailId { get; set; } // ID chi tiết toa thuốc

        [ForeignKey("Prescription")]
        public int TemplatePrescriptionId { get; set; } // ID đơn thuốc (FK)

        [ForeignKey("DrugCatalog")]
        public int DrugCatalogId { get; set; } // ID danh mục thuốc (FK)

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MorningDose { get; set; } // Liều buổi sáng 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NoonDose { get; set; } // Liều buổi trưa

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AfternoonDose { get; set; } // Liều buổi chiều

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? EveningDose { get; set; } // Liều buổi tối

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DaysOfSupply { get; set; } // Số ngày sử dụng thuốc

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Quantity { get; set; } // Số lượng thuốc

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; } // Thành tiền của thuốc

        [StringLength(500)]
        public string? Note { get; set; } // Ghi chú riêng

        // Quan hệ
        public virtual Prescription? Prescription { get; set; } // Toa thuốc
        public virtual TemplatePrescription? TemplatePrescription { get; set; }
        public virtual DrugCatalog DrugCatalog { get; set; } // Danh mục thuốc
    }

}
