using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Models
{
    public class BillDetail
    {
        [Key]
        public int BillDetailId { get; set; } // ID chi tiết hóa đơn

        public int BillId { get; set; } // ID hóa đơn liên quan

        public int ServiceCatalogId { get; set; } // ID dịch vụ được sử dụng

        [Required]
        public int Quantity { get; set; } // Số lượng dịch vụ

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; } // Giá đơn vị của dịch vụ

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Subtotal { get; set; } // Tổng tiền (Quantity * UnitPrice)

        // Quan hệ
        public virtual Bill Bill { get; set; } // Tham chiếu đến hóa đơn
        public virtual ServiceCatalog ServiceCatalog { get; set; } // Tham chiếu đến dịch vụ
    }
}
