using EBazar.Data;
using EBazar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EBazar.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CartController(AppDbContext context,UserManager<AppUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        
        }

        public IActionResult Index(int id)
        {
            var prod=_context.Products.FirstOrDefault(m => m.Id == id);
            if(prod != null)
            {

            return View(prod);
            }
            return RedirectToAction("Index","Product");
        }



        //Add to Cart


        public async Task<IActionResult> AddToCart(int prodId, int quantity)
        {
            //Getting the product
            var prod=_context.Products.FirstOrDefault(p => p.Id == prodId);

            //await _context.SaveChangesAsync();
            //Calculating Total
            if (prod == null)
            {
               return RedirectToAction("Index", "Home");
            }
            var total = prod.Price * quantity;
            //Getting the AppUser object
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Cart)
            .ThenInclude(c => c.CartItems).ThenInclude(b=>b.Product)
            .FirstOrDefaultAsync(u => u.Id == userId);


           // var existingCartItem = await appUser.Cart.CartItems.Product;
            bool isInCart = appUser.Cart.CartItems.Any(item => item.Product.Id == prodId);
            var cartItemExists = appUser.Cart.CartItems.FirstOrDefault(item => item.Product.Id == prodId);
            if (cartItemExists != null)
            {
                cartItemExists.Quantity += quantity;
            }
            else
            {
            var cartItem = new CartItem
            {
                Product=prod,
                Quantity=quantity,

            };
            //Updating User Cart
            appUser.Cart.CartItems.Add(cartItem);
            

            }
            appUser.Cart.total += total;
            await _userManager.UpdateAsync(appUser);
            

            return RedirectToAction("Cart");
        }


        //Remove from Cart



        public async Task<IActionResult> RemoveCartItemAsync(int cartItemId)
        {
           var cartItem= _context.CartItems.FirstOrDefault(m => m.Id == cartItemId);

            if (cartItem == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
     .Include(u => u.Cart)
         .ThenInclude(c => c.CartItems).ThenInclude(b=>b.Product)
     .FirstOrDefaultAsync(u => u.Id == userId);
          
                    appUser.Cart.CartItems.Remove(cartItem);
              
           
            await _userManager.UpdateAsync(appUser);
            return RedirectToAction("Cart");
        }



        // Render Cart

        public async Task<IActionResult> Cart()
        {
            //Getting the AppUser object
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var appUser = await _userManager.Users
                .Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems).ThenInclude(b=>b.Product)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (appUser != null)
            {
                return View(appUser);
            }
            return View("LoginToAdd");

        }
    }
}
