using System.Net.Http.Json;
using UnifiedFrontend.Models.CartModel;

namespace UnifiedFrontend.Services.CartService
{
    public class CartServiceApi
    {
        private readonly HttpClient _httpClient;

        public CartServiceApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShoppingBasket> GetBasketAsync(Guid userId)
        {
            return await _httpClient.GetFromJsonAsync<ShoppingBasket>($"api/shoppingbasket/{userId}");
        }

        public async Task<bool> AddProductToBasketAsync(Guid userId, BasketLine basketLine)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/shoppingbasket/{userId}/basketlines", basketLine);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQuantityAsync(Guid userId, Guid productId, int quantity)
        {
            var payload = new { Quantity = quantity };
            var response = await _httpClient.PutAsJsonAsync($"api/shoppingbasket/{userId}/basketlines/{productId}", payload);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveProductFromBasketAsync(Guid userId, Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"api/shoppingbasket/{userId}/basketlines/{productId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CheckoutAsync(Guid userId)
        {
            var response = await _httpClient.PostAsync($"api/shoppingbasket/{userId}/checkout", null);
            return response.IsSuccessStatusCode;
        }
    }
}
