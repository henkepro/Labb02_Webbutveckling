using Labb02_Webbutveckling.Model;
using Labb02_Webbutveckling.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Labb02_Webbutveckling.Controllers
{
    [ApiController]
    [Route("api/update")]
    public class UpdateController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public UpdateController(ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        [HttpPut("customer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            if (updatedCustomer == null) return BadRequest();

            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Email = updatedCustomer.Email;
            customer.PhoneNumber = updatedCustomer.PhoneNumber;
            customer.Password = updatedCustomer.Password;
            customer.Adress = updatedCustomer.Adress;

            await _customerRepository.UpdateCustomerAsync(customer);
            return Ok(customer);
        }

        [HttpPut("product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;
            product.Description = updatedProduct.Description;
            product.Category = updatedProduct.Category;

            await _productRepository.UpdateProductAsync(product);
            return Ok(product);
        }
    }
}
