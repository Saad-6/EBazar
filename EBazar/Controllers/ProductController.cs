using EBazar.Data;
using EBazar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace EBazar.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _client;
        public ProductController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44356");
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
        public async Task<IActionResult> Search(string searchItem)
        {
            if(searchItem != null)
            {
                // Find products in current project
                var matchingProducts = _context.Products
                .Where(p => p.Name.ToUpper().Contains(searchItem.ToUpper()))
                .ToList();

                // If not found call the search API of the other project
                if (matchingProducts.Count==0)
                {
                   // Calling the api and storing json response (Response is a List)
                    
                    var apiResponse = await _client.GetStringAsync("/api/ProductAPI/Search?query=" + searchItem);
                    
                    // Parse the List and keep count of total prods returned
                    
                    JArray json = JArray.Parse(apiResponse);
                    int count = json.Count();
                    
                    // Create a new List to convert json to custom object

                    List<Product> products = new List<Product>();   
                    
                    // Iterate through each parsed index and assign values to a new product object each time 
                    // at the end of the loop , add the product object to the products list
                    // 
                    for(int i = 0; i < count; i++)
                    {
                        var product = new Product
                        {
                            Id = (int)json[i]["id"],           
                            Name = (string)json[i]["name"],      
                            PictureUrl = (string)json[i]["pictureUrl"],
                            Description = (string)json[i]["description"],
                            Price = (int)json[i]["price"]
                         
                        };
                        products.Add(product);

                    }
                   
                    // If atleast one product is found return it to the VIew
                    if (products.Count>0)
                    {

                    return View(products);
                    }

                }

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
