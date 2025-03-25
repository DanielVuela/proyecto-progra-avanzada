using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacroBalance.Controllers
{
    public class AccountController : Controller
    {
        // Acción para mostrar la vista del perfil de usuario
        public ActionResult UserProfile()
        {
            // cargar los datos del usuario desde la base de datos
            // Ejemplo:
            // var user = UserService.GetUserById(User.Identity.Name);
            // return View(user);

            return View();
        }

        // Acción para mostrar la vista de reinicio de contraseña
        public ActionResult ResetPassword()
        {
            return View();
        }

        // Acción para mostrar la vista de  ajustes de cuenta
        public ActionResult AjustesCuenta()
        {
            return View();
        }

        // Acción para procesar el formulario de reinicio de contraseña
        [HttpPost]
        public ActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                TempData["ErrorMessage"] = "Los campos de contraseña no pueden estar vacíos.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                TempData["ErrorMessage"] = "Las contraseñas no coinciden.";
                return View();
            }

            try
            {
                // Lógica para actualizar la contraseña en la base de datos
                // Ejemplo:
                // UserService.UpdatePassword(User.Identity.Name, newPassword);

                TempData["SuccessMessage"] = "Contraseña actualizada correctamente.";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                TempData["ErrorMessage"] = "Ocurrió un error al actualizar la contraseña: " + ex.Message;
                return View();
            }
        }

        public ActionResult Recordatorios()
        {
            return View();
        }

    }
}