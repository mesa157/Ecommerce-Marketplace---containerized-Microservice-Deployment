using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderDetails(int orderId);
    }

    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Order> GetOrderDetails(int orderId)
        {
            var client = _httpClientFactory.CreateClient("ShippingBasketService");
            var response = await client.GetAsync($"/api/orders/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadAsAsync<Order>();
                return order;
            }

            throw new InvalidOperationException("Order not found.");
        }
    }
}