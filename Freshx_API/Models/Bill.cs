using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freshx_API.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; } // ID hóa đơn

        public int ReceptionId { get; set; } // ID tiếp nhận liên quan đến hóa đơn
        public int? CashierId { get; set; } // nhân viên tiếp nhận
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; } // Tổng số tiền

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; } = "Pending"; // Trạng thái thanh toán (Pending, Paid)

        public DateTime CreatedDate { get; set; } = DateTime.Now; // Ngày tạo hóa đơn

        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật

        // Quan hệ
        public virtual Reception Reception { get; set; } // Tham chiếu đến
        public virtual ICollection<BillDetail> BillDetails { get; set; } = new HashSet<BillDetail>(); // Danh sách chi tiết hóa đơn
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>(); // Danh sách thanh toán
        [ForeignKey("CashierId")]
        public virtual Employee? Cashier { get; set; } // nhân viên tiếp nhận
    }
}
