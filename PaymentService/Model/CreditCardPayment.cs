using System.ComponentModel.DataAnnotations;

namespace PaymentService.Model;

public class CreditCardPayment
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string CardNumber { get; set; }
    public string CVC { get; set; }
    public string CardholderName { get; set; }
    public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
}
