namespace Labb02_Webbutveckling.Controllers;

using Labb02_Webbutveckling.DataModels;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/add")]
public class AddController : ControllerBase
{
    private readonly MyContext _dbContext;

    public AddController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("product")]
    public async Task<IActionResult> AddProduct([FromBody] Product newProduct)
    {
        if(newProduct == null) return BadRequest();

        _dbContext.Products.Add(newProduct);
        await _dbContext.SaveChangesAsync();

        return Ok(newProduct);
    }
    [HttpPost("customer")]
    public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
    {
        if(customer == null) return BadRequest();

        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("shoppingcart")]
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

    [HttpPost("shoppingcart/create")]
    public async Task<IActionResult> AddCart([FromBody] ShoppingCart cart)
    {
        _dbContext.ShoppingCarts.Add(cart);
        await _dbContext.SaveChangesAsync();

        return Ok(cart);
    }
}
