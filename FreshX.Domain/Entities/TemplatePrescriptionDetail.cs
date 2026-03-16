using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public partial class TemplatePrescriptionDetail : BaseEntity
    {
        /// <summary>
        /// ID danh mục thuốc (FK)
        /// </summary>
        public int DrugCatalogId { get; set; }

        /// <summary>
        /// Liều buổi sáng 
        /// </summary>
        public decimal? MorningDose { get; set; }

        /// <summary>
        /// Liều buổi trưa
        /// </summary>
        public decimal? NoonDose { get; set; }

        /// <summary>
        /// Liều buổi chiều
        /// </summary>
        public decimal? AfternoonDose { get; set; }

        /// <summary>
        /// Liều buổi tối
        /// </summary>
        public decimal? EveningDose { get; set; }

        /// <summary>
        /// Số ngày sử dụng thuốc
        /// </summary>
        public decimal? DaysOfSupply { get; set; } 

        /// <summary>
        /// Số lượng thuốc
        /// </summary>
        public decimal? Quantity { get; set; } 

        /// <summary>
        /// Thành tiền của thuốc
        /// </summary>
        public decimal? TotalAmount { get; set; } 

        /// <summary>
        /// Ghi chú riêng
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Toa thuốc
        /// </summary>
        public virtual Prescription? Prescription { get; set; }

        public virtual TemplatePrescription? TemplatePrescription { get; set; }

        /// <summary>
        /// Danh mục thuốc
        /// </summary>
        public virtual DrugCatalog? DrugCatalog { get; set; }
    }
}