using System.Threading.Tasks;
using PaymentService.Model;

namespace PaymentService.Services
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPayment(Payment payment);
    }
}
