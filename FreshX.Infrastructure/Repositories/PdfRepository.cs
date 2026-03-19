using FreshX.Application.Interfaces.Payments;
using FreshX.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace FreshX.Infrastructure.Repositories;

public class PdfRepository(FreshXDbContext context) : IPdfRepository
{
    public async Task<byte[]> GenerateBillPdfAsync(int billId)
    {
        var bill = await context.Bills
            .AsNoTracking()
            .Include(b => b.BillDetails)
            .ThenInclude(bd => bd.ServiceCatalog)
            .Include(b => b.Reception!)
            .ThenInclude(r => r.Patient)
            .FirstOrDefaultAsync(b => b.Id == billId)
            ?? throw new KeyNotFoundException($"Bill {billId} was not found.");

        using var memoryStream = new MemoryStream();
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var fontTitle = new XFont("Arial", 20);
        var fontText = new XFont("Arial", 12);
        double y = 50;

        gfx.DrawString("HOA DON", fontTitle, XBrushes.Black, new XPoint(220, y));
        y += 30;
        gfx.DrawString($"Ma hoa don: {bill.Id}", fontText, XBrushes.Black, new XPoint(50, y));
        y += 20;
        gfx.DrawString($"Ten benh nhan: {bill.Reception?.Patient?.Name}", fontText, XBrushes.Black, new XPoint(50, y));
        y += 20;
        gfx.DrawString($"Tong so tien: {bill.TotalAmount:C}", fontText, XBrushes.Black, new XPoint(50, y));
        y += 20;
        gfx.DrawString($"Trang thai: {bill.PaymentStatus}", fontText, XBrushes.Black, new XPoint(50, y));
        y += 30;

        foreach (var detail in bill.BillDetails)
        {
            var line = $"Dich vu: {detail.ServiceCatalog?.Name}, SL: {detail.Quantity}, Gia: {detail.Subtotal:C}";
            gfx.DrawString(line, fontText, XBrushes.Black, new XPoint(50, y));
            y += 18;
        }

        document.Save(memoryStream, false);
        return memoryStream.ToArray();
    }
}
