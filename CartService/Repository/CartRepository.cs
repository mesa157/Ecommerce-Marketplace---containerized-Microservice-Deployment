using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CartService.Data;
using CartService.Model;

public class CartRepository : ICartRepository
{
    private readonly CartContext _context;

    public CartRepository(CartContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cart>> GetAllCartsAsync()
    {
        return await _context.Carts.Include(c => c.Items).ToListAsync();
    }

    public async Task<Cart> GetCartByIdAsync(int id)
    {
        return await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddCartAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCartAsync(int id)
    {
        var cart = await _context.Carts.FindAsync(id);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddCartItemAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveCartItemAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}