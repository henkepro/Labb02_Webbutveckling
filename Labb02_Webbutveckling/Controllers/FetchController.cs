namespace Labb02_Webbutveckling.Controllers;

using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/fetch")]
public class FetchController : ControllerBase
{
    private readonly MyContext _dbContext;

    public FetchController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _dbContext.Products.ToListAsync());
    }
    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok(await _dbContext.Customers.ToListAsync());
    }
    [HttpGet("shoppingcart/{customerId}")]
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
}
