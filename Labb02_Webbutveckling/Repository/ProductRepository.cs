using Labb02_Webbutveckling.Model;
using Microsoft.EntityFrameworkCore;

namespace Labb02_Webbutveckling.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _dbContext;

        public ProductRepository(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> SearchProductsByNameAsync(string productName)
        {
            return await _dbContext.Products
                .Where(p => p.Name.ToLower().Contains(productName.ToLower()))
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task ToggleProductAvailabilityAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if(product != null)
            {
                product.IsAvailable = !product.IsAvailable;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task AddProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
