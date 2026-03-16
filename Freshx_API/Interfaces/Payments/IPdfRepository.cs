namespace Freshx_API.Interfaces.Payments
{
    public interface IPdfRepository
    {
        Task<byte[]> GenerateBillPdfAsync(int billId);
    }
}
