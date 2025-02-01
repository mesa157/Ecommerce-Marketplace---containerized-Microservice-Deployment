using System;
using System.ComponentModel.DataAnnotations;

namespace UnifiedFrontend.Models.PaymentModel
{
    public class PaymentViewModel
    {
        public Guid UserId { get; set; }

        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVC must be 3 digits.")]
        public string CVC { get; set; }

        [Required]
        public string CardholderName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

        public decimal Amount { get; set; }
    }
}