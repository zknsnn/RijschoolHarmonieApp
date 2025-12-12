using Microsoft.AspNetCore.Mvc;
using RijschoolHarmonieApp.DTOs.Payment;
using RijschoolHarmonieApp.Services;

namespace RijschoolHarmonieApp.Controllers
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

        // GET: api/payment
        [HttpGet]
        public async Task<ActionResult<List<PaymentResponseDto>>> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();
            return Ok(payments);
        }

        // GET: api/payment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentResponseDto>> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null)
                return NotFound("Payment not found.");

            return Ok(payment);
        }

        // GET: api/payment/student/{id}
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<List<PaymentResponseDto>>> GetByStudentId(int studentId)
        {
            var payments = await _paymentService.GetByStudentIdAsync(studentId);

            if (payments == null || !payments.Any())
                return NotFound("No payments found for this student.");

            return Ok(payments);
        }

        // POST: api/payment
        [HttpPost]
        public async Task<ActionResult<PaymentResponseDto>> Create(CreatePaymentDto dto)
        {
            try
            {
                var createdPayment = await _paymentService.AddPaymentAsync(dto);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdPayment.PaymentId },
                    createdPayment
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/payment/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _paymentService.DeleteAsync(id);
            if (!deleted)
                return NotFound("Payment not found.");

            return NoContent();
        }
    }
}
