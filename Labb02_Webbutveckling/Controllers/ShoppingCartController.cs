namespace Labb02_Webbutveckling.Controllers;

using Labb02_Webbutveckling.DataModels;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/shoppingcart")]
public class ShoppingCartController : ControllerBase
{
    private readonly MyContext _dbContext;

    public ShoppingCartController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] ShoppingCartProductModel cartItem)
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
        return Ok();
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateCart([FromBody] ShoppingCart cart)
    {
        _dbContext.ShoppingCarts.Add(cart);
        await _dbContext.SaveChangesAsync();

        return Ok(cart);
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCart(int customerId)
    {
        var shoppingCart = await _dbContext.ShoppingCarts
            .Include(sc => sc.ShoppingCartProducts)
            .ThenInclude(sp => sp.Product)
            .FirstOrDefaultAsync(sc => sc.CustomerId == customerId);

        if(shoppingCart == null)
        {
            shoppingCart = new ShoppingCart
            {
                CustomerId = customerId,
                ShoppingCartProducts = new List<ShoppingCartProduct>()
            };

            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();
        }

        return Ok(shoppingCart);
    }

    [HttpDelete("remove/{cartId}/{productId}")]
    public async Task<IActionResult> RemoveFromCart(int cartId, int productId)
    {
        var cartItem = await _dbContext.ShoppingCartProducts
            .FirstOrDefaultAsync(cp => cp.ShoppingCartId == cartId && cp.ProductId == productId);

        _dbContext.ShoppingCartProducts.Remove(cartItem);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

}
