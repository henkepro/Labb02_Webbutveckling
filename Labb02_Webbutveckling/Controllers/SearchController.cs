namespace Labb02_Webbutveckling.Controllers;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly MyContext _dbContext;

    public SearchController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet("product")]
    public async Task<IActionResult> SearchProducts([FromQuery] string product)
    {
        if(string.IsNullOrWhiteSpace(product))
        {
            return BadRequest("Search query cannot be empty.");
        }

        var products = await _dbContext.Products
        .Where(c => c.Name.ToLower().Contains(product.ToLower()))
        .ToListAsync();

        if(products.Count == 0)
        {
            return Ok(new List<Product>());
        }

        return Ok(products);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> SearchProductsById(int productId)
    {
        if(productId <= 0)
        {
            return BadRequest();
        }

        var product = await _dbContext.Products
        .FirstOrDefaultAsync(c => c.ProductId == productId);

        if(product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }


    [HttpGet("email")]
    public async Task<IActionResult> SearchCustomersByEmail([FromQuery] string email)
    {
        if(string.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Email query cannot be empty" });

        var customers = await _dbContext.Customers
        .Where(c => c.Email.ToLower().Contains(email.ToLower()))
        .ToListAsync();

        return Ok(customers);
    }
}
