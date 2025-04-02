using Labb02_Webbutveckling.Model;

namespace Labb02_Webbutveckling.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> SearchProductsByNameAsync(string productName);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        Task ToggleProductAvailabilityAsync(int productId);
        Task RemoveProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
    }
}
