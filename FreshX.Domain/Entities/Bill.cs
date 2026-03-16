using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public class Bill : BaseEntity
    {
        /// <summary>
        /// ID tiếp nhận liên quan đến hóa đơn
        /// </summary>
        public int ReceptionId { get; set; }

        /// <summary>
        /// Nhân viên tiếp nhận
        /// </summary>
        public int? CashierId { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Trạng thái thanh toán (Pending, Paid)
        /// </summary>
        public string PaymentStatus { get; set; } = "Pending";

        /// <summary>
        /// Tham chiếu đến
        /// </summary>
        public virtual Reception? Reception { get; set; }

        /// <summary>
        /// Danh sách chi tiết hóa đơn
        /// </summary>
        public virtual ICollection<BillDetail> BillDetails { get; set; } = new HashSet<BillDetail>();

        /// <summary>
        /// Danh sách thanh toán
        /// </summary>
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

        /// <summary>
        /// Nhân viên tiếp nhận
        /// </summary>
        public virtual Employee? Cashier { get; set; }
    }
}