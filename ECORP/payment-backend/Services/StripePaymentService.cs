using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentService.Data;
using PaymentService.Model;
using PaymentService.Services;
using Stripe;
using System;
using System.Threading.Tasks;

namespace PaymentService.Services
{
    public class StripePaymentService : IPaymentService
    {
        private readonly ILogger<StripePaymentService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly StripeSettings _stripeSettings;

        public StripePaymentService(ILogger<StripePaymentService> logger, IConfiguration configuration, ApplicationDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;

            _stripeSettings = new StripeSettings
            {
                SecretKey = _configuration["Stripe:SecretKey"],
                PublishableKey = _configuration["Stripe:PublishableKey"]
            };

            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public async Task<Payment> ProcessPayment(Payment payment)
        {
            try
            {
                if (payment.Amount <= 0)
                    throw new ArgumentException("Amount must be greater than zero.");

                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = (long)(payment.Amount * 100),
                    Currency = payment.Currency,
                    Description = $"Payment for Order ID: {payment.Id}",
                    Source = payment.PaymentMethodToken
                };

                var chargeService = new ChargeService();
                Charge charge = await chargeService.CreateAsync(chargeOptions);

                payment.Status = charge.Status;
                payment.TransactionId = charge.Id;

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return payment;
            }
            catch (StripeException ex)
            {
                _logger.LogError($"Stripe error occurred: {ex.Message}");
                payment.Status = "Failed";
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return payment;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing payment: {ex.Message}");
                payment.Status = "Failed";
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return payment;
            }
        }

        public async Task<Payment> UpdatePaymentStatus(int id, string status)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                throw new InvalidOperationException("Payment not found.");
            }

            payment.Status = status;

            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                throw new InvalidOperationException("Payment not found.");
            }

            return payment;
        }
    }
}