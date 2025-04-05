using ASM_NhomSugar_SD19311.Model;
using Blazored.LocalStorage;

namespace ASM_NhomSugar_SD19311.Service
{
    public class ShoppingCartService
    {
        private readonly ILocalStorageService _localStorage;

        public ShoppingCartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<Products>> GetCartItemsAsync()
        {
            var cartItems = await _localStorage.GetItemAsync<List<Products>>("cartItems");
            return cartItems ?? new List<Products>();
        }

        public async Task AddToCartAsync(Products product)
        {
            var cartItems = await GetCartItemsAsync();
            cartItems.Add(product);
            await _localStorage.SetItemAsync("cartItems", cartItems);
        }

        public async Task RemoveFromCartAsync(int productId)
        {
            var cartItems = await GetCartItemsAsync();
            var itemToRemove = cartItems.FirstOrDefault(p => p.Id == productId);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                await _localStorage.SetItemAsync("cartItems", cartItems);
            }
        }

        public async Task<int> GetCartItemCountAsync()
        {
            var cartItems = await GetCartItemsAsync();
            return cartItems.Count;
        }
    }
}
