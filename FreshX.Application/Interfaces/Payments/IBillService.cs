using FreshX.Application.Dtos;
using FreshX.Application.Dtos.Payments;

namespace FreshX.Application.Interfaces.Payments
{
    public interface IBillingService
    {
        Task<BillDto> CreateBillAsync(BillDto billDto);
        Task<BillDto> GetBillByIdAsync(int billId);
        Task<IEnumerable<BillDto>> GetAllBillsAsync();
        Task<BillDto> UpdateBillAsync(int billId, BillDtoUpdate billDtoUpdate);
        Task<bool> DeleteBillAsync(int billId);
        Task<BillDto> GetBillWithDetailsAsync(int billId);
        Task<PaymentDto> ProcessPaymentAsync(PaymentDto paymentDto);
        //Task<byte[]> GenerateBillPdfAsync(int billId);
    }


}

