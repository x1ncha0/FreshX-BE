using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int BillId { get; set; } // ID hóa đơn liên quan

        public decimal AmountPaid { get; set; } // Số tiền đã thanh toán

        public string PaymentMethod { get; set; } = string.Empty; // Phương thức thanh toán (Cash, Card, etc.)

        public DateTime PaymentDate { get; set; } = DateTime.Now; // Ngày thanh toán

        public virtual Bill? Bill { get; set; } // Tham chiếu đến hóa đơn
    }
}