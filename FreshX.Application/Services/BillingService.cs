using FreshX.Application.Dtos.Payments;
using FreshX.Application.Interfaces.Payments;
using FreshX.Domain.Entities;

namespace FreshX.Application.Services;

public class BillingService(IBillingRepository repository) : IBillingService
{
    public async Task<BillDto> CreateBillAsync(BillDto billDto)
    {
        var bill = new Bill
        {
            ReceptionId = billDto.ReceptionId ?? 0,
            TotalAmount = billDto.TotalAmount ?? 0,
            PaymentStatus = string.IsNullOrWhiteSpace(billDto.PaymentStatus) ? "Pending" : billDto.PaymentStatus,
            CreatedDate = DateTime.UtcNow,
            BillDetails = billDto.BillDetails.Select(static d => new BillDetail
            {
                ServiceCatalogId = d.ServiceCatalogId ?? 0,
                Quantity = d.Quantity ?? 0,
                UnitPrice = d.UnitPrice ?? 0,
                Subtotal = d.Subtotal ?? 0
            }).ToList()
        };

        var created = await repository.AddBillAsync(bill);
        return ToDto(created);
    }

    public async Task<BillDto> GetBillByIdAsync(int billId) => ToDto(await repository.GetBillByIdAsync(billId));

    public async Task<IEnumerable<BillDto>> GetAllBillsAsync() => (await repository.GetAllBillsAsync()).Select(ToDto).ToList();

    public async Task<PaymentDto> ProcessPaymentAsync(PaymentDto paymentDto)
    {
        var payment = new Payment
        {
            BillId = paymentDto.BillId ?? 0,
            AmountPaid = paymentDto.AmountPaid ?? 0,
            PaymentDate = paymentDto.PaymentDate ?? DateTime.UtcNow,
            PaymentMethod = paymentDto.PaymentMethod ?? string.Empty
        };

        await repository.AddPaymentAsync(payment);
        var bill = await repository.GetBillByIdAsync(payment.BillId);

        bill.TotalAmount -= payment.AmountPaid;
        if (bill.TotalAmount <= 0)
        {
            bill.TotalAmount = 0;
            bill.PaymentStatus = "Paid";
        }
        else
        {
            bill.PaymentStatus = "Partially Paid";
        }

        await repository.UpdateAsync(bill);

        return new PaymentDto
        {
            BillId = payment.BillId,
            AmountPaid = payment.AmountPaid,
            PaymentDate = payment.PaymentDate,
            PaymentMethod = payment.PaymentMethod
        };
    }

    public async Task<BillDto> UpdateBillAsync(int billId, BillDtoUpdate billDtoUpdate)
    {
        var bill = await repository.GetBillByIdAsync(billId);
        bill.ReceptionId = billDtoUpdate.ReceptionId ?? bill.ReceptionId;
        bill.TotalAmount = billDtoUpdate.TotalAmount ?? bill.TotalAmount;
        bill.PaymentStatus = billDtoUpdate.PaymentStatus ?? bill.PaymentStatus;
        bill.UpdatedDate = DateTime.UtcNow;

        var updated = await repository.UpdateAsync(bill);
        return ToDto(updated);
    }

    public async Task<bool> DeleteBillAsync(int billId)
    {
        var bill = await repository.GetBillByIdAsync(billId);
        await repository.DeleteAsync(bill);
        return true;
    }

    public async Task<BillDto> GetBillWithDetailsAsync(int billId) => ToDto(await repository.GetBillWithDetailsAsync(billId));

    private static BillDto ToDto(Bill bill) => new()
    {
        BillId = bill.Id,
        ReceptionId = bill.ReceptionId,
        TotalAmount = bill.TotalAmount,
        PaymentStatus = bill.PaymentStatus,
        BillDetails = bill.BillDetails.Select(static detail => new BillDetailDto
        {
            ServiceCatalogId = detail.ServiceCatalogId,
            Quantity = detail.Quantity,
            UnitPrice = detail.UnitPrice,
            Subtotal = detail.Subtotal
        }).ToList()
    };
}