using EBazar.Models;

namespace EBazar.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> GetProductByNameAsync(string name);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
