namespace FreshX.Application.Interfaces.Payments
{
    public interface IPdfRepository
    {
        Task<byte[]> GenerateBillPdfAsync(int billId);
    }
}

