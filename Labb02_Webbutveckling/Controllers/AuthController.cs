﻿namespace Labb02_Webbutveckling.Controllers;

using Labb02_Webbutveckling.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly MyContext _dbContext;

    public AuthController(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutCustomer([FromBody] Customer loggedinCustomer)
    {
        var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == loggedinCustomer.Id);
        if(existingCustomer != null)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Customer loginRequest)
    {
        var customer = await _dbContext.Customers
        .FirstOrDefaultAsync(c => c.Email == loginRequest.Email && c.Password == loginRequest.Password);

        if(customer == null)
        {
            return Unauthorized();
        }

        return Ok(customer);
    }

}
