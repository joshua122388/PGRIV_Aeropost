using System.Diagnostics;
using Aeropost.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var nombreUsuario = HttpContext.Session.GetString("nombreUsuario");
            if (nombreUsuario != null)
                ViewBag.NombreUsuario = nombreUsuario;
            else
                return RedirectToAction("Login", "Usuario");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
