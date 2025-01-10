namespace PaymentService.Services
{
    using System.Threading.Tasks;
    using PaymentService.Models;

    public interface IPaymentService
    {
        Task<Payment> ProcessPayment(Payment payment);
        Task<Payment> UpdatePaymentStatus(int id, string status);
        Task<Payment> GetPaymentById(int id);
    }
}