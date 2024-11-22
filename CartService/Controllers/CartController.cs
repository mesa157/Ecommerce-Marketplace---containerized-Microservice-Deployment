using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CartService.Model;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    // GET: api/Cart
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
    {
        var carts = await _cartRepository.GetAllCartsAsync();
        return Ok(carts);
    }

    // GET: api/Cart/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetCart(int id)
    {
        var cart = await _cartRepository.GetCartByIdAsync(id);
        if (cart == null)
        {
            return NotFound();
        }
        return Ok(cart);
    }

    // POST: api/Cart
    [HttpPost]
    public async Task<ActionResult<Cart>> AddCart(Cart cart)
    {
        await _cartRepository.AddCartAsync(cart);
        return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
    }

    // PUT: api/Cart/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCart(int id, Cart cart)
    {
        if (id != cart.Id)
        {
            return BadRequest();
        }
        await _cartRepository.UpdateCartAsync(cart);
        return NoContent();
    }

    // DELETE: api/Cart/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        await _cartRepository.DeleteCartAsync(id);
        return NoContent();
    }

    // POST: api/Cart/AddItem
    [HttpPost("AddItem")]
    public async Task<ActionResult<CartItem>> AddCartItem(CartItem cartItem)
    {
        await _cartRepository.AddCartItemAsync(cartItem);
        return Ok(cartItem);
    }

    // DELETE: api/Cart/RemoveItem/5
    [HttpDelete("RemoveItem/{cartItemId}")]
    public async Task<IActionResult> RemoveCartItem(int cartItemId)
    {
        await _cartRepository.RemoveCartItemAsync(cartItemId);
        return NoContent();
    }
}