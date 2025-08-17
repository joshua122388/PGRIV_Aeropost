using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Aeropost.Models;

namespace Aeropost.Controllers
{
    public class PaqueteController : Controller
    {
        
        private readonly Service services = new Service();

        // GET: /Paquete
        public IActionResult Index()
        {
            var lista = services.mostrarPaquete(); // Array
            return View(lista);
        }

        // GET: /Paquete/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var p = services.buscarPaquete(id);
                return View(p);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: /Paquete/Create
        public IActionResult Create()
        {
            return View(new Paquete());
        }

        // POST: /Paquete/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Paquete modelo)
        {
            if (!ModelState.IsValid) return View(modelo);

            try
            {
                services.agregarPaquete(modelo); // genera tracking + fecha
                TempData["ok"] = $"Paquete creado. Tracking: {modelo.NumeroTracking}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
        }

        // GET: /Paquete/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var p = services.buscarPaquete(id);
                return View(p);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Paquete/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Paquete modelo)
        {
            if (!ModelState.IsValid) return View(modelo);

            try
            {
                services.actualizarPaquete(modelo); // NO toca tracking ni fecha
                TempData["ok"] = "Paquete actualizado.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
        }

        // GET: /Paquete/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                var p = services.buscarPaquete(id);
                return View(p);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Paquete/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var p = services.buscarPaquete(id);
                services.eliminarPaquete(p);
                TempData["ok"] = "Paquete eliminado.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: /Paquete/PorCliente?cedula=...
        public IActionResult PorCliente(string cedula)
        {
            var lista = string.IsNullOrWhiteSpace(cedula)
                ? Array.Empty<Paquete>()
                : services.mostrarPaquetesPorCliente(cedula);
            ViewBag.Cedula = cedula;
            return View(lista);
        }
    }
}
