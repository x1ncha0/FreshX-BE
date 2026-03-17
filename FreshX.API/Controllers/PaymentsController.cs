using FreshX.Application.Dtos.Payments;
using FreshX.Application.Interfaces.Payments;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(
    IBillingService billingService,
    IPdfService pdfService) : ControllerBase
{
    [HttpGet("bills")]
    public async Task<IActionResult> GetBills(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await billingService.GetAllBillsAsync());
    }

    [HttpGet("bills/{billId:int}")]
    public async Task<IActionResult> GetBillById(int billId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await billingService.GetBillByIdAsync(billId));
    }

    [HttpGet("bills/{billId:int}/details")]
    public async Task<IActionResult> GetBillWithDetails(int billId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await billingService.GetBillWithDetailsAsync(billId));
    }

    [HttpPost("bills")]
    public async Task<IActionResult> CreateBill([FromBody] BillDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await billingService.CreateBillAsync(dto));
    }

    [HttpPut("bills/{billId:int}")]
    public async Task<IActionResult> UpdateBill(int billId, [FromBody] BillDtoUpdate dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await billingService.UpdateBillAsync(billId, dto));
    }

    [HttpDelete("bills/{billId:int}")]
    public async Task<IActionResult> DeleteBill(int billId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await billingService.DeleteBillAsync(billId));
    }

    [HttpPost("payments")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto dto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await billingService.ProcessPaymentAsync(dto));
    }

    [HttpGet("bills/{billId:int}/pdf")]
    public async Task<IActionResult> GenerateBillPdf(int billId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var pdf = await pdfService.GenerateBillPdfAsync(billId);
        return File(pdf, "application/pdf", $"bill-{billId}.pdf");
    }
}
