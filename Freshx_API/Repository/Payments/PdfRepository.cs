using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Freshx_API.Models;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Microsoft.EntityFrameworkCore;
using Freshx_API.Interfaces.Payments;

namespace Freshx_API.Repository.Payments
{
    public class PdfRepository : IPdfRepository
    {
        private readonly FreshxDBContext _context;

        public PdfRepository(FreshxDBContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerateBillPdfAsync(int billId)
        {
            var bill = await _context.Bills
                .Include(b => b.BillDetails)
                .ThenInclude(bd => bd.ServiceCatalog)
                .Include(b => b.Reception)
                .ThenInclude(r => r.Patient)
                .FirstOrDefaultAsync(b => b.BillId == billId);

            if (bill == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                var fontTitle = new XFont("Arial", 20);
                var fontText = new XFont("Arial", 12);

                double yPosition = 50;

                // Header
                gfx.DrawString("HÓA ĐƠN", fontTitle, XBrushes.Black, new XPoint(200, yPosition));
                yPosition += 30;

                // Bill Information
                gfx.DrawString($"Mã hóa đơn: {bill.BillId}", fontText, XBrushes.Black, new XPoint(50, yPosition));
                yPosition += 20;

                // Uncomment and adjust these lines if necessary
                gfx.DrawString($"Tên bệnh nhân: {bill.Reception?.Patient?.Name}", fontText, XBrushes.Black, 50, yPosition);
                yPosition += 20;

                gfx.DrawString($"Giới tính: {bill.Reception?.Patient?.Gender}", fontText, XBrushes.Black, 50, yPosition);
                yPosition += 20;

                gfx.DrawString($"Ngày sinh: {bill.Reception?.Patient?.DateOfBirth:dd/MM/yyyy}", fontText, XBrushes.Black, 50, yPosition);
                yPosition += 20;

                gfx.DrawString($"Số điện thoại: {bill.Reception?.Patient?.PhoneNumber}", fontText, XBrushes.Black, 50, yPosition);
                yPosition += 20;

                gfx.DrawString($"Địa chỉ: {bill.Reception?.Patient?.Address}", fontText, XBrushes.Black, 50, yPosition);
                yPosition += 20;

                gfx.DrawString($"Tổng số tiền: {bill.TotalAmount:C}", fontText, XBrushes.Black, new XPoint(50, yPosition));
                yPosition += 20;

                gfx.DrawString($"Trạng thái thanh toán: {bill.PaymentStatus}", fontText, XBrushes.Black, new XPoint(50, yPosition));
                yPosition += 20;

                gfx.DrawString($"Số tiền đã thanh toán: {bill.TotalAmount:C}", fontText, XBrushes.Black, new XPoint(50, yPosition));
                yPosition += 20;

                // Bill Details
                if (bill.BillDetails.Any())
                {
                    gfx.DrawString("Chi tiết hóa đơn:", fontText, XBrushes.Black, new XPoint(50, yPosition));
                    yPosition += 20;

                    foreach (var detail in bill.BillDetails)
                    {
                        gfx.DrawString(
                            $"Dịch vụ: {detail.ServiceCatalog.Name}, Số lượng: {detail.Quantity}, Giá: {detail.Subtotal:C}",
                            fontText, XBrushes.Black, new XPoint(50, yPosition));
                        yPosition += 20;
                    }
                }

                // Save to MemoryStream
                document.Save(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream.ToArray();
            }
        }
    }
}
