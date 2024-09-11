using Medical_CRM_Domain.DTOs.PaymentDTOs;
using Medical_CRM_Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CRM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/Payment
        [HttpGet("All")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        // GET: api/Payment/{id}
        [HttpGet("Find/{id}")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        // POST: api/Payment
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _paymentService.CreatePaymentAsync(paymentDto);
            return Ok(new { Message = "Payment created successfully" });
        }

        // PUT: api/Payment/{id}
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] PaymentUpdateDto paymentDto)
        {
            if (id != paymentDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _paymentService.UpdatePaymentAsync(paymentDto);
            return NoContent();
        }

        // DELETE: api/Payment/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            await _paymentService.DeletePaymentAsync(id);
            return NoContent();
        }
    }
}
