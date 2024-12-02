using OrderService.Model;
using System.Threading.Tasks;

namespace OrderService.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderWithItemsAsync(int id);
    }
}
