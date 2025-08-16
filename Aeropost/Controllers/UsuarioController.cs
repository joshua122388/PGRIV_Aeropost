using Aeropost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class UsuarioController : Controller
    {

        private Service services;

        public UsuarioController()
        {
            this.services = new Service();
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            var usuarios = services.mostrarUsuario();
            return View(usuarios);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario, string passConfirmacion)
        {
            try
            {
                // Verificar que ambos campos de contraseña estén llenos
                if (string.IsNullOrEmpty(usuario.Pass) || string.IsNullOrEmpty(passConfirmacion))
                {
                    ViewBag.ErrorMessage = "Ambos campos de contraseña deben estar llenos";
                    return View(usuario);
                }

                // Validar que las contraseñas coincidan
                var validacionUsuario = usuario.validacionClave(usuario.Pass, passConfirmacion);
                if (validacionUsuario == false)
                {
                    ViewBag.ErrorMessage = "Las contraseñas no coinciden";
                    return View(usuario);
                }

                if (ModelState.IsValid)
                {
                    services.agregarUsuario(usuario);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Verifique que los datos ingresados sean válidos";
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "Ocurrió un error al crear el usuario";
            }
            return View(usuario);
        }


        // GET: UsuarioController/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: UsuarioController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string usuario, string password)
        {
            try
            {
                var usuarioLogueado = services.login(usuario, password);
                // redireccionamiento temporal, esto puedo cambiar luego
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            var usuarioAnterior = services.buscarUsuario(id);
            return View(usuarioAnterior);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario, string passConfirmacion)
        {
            try
            {
                // Solo validar contraseñas si al menos uno de los campos tiene contenido
                if (!string.IsNullOrEmpty(usuario.Pass) || !string.IsNullOrEmpty(passConfirmacion))
                {
                    // Verificar que ambos campos estén llenos
                    if (string.IsNullOrEmpty(usuario.Pass) || string.IsNullOrEmpty(passConfirmacion))
                    {
                        ViewBag.ErrorMessage = "Ambos campos de contraseña deben estar llenos para actualizar la clave";
                        return View(usuario);
                    }

                    // Validar que las contraseñas coincidan
                    var validacionUsuario = usuario.validacionClave(usuario.Pass, passConfirmacion);
                    if (validacionUsuario == false)
                    {
                        ViewBag.ErrorMessage = "Las contraseñas no son las mismas";
                        return View(usuario);
                    }
                }

                if (ModelState.IsValid)
                {
                    services.actualizarUsuario(usuario);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Verique que los datos ingresados sean validos";
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "Ocurrió un error al actualizar el usuario";
            }
            return View(usuario);
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            try{ 
                var buscarUsuario = services.buscarUsuario(id);
                services.eliminarUsuario(buscarUsuario);
                return RedirectToAction("Index");
            }
            
            catch(Exception ex){   
            
                return View(ex);
            }
            
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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