using System;
using System.Collections.Generic;
using Aeropost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aeropost.Controllers
{
    public class PaqueteController : Controller
    {
        private Service services;


        public PaqueteController()
        {
            this.services = new Service();
        }

        // GET: Paquete
        public ActionResult Index()
        {
            try
            {
                var lista = services.ListarPaquetes(); // <- Service
                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new List<Paquete>());
            }
        }

        // GET: Paquete/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var modelo = services.ObtenerPaquete(id); // <- Service
                if (modelo == null) return NotFound();
                return View(modelo);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: Paquete/Create
        public ActionResult Create()
        {
            try
            {
                CargarCombos();
                return View(new Paquete());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new Paquete());
            }
        }

        // POST: Paquete/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paquete modelo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    CargarCombos();
                    return View(modelo);
                }

                if (!services.AgregarPaquete(modelo, out string error)) // <- Service
                {
                    ModelState.AddModelError("", error);
                    CargarCombos();
                    return View(modelo);
                }

                TempData["ok"] = "Paquete registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                CargarCombos();
                return View(modelo);
            }
        }

        // GET: Paquete/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var modelo = services.ObtenerPaquete(id); // <- Service
                if (modelo == null) return NotFound();
                CargarCombos();
                return View(modelo);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // POST: Paquete/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paquete modelo)
        {
            try
            {
                // NO validar campos de solo lectura solo los lee
                ModelState.Remove(nameof(Paquete.NumeroTracking));
                ModelState.Remove(nameof(Paquete.FechaRegistro));

                if (!ModelState.IsValid)
                {
                    CargarCombos();
                    return View(modelo);
                }

                if (!services.ActualizarPaquete(modelo, out string error)) // <- Service
                {
                    ModelState.AddModelError("", error);
                    CargarCombos();
                    return View(modelo);
                }

                TempData["ok"] = "Paquete actualizado.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                CargarCombos();
                return View(modelo);
            }
        }

        // GET: Paquete/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var modelo = services.ObtenerPaquete(id); // <- Service
                if (modelo == null) return NotFound();
                return View(modelo);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // POST: Paquete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (!services.EliminarPaquete(id, out string error)) // <- Service
                {
                    ModelState.AddModelError("", error);
                    var modelo = services.ObtenerPaquete(id);
                    return View("Delete", modelo);
                }

                TempData["ok"] = "Paquete eliminado.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                var modelo = services.ObtenerPaquete(id);
                return View("Delete", modelo);
            }
        }

        // GET: Paquete/ReportePorCliente?cedula=XXXXXXXX
        public ActionResult ReportePorCliente(string cedula)
        {
            try
            {
                ViewBag.Cedula = cedula;
                var lista = string.IsNullOrWhiteSpace(cedula)
                    ? new List<Paquete>()
                    : services.ListarPaquetesPorCliente(cedula); // <- Service
                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new List<Paquete>());
            }
        }

        // ====== Helpers (mismo estilo simple) ======
        private void CargarCombos()
        {
            ViewBag.Tiendas = new SelectList(new[] { "Amazon", "Shein", "Temu", "eBay", "AliExpress" });

            // var clientes = services.ListarClientes();
            // ViewBag.Cedulas = new SelectList(clientes, "Cedula", "Cedula");
        }
    }
}