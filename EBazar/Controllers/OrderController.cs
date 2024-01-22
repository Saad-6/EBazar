using EBazar.Data;
using EBazar.Data.Enums;
using EBazar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ThreePointZero.Data.Enums.Address;

namespace EBazar.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrders = _context.Orders
        .Include(o => o.Address)
        .Include(o => o.CartItems)
        .Where(o => o.UserId == userId)
        .ToList();

            return View(userOrders);
        }
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
            .Include(u => u.Cart)
            .ThenInclude(c => c.CartItems).ThenInclude(b => b.Product)
            .FirstOrDefaultAsync(u => u.Id == userId);

            return View(appUser);
        }
        public async Task<IActionResult> MakePaymentAsync(Address address)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems).ThenInclude(b => b.Product)
                .FirstOrDefaultAsync(u => u.Id == userId);

            appUser.Address = address;

            var order = new Order
            {
                Address = address,
                CreatedDate = DateTime.Now,
                OrderStatus = Status.Pending,
                OrderName = appUser.AppUserName,
                CartItems = appUser.Cart.CartItems.ToList(),
                total=appUser.Cart.total,
                UserId = userId
            };
            foreach (var cartItem in appUser.Cart.CartItems)
            {
                // Set the OrderId for each CartItem
                cartItem.OrderId = order.Id;
            }

            _context.Orders.Add(order);
            
            await _context.SaveChangesAsync();
            
            // Clear cart items
           appUser.Cart.CartItems.Clear();
           appUser.Cart.total = 0;

            // Save changes to the updated user (including clearing cart items)
            await _userManager.UpdateAsync(appUser);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userOrder = _context.Orders
        .Include(o => o.Address)
        .Include(o => o.CartItems).ThenInclude(ci => ci.Product)
        .FirstOrDefault(o => o.Id == id);




            if (userOrder== null)
            {

            return RedirectToAction("Index", "Home");
            }
            return View(userOrder);
        }

        public JsonResult GetDistricts(string province)
        {
            // Based on the selected province, fetch the relevant district enum
            switch (province)
            {
                case "Punjab":
                    return Json(Enum.GetNames(typeof(PunjabDistricts)).ToList());
                case "Sindh":
                    return Json(Enum.GetNames(typeof(SindhDistricts)).ToList());
                case "Balochistan":
                    return Json(Enum.GetNames(typeof(BalochistanDistricts)).ToList());
                case "KPK":
                    return Json(Enum.GetNames(typeof(KPKDistricts)).ToList());

                default:
                    return Json(new List<string>()); // Return an empty list if no match
            }
        }

    }
}
