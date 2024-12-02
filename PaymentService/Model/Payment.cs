using System;

namespace PaymentService.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; } // e.g., CreditCard, PayPal, etc.
        public string TransactionId { get; set; }
        public int OrderId { get; set; } // Reference to the associated order
        public string CustomerId { get; set; } // Reference to the customer making the payment
    }
}