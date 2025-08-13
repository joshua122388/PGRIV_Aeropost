using Aeropost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Aeropost.Controllers
{
    public class FacturaController : Controller
    {
        private readonly Service _context;

        // GET: /Factura/
        public IActionResult Index()
        {
            var facturas = _context.Facturas.ToList();
            return View(facturas);
        }

        // GET: /Factura/Details/5
        public IActionResult Details(int id)
        {
            var factura = _context.Facturas.FirstOrDefault(f => f.Id == id);
            if (factura == null)
                return NotFound();

            return View(factura);
        }

        // GET: /Factura/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Factura/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Facturas.Add(factura);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: /Factura/Edit/5
        public IActionResult Edit(int id)
        {
            var factura = _context.Facturas.FirstOrDefault(f => f.Id == id);
            if (factura == null)
                return NotFound();

            return View(factura);
        }

        // POST: /Factura/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Factura factura)
        {
            if (id != factura.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var facturaExistente = _context.Facturas.FirstOrDefault(f => f.Id == id);
                if (facturaExistente == null)
                    return NotFound();

                // Actualizar campos
                facturaExistente.NumeroFactura = factura.NumeroFactura;
                facturaExistente.Fecha = factura.Fecha;
                facturaExistente.CedulaCliente = factura.CedulaCliente;
                facturaExistente.MontoTotal = factura.MontoTotal;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: /Factura/Delete/5
        public IActionResult Delete(int id)
        {
            var factura = _context.Facturas.FirstOrDefault(f => f.Id == id);
            if (factura == null)
                return NotFound();

            return View(factura);
        }

        // POST: /Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var factura = _context.Facturas.FirstOrDefault(f => f.Id == id);
            if (factura == null)
                return NotFound();

            _context.Facturas.Remove(factura);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}