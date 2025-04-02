namespace Labb02_Webbutveckling.Controllers
{
    using Labb02_Webbutveckling.Model;
    using Microsoft.AspNetCore.Mvc;
    using Labb02_Webbutveckling.Repository;

    [ApiController]
    [Route("api/fetch")]
    public class FetchController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public FetchController(IProductRepository productRepository, ICustomerRepository customerRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("shoppingcart/{customerId}")]
        public async Task<IActionResult> GetCart(int customerId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerIdAsync(customerId);

            if(shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    CustomerId = customerId,
                    ShoppingCartProducts = new List<ShoppingCartProduct>()
                };

                await _shoppingCartRepository.CreateShoppingCartAsync(shoppingCart);
            }

            return Ok(shoppingCart);
        }
    }
}
