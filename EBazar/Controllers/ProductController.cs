using EBazar.Data;
using EBazar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBazar.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

          public IActionResult Index()
        {
            var Prods=_context.Products.ToList();
            return View(Prods);
        }
        public IActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNew(Product obj)
        {
            _context.Products.Add(obj);
            _context.SaveChanges();
         
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public IActionResult Search(string searchItem)
        {
            if(searchItem != null)
            {
                var matchingProducts = _context.Products
             .Where(p => p.Name.ToUpper().Contains(searchItem.ToUpper()))
             .ToList();


                return View(matchingProducts);

            }
            
           

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit(int id)
        {
            var prod=_context.Products.FirstOrDefault(m => m.Id == id);
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if(obj != null)
            {
                _context.Products.Update(obj);
                _context.SaveChanges();
               
            }
            return RedirectToAction("Index","Product");
        }
        public IActionResult Delete(int id)
        {
            var prod = _context.Products.FirstOrDefault(m => m.Id == id);
            return View(prod);

        }
        [HttpPost]
        public IActionResult Delete(Product obj)
        {
            if (obj != null)
            {
                _context.Products.Remove(obj);
                _context.SaveChanges();

            }
            return RedirectToAction("Index", "Product");
        }

    }
}
