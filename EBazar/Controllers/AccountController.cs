using Microsoft.AspNetCore.Mvc;

namespace EBazar.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
