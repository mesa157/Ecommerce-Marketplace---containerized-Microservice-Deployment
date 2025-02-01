using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnifiedFrontend.Models.PaymentModel;

namespace UnifiedFrontend.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PaymentController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(Guid userId, decimal totalAmount)
        {
            var model = new PaymentViewModel
            {
                UserId = userId,
                Amount = totalAmount
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid payment details. Please check and try again.";
                return View("Index", model);
            }

            var apiUrl = _configuration["BackendServices3:PaymentService"];
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.PostAsJsonAsync($"{apiUrl}/api/payments", model);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Payment failed for UserId: {UserId}. StatusCode: {StatusCode}", model.UserId, response.StatusCode);
                    TempData["ErrorMessage"] = "Payment failed. Please try again.";
                    return View("Index", model);
                }

                var cartServiceUrl = _configuration["BackendServices2:CartService"];
                var cartServiceClient = _httpClientFactory.CreateClient();
                var clearResponse = await client.PostAsync($"{cartServiceUrl}/api/shoppingbasket/clear/{model.UserId}", null);

                TempData["SuccessMessage"] = "Payment successful! Thank you for your order.";
                return RedirectToAction("OrderConfirmation", new { userId = model.UserId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment for UserId: {UserId}", model.UserId);
                TempData["ErrorMessage"] = "An error occurred during payment.";
                return View("Index", model);
            }
        }

        public IActionResult OrderConfirmation(Guid userId)
        {
            ViewBag.UserId = userId;
            return View();
        }
    }
}
