using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Model;
using System.Threading.Tasks;

namespace OrderService.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context) { }

        public async Task<Order> GetOrderWithItemsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .SingleOrDefaultAsync(o => o.Id == id);
        }
    }
}
