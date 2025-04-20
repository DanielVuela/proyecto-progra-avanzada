using System;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Service;

namespace MacroBalance.Controllers
{
    public class CalculadoraController : Controller
    {
        [HttpGet]
        public ActionResult CalcularCalorias()
        {
            int? usuarioId = Session["UsuarioId"] as int?;
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            using (var db = new MacroBalanceEntities())
            {
                var usuario = db.Usuario.FirstOrDefault(u => u.Id == usuarioId);
                if (usuario == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                return View(usuario); 
            }
        }

        [HttpPost]
        public ActionResult CalcularCalorias(Usuario model)
        {
            try
            {
                int? usuarioId = Session["UsuarioId"] as int?;
                if (usuarioId == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                using (var db = new MacroBalanceEntities())
                {
                    var usuario = db.Usuario.FirstOrDefault(u => u.Id == usuarioId);
                    if (usuario == null)
                    {
                        return RedirectToAction("Login", "Login");
                    }

                    usuario.FechaDeNacimiento = model.FechaDeNacimiento;
                    usuario.Peso = model.Peso;
                    usuario.Altura = model.Altura;
                    usuario.NivelActividadFisica = model.NivelActividadFisica;
                    usuario.Genero = model.Genero;

                    db.SaveChanges();

                    int edad = 0;
                    if (usuario.FechaDeNacimiento.HasValue)
                    {
                        edad = DateTime.Now.Year - usuario.FechaDeNacimiento.Value.Year;
                        if (DateTime.Now.DayOfYear < usuario.FechaDeNacimiento.Value.DayOfYear)
                            edad--;
                    }

                    double pesoDouble = (double)(usuario.Peso ?? 0m);
                    double alturaDouble = (double)(usuario.Altura ?? 0m) * 100;

                    double tdee = CalculoCaloriasService.CalcularTDEE(
                        pesoDouble,
                        alturaDouble,
                        edad,
                        usuario.Genero,
                        usuario.NivelActividadFisica
                    );

                    ViewBag.TDEE = tdee;

                    return View(usuario); 
                }
            }
            catch (Exception ex)
            {
                ErrorLoggerService.Log(ex, "Calculadora");
                return View();
            }
        }
    }
}
