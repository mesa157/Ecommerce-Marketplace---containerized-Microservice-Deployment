using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentService;
using PaymentService.Data;
using PaymentService.Model;
using PaymentService.Services;
using System;
using System.Threading.Tasks;

namespace PaymentService.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IOrderService _orderService;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger, IOrderService orderService)
        {
            _paymentService = paymentService;
            _logger = logger;
            _orderService = orderService;
        }

        // POST: api/payments
        [HttpPost]
        public async Task<IActionResult> PostPayment([FromBody] Payment payment)
        {
            if (payment == null)
            {
                return BadRequest("Payment details are required.");
            }

            var result = await _paymentService.ProcessPayment(payment);

            if (result.Status == "Succeeded")
            {
                return Ok(result);
            }

            return StatusCode(500, $"Payment failed: {result.Status}");
        }

        // PUT: api/payments/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, [FromBody] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status is required.");
            }

            try
            {
                var result = await _paymentService.UpdatePaymentStatus(id, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating payment status: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the payment status.");
            }
        }

        // GET: api/payments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            try
            {
                var payment = await _paymentService.GetPaymentById(id);
                return Ok(payment);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Payment not found: {ex.Message}");
                return NotFound(ex.Message); // Return a 404 if payment is not found
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving payment: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the payment.");
            }
        }

        // GET: api/payments/order/{orderId}
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderPaymentDetails(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderById(orderId);
                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                var payment = new Payment
                {
                    Amount = order.TotalAmount,
                    Currency = "USD", // Assuming USD, adjust as necessary
                    PaymentDate = DateTime.UtcNow,
                    Method = PaymentMethod.CreditCard, // Default method, adjust as necessary
                    Status = "Pending",
                    TransactionId = string.Empty,
                    PaymentMethodToken = string.Empty // This should be set from the front-end
                };

                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving order payment details: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving order payment details.");
            }
        }
    }
}