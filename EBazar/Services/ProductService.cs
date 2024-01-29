using EBazar.Data;
using EBazar.Models;
using Microsoft.EntityFrameworkCore;

namespace EBazar.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
           await _context.Products.AddAsync(product);
         await   _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
          var prods=  _context.Products.ToList();

            return prods;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var prod = _context.Products.FirstOrDefault(m => m.Id == productId);
            return prod;
        }
        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            var matchingProducts = _context.Products
                    .Where(p => p.Name.ToUpper().Contains(name.ToUpper()))
                    .ToList();
            return matchingProducts;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
