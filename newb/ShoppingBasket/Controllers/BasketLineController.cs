using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Models;
using ShoppingBasket.Service;

namespace ShoppingBasket.Controllers
{

    [Route("api/shoppingbasket/{userId}/basketlines")]
    [ApiController]
    public class BasketLinesController : ControllerBase
    {
        private readonly IShoppingBasketService _shoppingBasketService;

        public BasketLinesController(IShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
        }

        [HttpPost]
        public async Task<ActionResult> AddProductToBasket(Guid userId, [FromBody] BasketLineForCreation basketLineForCreation)
        {
            await _shoppingBasketService.AddProductToBasket(userId, basketLineForCreation);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> RemoveProductFromBasket(Guid userId, Guid productId)
        {
            await _shoppingBasketService.RemoveProductFromBasket(userId, productId);
            return NoContent();
        }
    }

    [Route("api/shoppingbasket")]
    [ApiController]
    public class ShoppingBasketController : ControllerBase
    {
        private readonly IShoppingBasketService _shoppingBasketService;

        public ShoppingBasketController(IShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ShoppingBasketDto>> GetShoppingBasket(Guid userId)
        {
            var basket = await _shoppingBasketService.GetShoppingBasketByUserId(userId);
            return Ok(basket);
        }
    }
}
