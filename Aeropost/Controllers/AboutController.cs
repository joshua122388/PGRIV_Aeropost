using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class AboutController : Controller
    {
        // GET: /About/Index
        public IActionResult Index()
        {
            return View("AboutUs");
        }
    }
}