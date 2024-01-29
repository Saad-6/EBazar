using EBazar.Models;

namespace EBazar.Services
{
    public interface IStoreService
    {
        Task AddProductToStoreAsync( Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> GetAllProductsInStoreAsync();
    }
}
