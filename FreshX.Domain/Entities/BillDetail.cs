using FreshX.Domain.Common;

namespace FreshX.Domain.Entities
{
    public class BillDetail : BaseEntity
    {
        /// <summary>
        /// ID hóa đơn liên quan
        /// </summary>
        public int BillId { get; set; }

        /// <summary>
        /// ID dịch vụ được sử dụng
        /// </summary>
        public int ServiceCatalogId { get; set; }

        /// <summary>
        /// Số lượng dịch vụ
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Giá đơn vị của dịch vụ
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Tổng tiền (Quantity * UnitPrice)
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Tham chiếu đến hóa đơn
        /// </summary>
        public virtual Bill? Bill { get; set; }

        /// <summary>
        /// Tham chiếu đến dịch vụ
        /// </summary>
        public virtual ServiceCatalog? ServiceCatalog { get; set; }
    }
}