using Aeropost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class ClienteController : Controller
    {
        private Service services;

        public ClienteController()
        {
            this.services = new Service();
        }

        // GET: ClienteController
        public ActionResult Index()
        {
            var clientes = services.mostrarCliente();
            return View(clientes);
        }

        // GET: ClienteController/Details/5S
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.agregarCliente(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch { }
            return View();
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(string cedula)
        {
            var clienteAnterior = services.buscarCliente(cedula);
            if (clienteAnterior == null)
            {
                ViewBag.Error = "Ese cliente no está registrado";
                return View();
            }
            return View(clienteAnterior);
        }


        // POST: ClienteController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.actualizarCliente(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch { }
            return View();
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(string cedula)
        {
            try
            {
                var clienteEliminado = services.buscarCliente(cedula);
                services.eliminarCliente(clienteEliminado);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string cedula, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
