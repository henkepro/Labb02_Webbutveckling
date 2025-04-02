namespace Labb02_Webbutveckling.Controllers
{
    using Labb02_Webbutveckling.Model;
    using Microsoft.AspNetCore.Mvc;
    using Labb02_Webbutveckling.Repository;

    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public SearchController(IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        [HttpGet("product")]
        public async Task<IActionResult> SearchProducts([FromQuery] string product)
        {
            if(string.IsNullOrWhiteSpace(product))
            {
                return BadRequest("Search query cannot be empty.");
            }

            var products = await _productRepository.SearchProductsByNameAsync(product);

            if(products.Count == 0)
            {
                return Ok(new List<Product>());
            }

            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> SearchProductById(int productId)
        {
            if(productId <= 0)
            {
                return BadRequest();
            }

            var product = await _productRepository.GetProductByIdAsync(productId);

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
                return BadRequest("Email query cannot be empty");

            var customers = await _customerRepository.SearchCustomersByEmailAsync(email);

            return Ok(customers);
        }
    }
}
