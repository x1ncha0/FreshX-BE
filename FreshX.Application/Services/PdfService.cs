using FreshX.Application.Interfaces.Payments;

namespace FreshX.Application.Services;

public class PdfService(IPdfRepository pdfRepository) : IPdfService
{
    public Task<byte[]> GenerateBillPdfAsync(int billId) => pdfRepository.GenerateBillPdfAsync(billId);
}