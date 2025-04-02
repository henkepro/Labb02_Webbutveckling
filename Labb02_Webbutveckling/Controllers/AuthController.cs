namespace Labb02_Webbutveckling.Controllers
{
    using Labb02_Webbutveckling.Model;
    using Microsoft.AspNetCore.Mvc;
    using Labb02_Webbutveckling.Repository;

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutCustomer([FromBody] Customer loggedinCustomer)
        {
            var existingCustomer = await _authRepository.GetCustomerByIdAsync(loggedinCustomer.Id);
            if(existingCustomer != null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Customer loginRequest)
        {
            var customer = await _authRepository.GetCustomerByEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);

            if(customer == null)
            {
                return Unauthorized();
            }

            return Ok(customer);
        }
    }
}
