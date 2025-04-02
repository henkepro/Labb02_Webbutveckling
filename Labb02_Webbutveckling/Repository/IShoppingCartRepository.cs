using Labb02_Webbutveckling.DataModels;
using Labb02_Webbutveckling.Model;

namespace Labb02_Webbutveckling.Repository
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCartProduct> GetCartItemAsync(int cartId, int productId);
        Task RemoveCartItemAsync(ShoppingCartProduct cartItem);
        Task<ShoppingCart> GetShoppingCartByCustomerIdAsync(int customerId);
        Task CreateShoppingCartAsync(ShoppingCart shoppingCart);
        Task AddShoppingCartAsync(ShoppingCart cart);
        Task AddProductToCartAsync(ShoppingCartProductModel cartItem);
    }
}
