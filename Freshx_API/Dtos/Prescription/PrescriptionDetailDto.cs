using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Freshx_API.Dtos.Prescription
{
    public class CreatePrescriptionDetailDto
    {
        public int PrescriptionId { get; set; } // ID đơn thuốc (FK)
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
    }



    public class DetailDto
    {
        public int PrescriptionDetailId { get; set; } // ID chi tiết toa thuốc
        public int PrescriptionId { get; set; } // ID đơn thuốc (FK)
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
    }
    public class UpdatePrescriptionDetailDto
    {
        public int PrescriptionDetailId { get; set; }
        public int PrescriptionId { get; set; }
        public int DrugCatalogId { get; set; }
        public decimal? MorningDose { get; set; }
        public decimal? NoonDose { get; set; }
        public decimal? AfternoonDose { get; set; }
        public decimal? EveningDose { get; set; }
        public decimal? DaysOfSupply { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Note { get; set; }
    }


}
