using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentService.Services;
using System.Threading.Tasks;

namespace PaymentService.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                return Ok(order);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Order not found: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving order: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the order.");
            }
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order details are required.");
            }

            var result = await _orderService.CreateOrder(order);

            if (result != null)
            {
                return CreatedAtAction(nameof(GetOrder), new { id = result.Id }, result);
            }

            return StatusCode(500, "Order creation failed.");
        }
    }
}