using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MacroBalance.Database;

namespace MacroBalance.Controllers
{
    public class AccountController : Controller
    {
        private MacroBalanceEntities db = new MacroBalanceEntities();

        public ActionResult UserProfile()
        {
            int? userId = Session["UsuarioId"] as int?;
            if (userId == null)
                return RedirectToAction("Login", "Login");

            var usuario = db.Usuario.Find(userId);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult UserProfile(Usuario model, HttpPostedFileBase foto)
        {
            int? userId = Session["UsuarioId"] as int?;
            if (userId == null)
                return RedirectToAction("Login", "Login");

            var usuario = db.Usuario.Find(userId);
            if (usuario == null)
                return HttpNotFound();

            usuario.Nombre = model.Nombre;
            usuario.Apellidos = model.Apellidos;
            usuario.FechaDeNacimiento = model.FechaDeNacimiento;
            usuario.Peso = model.Peso;
            usuario.Altura = model.Altura;
            usuario.NivelActividadFisica = model.NivelActividadFisica;
            usuario.Genero = model.Genero;

            if (foto != null && foto.ContentLength > 0)
            {
                var nombreArchivo = $"perfil_{userId}_{Path.GetFileName(foto.FileName)}";
                var rutaDirectorio = Server.MapPath("~/assets/img/perfiles");

                if (!Directory.Exists(rutaDirectorio))
                {
                    Directory.CreateDirectory(rutaDirectorio);
                }

                var rutaGuardar = Path.Combine(rutaDirectorio, nombreArchivo);
                foto.SaveAs(rutaGuardar);

                usuario.FotoDePerfilUrl = $"/assets/img/perfiles/{nombreArchivo}";
            }

            db.SaveChanges();

            Session["FotoPerfil"] = usuario.FotoDePerfilUrl;
            Session["Nombre"] = usuario.Nombre;

            TempData["SuccessMessage"] = "Perfil actualizado correctamente.";
            return RedirectToAction("UserProfile");
        }

        public ActionResult ResetPassword() => View();

        public ActionResult AjustesCuenta() => View();

        public ActionResult Recordatorios() => View();
    }
}
