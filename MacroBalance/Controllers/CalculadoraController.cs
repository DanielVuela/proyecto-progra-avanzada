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
            return View();
        }

        [HttpPost]
        public ActionResult CalcularCalorias(Usuario model)
        {
            try
            {
                using (var db = new MacroBalanceEntities1())
                {
                    var usuarioExistente = db.Usuario
                        .FirstOrDefault(u => u.CorreoElectronico == model.CorreoElectronico);

                    if (usuarioExistente == null)
                    {
                        db.Usuario.Add(model);
                    }
                    else
                    {
                        usuarioExistente.Nombre = model.Nombre;
                        usuarioExistente.Apellidos = model.Apellidos;
                        usuarioExistente.FotoDePerfilUrl = model.FotoDePerfilUrl;
                        usuarioExistente.FechaDeNacimiento = model.FechaDeNacimiento;
                        usuarioExistente.Peso = model.Peso;
                        usuarioExistente.Altura = model.Altura;
                        usuarioExistente.NivelActividadFisica = model.NivelActividadFisica;
                        usuarioExistente.Genero = model.Genero;
                    }

                    db.SaveChanges();

                    // Calcular edad
                    int edad = 0;
                    if (model.FechaDeNacimiento.HasValue)
                    {
                        edad = DateTime.Now.Year - model.FechaDeNacimiento.Value.Year;
                        if (DateTime.Now.DayOfYear < model.FechaDeNacimiento.Value.DayOfYear)
                            edad--;
                    }

                    double pesoDouble = (double)(model.Peso ?? 0m);
                    double alturaDouble = (double)(model.Altura ?? 0m) * 100;

                    double tdee = CalculoCaloriasService.CalcularTDEE(
                        pesoDouble,
                        alturaDouble,
                        edad,
                        model.Genero,
                        model.NivelActividadFisica
                    );

                    ViewBag.TDEE = tdee;
                }

                return View();
            }
            catch (Exception ex)
            {
                ErrorLoggerService.Log(ex, "Calculadora");
                return View();
            }
        }
    }
}
