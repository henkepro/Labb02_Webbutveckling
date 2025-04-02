namespace Labb02_Webbutveckling.Controllers
{
    using Labb02_Webbutveckling.DataModels;
    using Labb02_Webbutveckling.Model;
    using Labb02_Webbutveckling.Repository;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/add")]
    public class AddController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public AddController(IProductRepository productRepository, ICustomerRepository customerRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpPost("product")]
        public async Task<IActionResult> AddProduct([FromBody] Product newProduct)
        {
            if(newProduct == null) return BadRequest();

            await _productRepository.AddProductAsync(newProduct);
            return Ok(newProduct);
        }

        [HttpPost("customer")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if(customer == null) return BadRequest();

            await _customerRepository.AddCustomerAsync(customer);
            return Ok();
        }

        [HttpPost("shoppingcart")]
        public async Task<IActionResult> AddToCart([FromBody] ShoppingCartProductModel cartItem)
        {
            await _shoppingCartRepository.AddProductToCartAsync(cartItem);
            return Ok();
        }

        [HttpPost("shoppingcart/create")]
        public async Task<IActionResult> AddCart([FromBody] ShoppingCart cart)
        {
            await _shoppingCartRepository.AddShoppingCartAsync(cart);
            return Ok(cart);
        }
    }
}
