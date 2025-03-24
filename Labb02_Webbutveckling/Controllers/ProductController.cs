namespace Labb02_Webbutveckling.Controllers;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly MyContext _dbContext;

    public ProductController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string query)
    {
        if(string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty.");
        }

        var products = await _dbContext.Products
        .Where(c => c.Name.ToLower().Contains(query.ToLower()))
        .ToListAsync();

        if(products.Count == 0)
        {
            return Ok(new List<Product>());
        }

        return Ok(products);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _dbContext.Products.ToListAsync();
        return Ok(products);
    }
}