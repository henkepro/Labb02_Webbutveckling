using Labb02_Webbutveckling.DataModels;
using Labb02_Webbutveckling.Model;
using Labb02_Webbutveckling.Repository;
using Microsoft.EntityFrameworkCore;

namespace Labb02_Webbutveckling.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly MyContext _dbContext;

        public ShoppingCartRepository(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShoppingCartProduct> GetCartItemAsync(int cartId, int productId)
        {
            return await _dbContext.ShoppingCartProducts
                .FirstOrDefaultAsync(cp => cp.ShoppingCartId == cartId && cp.ProductId == productId);
        }

        public async Task RemoveCartItemAsync(ShoppingCartProduct cartItem)
        {
            _dbContext.ShoppingCartProducts.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<ShoppingCart> GetShoppingCartByCustomerIdAsync(int customerId)
        {
            return await _dbContext.ShoppingCarts
                .Include(sc => sc.ShoppingCartProducts)
                .ThenInclude(sp => sp.Product)
                .FirstOrDefaultAsync(sc => sc.CustomerId == customerId);
        }

        public async Task CreateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddShoppingCartAsync(ShoppingCart cart)
        {
            _dbContext.ShoppingCarts.Add(cart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductToCartAsync(ShoppingCartProductModel cartItem)
        {
            var shoppingCart = await _dbContext.ShoppingCarts
                .Include(sc => sc.ShoppingCartProducts)
                .ThenInclude(sp => sp.Product)
                .FirstOrDefaultAsync(sc => sc.Id == cartItem.ShoppingCartId);

            var existingProduct = shoppingCart.ShoppingCartProducts
                .FirstOrDefault(sp => sp.ProductId == cartItem.ProductId);

            if(existingProduct != null)
            {
                existingProduct.Quantity++;
            }
            else
            {
                var product = shoppingCart.ShoppingCartProducts
                    .Select(sp => sp.Product)
                    .FirstOrDefault(p => p.ProductId == cartItem.ProductId)
                    ?? await _dbContext.Products.FindAsync(cartItem.ProductId);

                shoppingCart.ShoppingCartProducts.Add(new ShoppingCartProduct
                {
                    ProductId = product.ProductId,
                    ShoppingCartId = shoppingCart.Id,
                    Quantity = 1
                });
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
