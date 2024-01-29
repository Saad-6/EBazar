using EBazar.Data;
using EBazar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EBazar.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user=_context.Users.Include(m=>m.Address).FirstOrDefault(m=>m.Id==userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var userOrders = _context.Orders
        .Include(o => o.Address)
        .Include(o => o.CartItems)
        .Where(o => o.UserId == userId)
        .ToList();


            return View(user);
        }
        public IActionResult EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Include(m => m.Address).FirstOrDefault(m => m.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
          


            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfileAsync(AppUser user)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Cart)
            .ThenInclude(c => c.CartItems).ThenInclude(b => b.Product)
            .FirstOrDefaultAsync(u => u.Id == userId);
           // var appUser = await _userManager.FindByIdAsync(user.Id);
            if(ModelState.IsValid && appUser!=null)
            {
                if (user.AppUserName != null)
                {
                    appUser.AppUserName = user.AppUserName;
                }
                if (user.UserName != null)
                {
                    appUser.UserName = user.UserName;
                }
                if (user.ProfilePictureUrl != null)
                {
                    appUser.ProfilePictureUrl=user.ProfilePictureUrl;
                }
                if (user.Address != null)
                {
                    appUser.Address = user.Address;

                }
                var result = await _userManager.UpdateAsync(appUser);

                if (result.Succeeded)
                {
                    
                    return RedirectToAction("Index");
                }
                
                    return View();
            }
            return RedirectToAction("Index");
        }
        

        public IActionResult AdminIndex()
        {
            if (User.IsInRole("Admin"))
            {

                return View();

            }


            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> NewAddressAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Id == userId);
            appUser.Address=null;
            
            await  _userManager.UpdateAsync(appUser);


            return RedirectToAction("Checkout", "Order");
        }
       


    }
}
