using EBazar.Data;
using EBazar.Models;
using EBazar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace EBazar.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly IProductService _service;

        // For using the API

        private readonly HttpClient _client;
        public ProductController( IHttpClientFactory httpClientFactory, IProductService service)
        {
            
            _service = service;
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44356");
        }

        // Display all Products on the Index page
        public async Task<IActionResult> IndexAsync()
        {
            var Prods= await _service.GetAllProductsAsync();
            return View(Prods);
        }

        // View For Adding a New Product
        public IActionResult AddNew()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        // Actually Adding The Product To DB
        [HttpPost]
        public async Task<IActionResult> AddNewAsync(Product obj)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
               await _service.AddProductAsync(obj);
            }
         
            return RedirectToAction("Index","Home");
        }

        // Seaching The Product From Current and Previous Project
        [HttpPost]
        public async Task<IActionResult> Search(string searchItem)
        {
            if (searchItem != null)
            {
                // Find products in the current project
                var matchingProducts = await _service.GetProductByNameAsync(searchItem);

                // If not found, call the search API of the other project
                if (matchingProducts==null)
                {
                    try
                    {
                        // Calling the API and storing JSON response (Response is a List)
                        var apiResponse = await _client.GetStringAsync("/api/ProductAPI/Search?query=" + searchItem);

                        // Parse the List and keep count of total prods returned
                        if (!string.IsNullOrEmpty(apiResponse))
                        {
                            JArray json = JArray.Parse(apiResponse);
                            int count = json.Count();

                            // Create a new Product List to convert JSON to a custom object
                            List<Product> products = new List<Product>();

                            // Iterate through each parsed index and assign values to a new product object each time
                            // at the end of the loop, add the product object to the products list
                            for (int i = 0; i < count; i++)
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

                            // If at least one product is found, return it to the view
                            if (products.Count > 0)
                            {
                                return View(products);
                            }
                        }
                    }
                    catch (HttpRequestException)
                    {
                       
                    }
                }

                return View(matchingProducts);
            }

            return RedirectToAction("Index", "Home");
        }


        // Edit a Product , First of all , Find it and Return it in a View to be Edited
        public async Task<IActionResult> EditAsync(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

           var prod= await _service.GetProductByIdAsync(id);
            return View(prod);
        }


        // Actually Editing the Product 
        [HttpPost]
        public async Task<IActionResult> EditAsync(Product obj)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (obj != null)
            {
               await _service.UpdateProductAsync(obj);
               
            }
            return RedirectToAction("Index","Product");
        }

        // Delete a Product , First of all , Find it and Return it in a View to be Deleted
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            var prod = await _service.GetProductByIdAsync(id);
            return View(prod);

        }

        // Actual Deletion Process
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(Product obj)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (obj != null)
            {
                await _service.DeleteProductAsync(obj);

            }
            return RedirectToAction("Index", "Product");
        }

    }
}
