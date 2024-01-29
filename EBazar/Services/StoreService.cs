using EBazar.Data;
using EBazar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EBazar.Services
{
    public class StoreService : IStoreService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public StoreService(AppDbContext context,UserManager<AppUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public  async Task AddProductToStoreAsync(Product product)
        {
           

           
        }

        public Task DeleteProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsInStoreAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
