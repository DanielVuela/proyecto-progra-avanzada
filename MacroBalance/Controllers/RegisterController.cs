using System;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;
using MacroBalance.Service;
using Macrobalance.Constants;

namespace MacroBalance.Controllers
{
    public class RegisterController : Controller
    {
        private MacroBalanceEntities db = new MacroBalanceEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index"); 
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Index", model); 

                var existente = db.Usuario.FirstOrDefault(u => u.CorreoElectronico == model.CorreoElectronico);
                if (existente != null)
                {
                    ModelState.AddModelError("", "Este correo ya está registrado.");
                    return View("Index", model); 
                }

                var nuevoUsuario = new Usuario
                {
                    Nombre = model.Nombre,
                    CorreoElectronico = model.CorreoElectronico,
                    Contrasena = model.Contrasena,
                    Rol = UserRoles.Usuario,
                };

                db.Usuario.Add(nuevoUsuario);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ErrorLoggerService.Log(e, "Register");
                ModelState.AddModelError("", "Ocurrió un error inesperado.");
                return View("Index", model); 
            }

            return RedirectToAction("Index", "Login");
        }
    }
}

