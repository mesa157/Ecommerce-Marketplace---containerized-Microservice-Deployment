
using System.Collections.Generic;
using System.Threading.Tasks;
using CartService.Model;

public interface ICartRepository
{
    Task<IEnumerable<Cart>> GetAllCartsAsync();
    Task<Cart> GetCartByIdAsync(int id);
    Task AddCartAsync(Cart cart);
    Task UpdateCartAsync(Cart cart);
    Task DeleteCartAsync(int id);
    Task AddCartItemAsync(CartItem cartItem);
    Task RemoveCartItemAsync(int cartItemId);
}