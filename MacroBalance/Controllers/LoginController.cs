using System;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;
using MacroBalance.Service;
using Macrobalance.Constants;

namespace MacroBalance.Controllers
{
    public class LoginController : Controller
    {
        private readonly MacroBalanceEntities db = new MacroBalanceEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var usuario = db.Usuario.FirstOrDefault(u =>
                    u.CorreoElectronico == model.CorreoElectronico &&
                    u.Contrasena == model.Contrasena
                );

                if (usuario == null)
                {
                    ViewBag.Error = "Credenciales inválidas.";
                    return View(model);
                }

                Session[SessionAtributes.UsuarioId] = usuario.Id;
                Session[SessionAtributes.Nombre] = usuario.Nombre;
                Session[SessionAtributes.Rol] = usuario.Rol;
                Session["FotoPerfil"] = usuario.FotoDePerfilUrl; 

                if (usuario.Rol == UserRoles.Admin)
                    return RedirectToAction("Index", "Administrador");
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ErrorLoggerService.Log(ex, "Login");
                ViewBag.Error = "Ocurrió un error inesperado.";
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
