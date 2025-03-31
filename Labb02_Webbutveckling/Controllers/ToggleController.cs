namespace Labb02_Webbutveckling.Controllers;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/toggle")]
public class ToggleController : ControllerBase
{
    private readonly MyContext _dbContext;

    public ToggleController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut]
    public async Task<IActionResult> ToggleProduct([FromBody] Product productToToggle)
    {
        var product = await _dbContext.Products.FindAsync(productToToggle.ProductId);
        if(product == null) return NotFound();

        product.IsAvailable = !product.IsAvailable;
        await _dbContext.SaveChangesAsync();

        return Ok(product);
    }
}
