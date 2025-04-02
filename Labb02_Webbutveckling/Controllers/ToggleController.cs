namespace Labb02_Webbutveckling.Controllers
{
    using Labb02_Webbutveckling.Model;
    using Microsoft.AspNetCore.Mvc;
    using Labb02_Webbutveckling.Repository;

    [ApiController]
    [Route("api/toggle")]
    public class ToggleController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ToggleController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPut]
        public async Task<IActionResult> ToggleProduct([FromBody] Product productToToggle)
        {
            if(productToToggle == null) return BadRequest();

            var product = await _productRepository.GetProductByIdAsync(productToToggle.ProductId);
            if(product == null) return NotFound();

            await _productRepository.ToggleProductAvailabilityAsync(productToToggle.ProductId);

            return Ok(product);
        }
    }
}
