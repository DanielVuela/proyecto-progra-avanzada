using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Macrobalance.Constants;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;
using MacroBalance.Service;

namespace MacroBalance.Controllers
{
    public class RegistroAlimentoController : Controller
    {
        [HttpGet]
        public ActionResult Crear()
        {
            int usuarioId = (int)Session[SessionAtributes.UsuarioId];

            using (var db = new MacroBalanceEntities1())
            {
                // Obtener la dieta más reciente del usuario
                var dieta = db.Dieta
                    .Where(d => d.IdUsuario == usuarioId)
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault();

                if (dieta == null)
                {
                    TempData["Error"] = "No tenés una dieta activa.";
                    return RedirectToAction("Hoy", "Bitacora");
                }

                var model = new RegistroAlimentoViewModel
                {
                    DietaId = dieta.Id,
                    Alimentos = db.Alimento
                        .Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = a.Nombre
                        }).ToList()
                };

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Crear(RegistroAlimentoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                using (var db = new MacroBalanceEntities1())
                {
                    model.Alimentos = db.Alimento
                        .Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = a.Nombre
                        }).ToList();
                }
                return View(model);
            }

            try
            {
                using (var db = new MacroBalanceEntities1())
                {
                    var nuevo = new RegistroAlimento
                    {
                        DietaId = model.DietaId,
                        AlimentoId = model.AlimentoId,
                        Cantidad = model.Cantidad,
                        Fecha = DateTime.Today
                    };

                    db.RegistroAlimento.Add(nuevo);
                    db.SaveChanges();

                    TempData["Success"] = "Alimento agregado correctamente.";
                    return RedirectToAction("Hoy", "Bitacora");
                }
            }
            catch (Exception ex)
            {
                ErrorLoggerService.Log(ex, "RegistroAlimento/Crear", model.DietaId);
                ViewBag.Error = "Error al registrar alimento.";
                return View(model);
            }
        }

    }
}