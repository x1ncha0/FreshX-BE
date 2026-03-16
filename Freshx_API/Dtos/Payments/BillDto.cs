namespace Freshx_API.Dtos.Payments
{
    public class BillDto
    {
        public int? BillId { get; set; }
        public int? ReceptionId { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? PaymentStatus { get; set; } = "Pending";
        public List<BillDetailDto> BillDetails { get; set; } = new();
    }
    public class BillDtoUpdate
    {
        public int BillId { get; set; }
        public int? ReceptionId { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<BillDetailDtoUpdate> BillDetails { get; set; } = new List<BillDetailDtoUpdate>();
    }

}
