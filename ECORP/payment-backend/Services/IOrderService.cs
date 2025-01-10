namespace PaymentService.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderDetails(int orderId);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<bool> CreateOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int orderId);
    }
}