using Freshx_API.Models;

namespace Freshx_API.Interfaces.Payments
{
    public interface IBillingRepository
    {
        Task<Bill> AddBillAsync(Bill bill);
        Task<Bill> GetBillByIdAsync(int billId);
        Task<IEnumerable<Bill>> GetAllBillsAsync();
        Task AddPaymentAsync(Payment payment);
        Task<Bill> UpdateAsync(Bill bill);
        Task<Bill> DeleteAsync(Bill bill);
        Task<Bill> GetBillWithDetailsAsync(int billId);
        //Task<byte[]> GenerateBillPdfAsync(int billId);
    }
}


