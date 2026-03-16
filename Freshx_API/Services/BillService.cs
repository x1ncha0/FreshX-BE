using AutoMapper;
using Freshx_API.Dtos.Payments;
using Freshx_API.Interfaces.Payments;
using Freshx_API.Models;

namespace Freshx_API.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _repository;
        private readonly IMapper _mapper;


        public BillingService(IBillingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BillDto> CreateBillAsync(BillDto billDto)
        {
            var bill = _mapper.Map<Bill>(billDto);
            bill.PaymentStatus = "Pending";
            var createdBill = await _repository.AddBillAsync(bill);
            return _mapper.Map<BillDto>(createdBill);
        }

        public async Task<BillDto> GetBillByIdAsync(int billId)
        {
            var bill = await _repository.GetBillByIdAsync(billId);
            return _mapper.Map<BillDto>(bill);
        }

        public async Task<IEnumerable<BillDto>> GetAllBillsAsync()
        {
            var bills = await _repository.GetAllBillsAsync();
            return _mapper.Map<IEnumerable<BillDto>>(bills);
        }

        public async Task<PaymentDto> ProcessPaymentAsync(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            await _repository.AddPaymentAsync(payment);
            var bill = await _repository.GetBillByIdAsync(payment.BillId);
            if (bill != null)
            {
                bill.TotalAmount -= payment.AmountPaid;
                if (bill.TotalAmount >= payment.AmountPaid)
                {
                    bill.PaymentStatus = "Paid";
                    bill.TotalAmount = 0;
                }
                else 
                {
                    bill.PaymentStatus = "Partially Paid";
                }
                if(bill.TotalAmount == 0)
                {
                    bill.PaymentStatus = "Paid";
                }
                await _repository.UpdateAsync(bill);
            }
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<BillDto> UpdateBillAsync(int billId, BillDtoUpdate billDtoUpdate)
        {
            var bill = await _repository.GetBillByIdAsync(billId);
            if (bill == null)
            {
                return null; 
            }

            _mapper.Map(billDtoUpdate, bill);
            var updatedBill = await _repository.UpdateAsync(bill);

            return _mapper.Map<BillDto>(updatedBill);
        }

        public async Task<bool> DeleteBillAsync(int billId)
        {
            var bill = await _repository.GetBillByIdAsync(billId);
            if (bill == null)
            {
                return false; 
            }

            await _repository.DeleteAsync(bill);
            return true; 
        }
        public async Task<BillDto> GetBillWithDetailsAsync(int billId)
        {
            var bill = await _repository.GetBillWithDetailsAsync(billId);
            return _mapper.Map<BillDto>(bill); 
        }
    }
}
