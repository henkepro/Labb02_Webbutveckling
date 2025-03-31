namespace Labb02_Webbutveckling.Controllers;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/remove")]
public class RemoveController : ControllerBase
{
    private readonly MyContext _dbContext;

    public RemoveController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpDelete("product/{id}")]
    public async Task<IActionResult> RemoveProduct(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if(product == null) return NotFound();

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("customer/{id}")]
    public async Task<IActionResult> RemoveCustomer(int id)
    {
        var customer = await _dbContext.Customers.FindAsync(id);
        if(customer == null) return NotFound();

        _dbContext.Customers.Remove(customer);
        await _dbContext.SaveChangesAsync();

        return Ok(customer);
    }
    [HttpDelete("shoppingcart/{cartId}/{productId}")]
    public async Task<IActionResult> RemoveFromCart(int cartId, int productId)
    {
        var cartItem = await _dbContext.ShoppingCartProducts
            .FirstOrDefaultAsync(cp => cp.ShoppingCartId == cartId && cp.ProductId == productId);

        _dbContext.ShoppingCartProducts.Remove(cartItem);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}