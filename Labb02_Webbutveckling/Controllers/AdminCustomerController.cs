namespace Labb02_Webbutveckling.Controllers;

using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/admin/customers")]
public class AdminCustomerController : ControllerBase
{
    private readonly MyContext _dbContext;

    public AdminCustomerController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok(await _dbContext.Customers.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _dbContext.Customers.FindAsync(id);
        if(customer == null)
            return NotFound();

        return Ok(customer);
    }


    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _dbContext.Customers.FindAsync(id);
        if(customer == null) return NotFound();

        _dbContext.Customers.Remove(customer);
        await _dbContext.SaveChangesAsync();

        return Ok(customer);
    }
}
