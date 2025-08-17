using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Aeropost.Models;

namespace Aeropost.Controllers
{
    public class UsuarioController : Controller
    {
        // LISTA
        public IActionResult Index()
        {
            using var db = new Service();
            var data = db.usuarios.ToArray(); // tu Service expone DbSet usuarios
            return View(data);
        }

        // LOGIN (GET)
        [HttpGet]
        public IActionResult Login() => View();

        // LOGIN (POST)
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(string usuario, string password)
        {
            try
            {
                using var db = new Service();
                var u = db.login(usuario, password); // valida activo
                // sesión
                HttpContext.Session.SetString("User", u.User ?? "");
                HttpContext.Session.SetString("NombreCompleto", u.Nombre ?? "");
                // bitácora
                db.registrarLogin(u);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // CREATE
        [HttpGet]
        public IActionResult Create() => View(new Usuario { Estado = "activo", FechaRegistro = DateTime.Now });

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Usuario model)
        {
            var confirm = Request.Form["passConfirmacion"].ToString();
            if (model.Pass != confirm)
                ModelState.AddModelError("Pass", "Las claves no coinciden.");

            if (!ModelState.IsValid) return View(model);

            using var db = new Service();
            model.FechaRegistro = DateTime.Now;
            db.agregarUsuario(model);
            TempData["ok"] = "Usuario creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // EDIT
        [HttpGet]
        public IActionResult Edit(int id)
        {
            using var db = new Service();
            var u = db.buscarUsuario(id);
            return View(u);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario model)
        {
            if (!ModelState.IsValid) return View(model);

            using var db = new Service();
            // actualiza SIN tocar la cédula
            var u = db.usuarios.FirstOrDefault(x => x.Id == model.Id);
            if (u == null) { TempData["err"] = "Usuario no encontrado."; return RedirectToAction(nameof(Index)); }

            u.Nombre = model.Nombre;
            u.Genero = model.Genero;
            u.Estado = model.Estado;
            u.User = model.User;
            u.Pass = model.Pass;
            db.SaveChanges();

            TempData["ok"] = "Usuario actualizado.";
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpGet]
        public IActionResult Delete(int id)
        {
            using var db = new Service();
            var u = db.buscarUsuario(id);
            return View(u);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using var db = new Service();
            var u = db.usuarios.FirstOrDefault(x => x.Id == id);
            if (u != null) { db.eliminarUsuario(u); TempData["ok"] = "Usuario eliminado."; }
            return RedirectToAction(nameof(Index));
        }
    }
}
