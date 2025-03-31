namespace Labb02_Webbutveckling.Controllers;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/update")]
public class UpdateController : ControllerBase
{
    private readonly MyContext _dbContext;

    public UpdateController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpPut("customer/{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
    {
        if(updatedCustomer == null) return BadRequest();

        var customer = await _dbContext.Customers.FindAsync(id);

        customer.FirstName = updatedCustomer.FirstName;
        customer.LastName = updatedCustomer.LastName;
        customer.Email = updatedCustomer.Email;
        customer.PhoneNumber = updatedCustomer.PhoneNumber;
        customer.Password = updatedCustomer.Password;

        await _dbContext.SaveChangesAsync();
        return Ok(customer);
    }

    [HttpPut("product/{id}")]
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
}
