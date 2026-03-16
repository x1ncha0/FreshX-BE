namespace Freshx_API.Interfaces.Payments
{
    public interface IPdfService
    {
        Task<byte[]> GenerateBillPdfAsync(int billId);
    }
}
