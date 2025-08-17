using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Aeropost.Models;

namespace Aeropost.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            using var db = new Service();
            return View(db.mostrarCliente() as Cliente[]);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Cliente model)
        {
            if (!ModelState.IsValid) return View(model);
            using var db = new Service();
            db.agregarCliente(model);
            TempData["ok"] = "Cliente creado.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string cedula)
        {
            using var db = new Service();
            return View(db.buscarCliente(cedula));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Cliente model)
        {
            if (!ModelState.IsValid) return View(model);
            using var db = new Service();
            db.actualizarCliente(model);
            TempData["ok"] = "Cliente actualizado.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(string cedula)
        {
            using var db = new Service();
            return View(db.buscarCliente(cedula));
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string cedula)
        {
            using var db = new Service();
            var c = db.clientes.FirstOrDefault(x => x.Cedula == cedula);
            if (c != null) { db.eliminarCliente(c); TempData["ok"] = "Cliente eliminado."; }
            return RedirectToAction(nameof(Index));
        }

        // Reporte por tipo
        public IActionResult PorTipo(string tipo)
        {
            using var db = new Service();
            ViewBag.Tipo = tipo;
            var data = string.IsNullOrWhiteSpace(tipo) ? db.mostrarCliente() as Cliente[] : db.mostrarClientesPorTipo(tipo) as Cliente[];
            return View(data);
        }
    }
}
