namespace Freshx_API.Dtos.Payments
{
    public class BillDetailDto
    {
        public int? ServiceCatalogId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
    }
    public class BillDetailDtoUpdate
    {
        public int? BillDetailId { get; set; }
        public int? BillId { get; set; }
        public int? ServiceCatalogId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
    }
}

