namespace Labb02_Webbutveckling.Controllers;
using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly MyContext _dbContext;

    public CustomerController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Customer customer)
    {
        if(customer == null)
            return BadRequest("Invalid customer data");

        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();

        return Ok("Customer registered successfully!");
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
            return NotFound(new { message = "Customer not found" });

        return Ok(customer);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCustomersByEmail([FromQuery] string email)
    {
        if(string.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Email query cannot be empty" });

        var customers = await _dbContext.Customers
            .Where(c => c.Email.ToLower().Contains(email.ToLower()))
            .ToListAsync();

        return Ok(customers);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] Customer updatedUser)
    {
        var user = await _dbContext.Customers.FindAsync(id);
        if(user == null)
            return NotFound();

        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.PhoneNumber = updatedUser.PhoneNumber;
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;
        user.Adress = updatedUser.Adress;

        await _dbContext.SaveChangesAsync();
        return Ok(user);
    }
}
