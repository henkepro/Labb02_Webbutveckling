namespace Labb02_Webbutveckling.Controllers;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/admin/products")]
public class AdminProductsController : ControllerBase
{
    private readonly MyContext _dbContext;

    public AdminProductsController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _dbContext.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if(product == null) return NotFound();

        return Ok(product);
    }

    [HttpPut("toggle")]
    public async Task<IActionResult> ToggleProduct([FromBody] Product productToToggle)
    {
        var product = await _dbContext.Products.FindAsync(productToToggle.ProductId);
        if(product == null) return NotFound();

        product.IsAvailable = !product.IsAvailable;
        await _dbContext.SaveChangesAsync();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] Product newProduct)
    {
        if(newProduct == null) return BadRequest();

        _dbContext.Products.Add(newProduct);
        await _dbContext.SaveChangesAsync();

        return Ok(newProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if(product == null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Quantity = updatedProduct.Quantity;
        product.Description = updatedProduct.Description;
        product.Category = updatedProduct.Category;

        await _dbContext.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if(product == null) return NotFound();

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

        return Ok(product);
    }
}
