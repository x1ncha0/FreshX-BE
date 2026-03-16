using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public partial class PrescriptionDetail : BaseEntity
    {
        public int PrescriptionId { get; set; } // ID đơn thuốc (FK)

        public int DrugCatalogId { get; set; } // ID danh mục thuốc (FK)

        public decimal? MorningDose { get; set; } // Liều buổi sáng 

        public decimal? NoonDose { get; set; } // Liều buổi trưa

        public decimal? AfternoonDose { get; set; } // Liều buổi chiều

        public decimal? EveningDose { get; set; } // Liều buổi tối

        public decimal? DaysOfSupply { get; set; } // Số ngày sử dụng thuốc

        public decimal? Quantity { get; set; } // Số lượng thuốc

        public decimal? TotalAmount { get; set; } // Thành tiền của thuốc

        public string? Note { get; set; } // Ghi chú riêng

        public virtual Prescription? Prescription { get; set; } // Toa thuốc

        public virtual TemplatePrescription? TemplatePrescription { get; set; }

        public virtual DrugCatalog? DrugCatalog { get; set; } // Danh mục thuốc
    }
}