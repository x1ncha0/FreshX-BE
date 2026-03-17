namespace FreshX.Application.Interfaces.Payments
{
    public interface IPdfService
    {
        Task<byte[]> GenerateBillPdfAsync(int billId);
    }
}

