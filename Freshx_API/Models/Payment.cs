using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; } // ID thanh toán

        public int BillId { get; set; } // ID hóa đơn liên quan

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountPaid { get; set; } // Số tiền đã thanh toán

        [Required]
        [StringLength(20)]
        public string PaymentMethod { get; set; } // Phương thức thanh toán (Cash, Card, etc.)

        public DateTime PaymentDate { get; set; } = DateTime.Now; // Ngày thanh toán

        // Quan hệ
        public virtual Bill Bill { get; set; } // Tham chiếu đến hóa đơn
    }
}
