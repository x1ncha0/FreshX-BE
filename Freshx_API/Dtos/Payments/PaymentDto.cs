namespace Freshx_API.Dtos.Payments
{
    public class PaymentDto
    {
        public int? BillId { get; set; }
        public decimal? AmountPaid { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public string? PaymentMethod { get; set; }
    }

}
