using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Aeropost.Models;

namespace Aeropost.Controllers
{
    public class FacturaController : Controller
    {
        // LISTA
        public IActionResult Index()
        {
            using var db = new Service();
            var data = db.ObtenerFacturas();
            return View(data);
        }

        // CREATE por tracking (GET+POST)
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(string tracking)
        {
            if (string.IsNullOrWhiteSpace(tracking))
            {
                ModelState.AddModelError(string.Empty, "Debe indicar el número de tracking.");
                return View();
            }

            using var db = new Service();
            var pack = db.Paquetes.FirstOrDefault(p => p.NumeroTracking == tracking);
            if (pack == null)
            {
                ModelState.AddModelError(string.Empty, "No existe un paquete con ese tracking.");
                return View();
            }

            // Cálculo de monto total según enunciado:
            // Monto = (Peso × 12) + (13% de ValorTotal) + (10% de ValorTotal si especial)
            decimal baseTarifa = 12m;
            decimal monto = (decimal)pack.Peso * baseTarifa
                            + (decimal)pack.Val orTotal * 0.13m
                            + (pack.ProductosEspeciales ? (decimal)pack.ValorTotal * 0.10m : 0m);

            var factura = new Factura
            {
                NumeroFactura = $"FAC{DateTime.Now:yyyyMMddHHmmss}",
                NumeroTracking = pack.NumeroTracking,
                CedulaCliente = pack.CedulaCliente,
                FechaEntrega = DateTime.Now, // puedes editar luego
                MontoTotal = monto
            };

            db.AgregarFactura(factura);
            TempData["ok"] = $"Factura {factura.NumeroFactura} creada.";
            return RedirectToAction(nameof(Index));
        }

        // EDIT
        [HttpGet]
        public IActionResult Edit(int id)
        {
            using var db = new Service();
            var f = db.Facturas.Find(id);
            if (f == null) { TempData["err"] = "Factura no encontrada."; return RedirectToAction(nameof(Index)); }
            return View(f);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Factura model)
        {
            if (!ModelState.IsValid) return View(model);
            using var db = new Service();
            db.ActualizarFactura(model);
            TempData["ok"] = "Factura actualizada.";
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpGet]
        public IActionResult Delete(int id)
        {
            using var db = new Service();
            var f = db.Facturas.Find(id);
            if (f == null) { TempData["err"] = "Factura no encontrada."; return RedirectToAction(nameof(Index)); }
            return View(f);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using var db = new Service();
            db.EliminarFactura(id);
            TempData["ok"] = "Factura eliminada.";
            return RedirectToAction(nameof(Index));
        }

        // Reporte: total por mes
        public IActionResult TotalPorMes(int? mes, int? anio)
        {
            ViewBag.Mes = mes; ViewBag.Anio = anio;
            if (mes is null || anio is null) return View();

            using var db = new Service();
            var total = db.Facturas
                          .Where(f => f.FechaEntrega.Month == mes && f.FechaEntrega.Year == anio)
                          .Sum(f => (decimal?)f.MontoTotal) ?? 0m;
            ViewBag.Total = total;
            return View();
        }

        // Detalle por cédula (lista)
        public IActionResult PorCedula(string cedula)
        {
            ViewBag.Cedula = cedula;
            using var db = new Service();
            var data = string.IsNullOrWhiteSpace(cedula)
                ? db.Facturas.ToList()
                : db.Facturas.Where(f => f.CedulaCliente == cedula).ToList();
            return View(data);
        }

        // Buscar por tracking (una)
        public IActionResult PorTracking(string tracking)
        {
            ViewBag.Tracking = tracking;
            using var db = new Service();
            var f = string.IsNullOrWhiteSpace(tracking) ? null : db.Facturas.FirstOrDefault(x => x.NumeroTracking == tracking);
            return View(f);
        }
    }
}
