using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Aeropost.Models;

namespace Aeropost.Controllers
{
    public class PaqueteController : Controller
    {
        // LISTA
        public IActionResult Index()
        {
            using var db = new Service();
            var data = db.ListarPaquetes().ToArray();
            return View(data);
        }

        // DETALLE
        public IActionResult Details(int id)
        {
            using var db = new Service();
            var p = db.ObtenerPaquete(id);
            if (p == null) { TempData["err"] = "Paquete no encontrado."; return RedirectToAction(nameof(Index)); }
            return View(p);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Paquete model)
        {
            using var db = new Service();
            if (!ModelState.IsValid) return View(model);

            if (!db.AgregarPaquete(model, out var error))
            {
                ModelState.AddModelError(string.Empty, error);
                return View(model);
            }
            TempData["ok"] = "Paquete registrado.";
            return RedirectToAction(nameof(Index));
        }

        // EDIT
        [HttpGet]
        public IActionResult Edit(int id)
        {
            using var db = new Service();
            var p = db.ObtenerPaquete(id);
            if (p == null) { TempData["err"] = "Paquete no encontrado."; return RedirectToAction(nameof(Index)); }
            return View(p);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Paquete model)
        {
            using var db = new Service();
            if (!ModelState.IsValid) return View(model);

            if (!db.ActualizarPaquete(model, out var error))
            {
                ModelState.AddModelError(string.Empty, error);
                return View(model);
            }
            TempData["ok"] = "Paquete actualizado.";
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpGet]
        public IActionResult Delete(int id)
        {
            using var db = new Service();
            var p = db.ObtenerPaquete(id);
            if (p == null) { TempData["err"] = "Paquete no encontrado."; return RedirectToAction(nameof(Index)); }
            return View(p);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using var db = new Service();
            if (!db.EliminarPaquete(id, out var error))
                TempData["err"] = error;
            else
                TempData["ok"] = "Paquete eliminado.";
            return RedirectToAction(nameof(Index));
        }

        // Reporte por cédula
        public IActionResult PorCliente(string cedula)
        {
            using var db = new Service();
            ViewBag.Cedula = cedula;
            var data = string.IsNullOrWhiteSpace(cedula)
                ? db.ListarPaquetes().ToArray()
                : db.ListarPaquetesPorCliente(cedula).ToArray();
            return View(data);
        }
    }
}
