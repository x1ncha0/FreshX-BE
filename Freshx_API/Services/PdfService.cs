using Freshx_API.Interfaces.Payments;
using System.Threading.Tasks;

namespace Freshx_API.Services
{
    public class PdfService : IPdfService
    {
        private readonly IPdfRepository _pdfRepository;

        public PdfService(IPdfRepository pdfRepository)
        {
            _pdfRepository = pdfRepository;
        }

        public async Task<byte[]> GenerateBillPdfAsync(int billId)
        {
            return await _pdfRepository.GenerateBillPdfAsync(billId);
        }
    }
}
