using EBazar.Data;
using EBazar.Models;
using EBazar.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EBazar.Controllers
{
    public class StoreController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductService _service;
        private readonly AppDbContext _context;
        public StoreController(IProductService service, UserManager<AppUser> userManager,AppDbContext context)
        {
            _service = service;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Store).ThenInclude(u=>u.Products)
            .FirstOrDefaultAsync(u => u.Id == userId);


            return View(appUser);
        }
        public async Task<IActionResult> OpenStoreAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Store)
            .FirstOrDefaultAsync(u => u.Id == userId);
            if (appUser.Store != null)
            {
                return RedirectToAction("Index");
            }

            return View(appUser);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStoreAsync(AppUser user)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Store)
            .FirstOrDefaultAsync(u => u.Id == userId);
            if (user.Store== null)
            {

            return RedirectToAction("Index","Account");
            }
            
            
            appUser.Store = new Store();
            appUser.Store.Name = user.Store.Name;
            appUser.Store.Description = user.Store.Description;
            
            await _userManager.UpdateAsync(appUser);

            return RedirectToAction(nameof(Index));
            

        }

        public async Task<IActionResult> AddProduct()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Store)
            .FirstOrDefaultAsync(u => u.Id == userId);
            if (appUser.Store == null || appUser==null)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Store)
            .FirstOrDefaultAsync(u => u.Id == userId);
            appUser.Store.Products.Add(product);
            await _userManager.UpdateAsync(appUser);



            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var prod= await _service.GetProductByIdAsync(id);
            await _service.DeleteProductAsync(prod);

            return RedirectToAction("Index", "Store");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var prod = await _service.GetProductByIdAsync(id);
            

            return View(prod);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Store");
            }
            await _service.UpdateProductAsync(product);



            return RedirectToAction("Index", "Store");
        }
        public async Task<IActionResult> ManageOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Store)
            .FirstOrDefaultAsync(u => u.Id == userId);
            if(appUser == null || appUser.Store == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var storeOrders = _context.Orders
        .Include(m=>m.Address)
        .Include(o => o.CartItems)
        .ThenInclude(ci => ci.Product)
      //  .Where( o.StoreId == appUser.Store.Id)
        .ToList();
            return View();
        }
        public IActionResult ByStore()
        {
            var stores = _context.Stores.Include(m=>m.Products).ToList();
            return View(stores);
        }
        public IActionResult StoreDetail(int id)
        {
           var store= _context.Stores.Include(m => m.Products).FirstOrDefault(m => m.Id == id);

            return View(store);
        }
    }
}
