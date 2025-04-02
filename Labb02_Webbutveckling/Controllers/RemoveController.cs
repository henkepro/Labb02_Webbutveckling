namespace Labb02_Webbutveckling.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Labb02_Webbutveckling.Repository;

    [ApiController]
    [Route("api/remove")]
    public class RemoveController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public RemoveController(IProductRepository productRepository, ICustomerRepository customerRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpDelete("product/{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if(product == null) return NotFound();

            await _productRepository.RemoveProductAsync(product);
            return Ok(product);
        }

        [HttpDelete("customer/{id}")]
        public async Task<IActionResult> RemoveCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if(customer == null) return NotFound();

            await _customerRepository.RemoveCustomerAsync(customer);
            return Ok(customer);
        }

        [HttpDelete("shoppingcart/{cartId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int cartId, int productId)
        {
            var cartItem = await _shoppingCartRepository.GetCartItemAsync(cartId, productId);
            if(cartItem == null) return NotFound();

            await _shoppingCartRepository.RemoveCartItemAsync(cartItem);
            return Ok();
        }
    }
}
