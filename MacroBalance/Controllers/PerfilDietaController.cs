using System;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;
using MacroBalance.Service;


namespace MacroBalance.Controllers
{
    public class PerfilDietaController : Controller
    {
        private MacroBalanceEntities1 db = new MacroBalanceEntities1();

        [HttpGet]
        public ActionResult Asignar(int? usuarioId)
        {
            var usuarios = db.Usuario.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Nombre + " - " + u.CorreoElectronico
            }).ToList();

            DietaViewModel model;

            if (usuarioId.HasValue)
            {
                var perfil = db.PerfilDieta
                    .Where(p => p.UsuarioId == usuarioId.Value)
                    .OrderByDescending(p => p.FechaAsignacion)
                    .FirstOrDefault();

                model = new DietaViewModel
                {
                    UsuarioId = usuarioId.Value,
                    CaloriasObjetivo = perfil?.CaloriasObjetivo ?? 0,
                    Proteinas = perfil?.Proteinas ?? 0,
                    Carbohidratos = perfil?.Carbohidratos ?? 0,
                    Grasas = perfil?.Grasas ?? 0,
                    Agua = perfil?.Agua ?? 0,
                    Ejercicio = perfil?.Ejercicio ?? 0,
                    Usuarios = usuarios
                };
            }
            else
            {
                model = new DietaViewModel
                {
                    Usuarios = usuarios
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Asignar(DietaViewModel model)
        {
            model.Usuarios = db.Usuario.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Nombre + " - " + u.CorreoElectronico
            }).ToList();

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var perfilExistente = db.PerfilDieta
                    .FirstOrDefault(p => p.UsuarioId == model.UsuarioId);

                if (perfilExistente == null)
                {
                    db.PerfilDieta.Add(new PerfilDieta
                    {
                        UsuarioId = model.UsuarioId,
                        CaloriasObjetivo = model.CaloriasObjetivo,
                        Proteinas = model.Proteinas,
                        Carbohidratos = model.Carbohidratos,
                        Grasas = model.Grasas,
                        Agua = model.Agua,
                        Ejercicio = model.Ejercicio,
                        FechaAsignacion = DateTime.Now
                    });
                }
                else
                {
                    perfilExistente.CaloriasObjetivo = model.CaloriasObjetivo;
                    perfilExistente.Proteinas = model.Proteinas;
                    perfilExistente.Carbohidratos = model.Carbohidratos;
                    perfilExistente.Grasas = model.Grasas;
                    perfilExistente.Agua = model.Agua;
                    perfilExistente.Ejercicio = model.Ejercicio;
                    perfilExistente.FechaAsignacion = DateTime.Now;
                }

                db.SaveChanges();
                TempData["Success"] = "Perfil actualizado correctamente.";
                return RedirectToAction("Asignar", new { usuarioId = model.UsuarioId });
            }
            catch (Exception ex)
            {
                ErrorLoggerService.Log(ex, "PerfilDieta/Asignar", model.UsuarioId);
                ViewBag.Error = "Hubo un error al guardar los datos.";
                return View(model);
            }
        }


    }
}