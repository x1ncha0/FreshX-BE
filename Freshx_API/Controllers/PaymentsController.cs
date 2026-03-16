using Freshx_API.Dtos;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Dtos.DrugCatalog;
using Freshx_API.Dtos.Payments;
using Freshx_API.Interfaces.Payments;
using Freshx_API.Models;
using Freshx_API.Services;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Sprache;

namespace Freshx_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IBillingService _service;
        private readonly IPdfService _pdfService;

        public PaymentController(IBillingService service , IPdfService pdfService)
        {
            _service = service;
            _pdfService = pdfService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<BillDto>>> CreateBill([FromBody] BillDto billDto)
        {
            try
            {
                var bill = await _service.CreateBillAsync(billDto);
                return StatusCode(StatusCodes.Status201Created,
                   ResponseFactory.Success(Request.Path, CreateBill, "Tạo hóa đơn thành công.", StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra.", StatusCodes.Status500InternalServerError));

            }
        }

        [HttpGet("{billId}")]
        public async Task<ActionResult<ApiResponse<BillDto>>> GetBillById(int billId)
        {
            var bill = await _service.GetBillByIdAsync(billId);
            if (bill == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<DrugCatalogDetailDto>(Request.Path, "Không tìm thấy hóa đơn.", StatusCodes.Status404NotFound));

            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, bill);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBills()
        {
            var bills = await _service.GetAllBillsAsync();
            if (bills == null)
            {
            return StatusCode(StatusCodes.Status404NotFound,
            ResponseFactory.Error<BillDto>(Request.Path, "Không tìm thấy hóa đơn.", StatusCodes.Status404NotFound));
            }
            return StatusCode(StatusCodes.Status200OK, bills);
        }

        [HttpPost("payment")]
        public async Task<ActionResult<ApiResponse<BillDto>>> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            try
            {
                var bill = await _service.GetBillByIdAsync(paymentDto.BillId.Value);

                if (bill == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseFactory.Error<string>(Request.Path, "Hóa đơn không tồn tại.", StatusCodes.Status404NotFound));
                }
                if (bill.PaymentStatus == "Paid")
                {
                    return StatusCode(StatusCodes.Status200OK,
                        ResponseFactory.Error<string>(Request.Path, "Hóa đơn đã được thanh toán đầy đủ.", StatusCodes.Status200OK));
                }

                // Nếu hóa đơn chưa thanh toán đầy đủ, tiếp tục xử lý thanh toán
                var payment = await _service.ProcessPaymentAsync(paymentDto);

                return StatusCode(StatusCodes.Status200OK,
                    ResponseFactory.Success(Request.Path, payment, "Thanh toán thành công.", StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseFactory.Error<string>(Request.Path, "Một lỗi đã xảy ra: " + ex.Message, StatusCodes.Status500InternalServerError));
            }
        }
        [HttpPut("{billId}")]
        public async Task<ActionResult<ApiResponse<BillDto>>> UpdateBillAsync(int billId, [FromBody] BillDtoUpdate billDtoUpdate)
        {
            if (billDtoUpdate == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseFactory.Error<BillDetailDtoUpdate>(Request.Path, "Không tìm thấy hóa đơn.", StatusCodes.Status404NotFound));
            }

            var updatedBill = await _service.UpdateBillAsync(billId, billDtoUpdate);
            if (updatedBill == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseFactory.Error<BillDtoUpdate>(Request.Path, "Không tìm thấy hóa đơn.", StatusCodes.Status404NotFound));
            }

            return StatusCode(StatusCodes.Status200OK, billDtoUpdate);
        }

        [HttpDelete("{billId}")]
        public async Task<ActionResult<ApiResponse<BillDto>>> DeleteBillAsync(int billId)
        {
            var isDeleted = await _service.DeleteBillAsync(billId);
            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                ResponseFactory.Error<BillDetailDto>(Request.Path, "Không tìm thấy hóa đơn.", StatusCodes.Status404NotFound));
            }

            return StatusCode(StatusCodes.Status200OK,
               ResponseFactory.Success(Request.Path, isDeleted, "Xóa hóa đơn thành công.", StatusCodes.Status200OK));
        }
        [HttpGet("print/{billId}")]
        public async Task<ActionResult> GetBillForPrinting(int billId)
        {
            var pdfBytes = await _pdfService.GenerateBillPdfAsync(billId);
            if (pdfBytes == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseFactory.Error<string>(Request.Path, "Không tìm thấy hóa đơn.", StatusCodes.Status404NotFound));
            }

            return File(pdfBytes, "application/pdf", $"Invoice_{billId}.pdf");
        }

    }

}
