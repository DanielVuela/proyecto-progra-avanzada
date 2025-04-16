using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Models;
using MacroBalance.Service;
using Macrobalance.Constants;

namespace MacroBalance.Controllers
{
    public class RecordatoriosController : Controller
    {
        private readonly MacroBalanceEntities db = new MacroBalanceEntities();

        public ActionResult Recordatorios()
        {
            int usuarioId = ObtenerUsuarioActualId();

            if (usuarioId == -1)
            {
                TempData["ErrorMessage"] = "Usuario no autenticado.";
                return View(new List<Recordatorios>());
            }

            var recordatorios = db.Recordatorios.Where(r => r.UsuarioId == usuarioId).ToList();
            return View(recordatorios);
        }

        public ActionResult CaloriasMaximas()
        {
            int usuarioId = ObtenerUsuarioActualId();

            if (usuarioId == -1)
            {
                TempData["ErrorMessage"] = "Usuario no autenticado.";
                return View(new Recordatorios());
            }

            var recordatorio = db.Recordatorios
                                 .FirstOrDefault(r => r.UsuarioId == usuarioId && r.Nombre == "Calorías Máximas");

            return View(recordatorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarRecordatorioCalorias(RecordatorioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Datos inválidos.";
                return RedirectToAction("CaloriasMaximas");
            }

            int usuarioId = ObtenerUsuarioActualId();

            if (usuarioId == -1)
            {
                TempData["WarningMessage"] = "No estás autenticado.";
                return RedirectToAction("CaloriasMaximas");
            }

            bool usuarioExiste = db.Usuario.Any(u => u.Id == usuarioId);

            if (!usuarioExiste)
            {
                TempData["ErrorMessage"] = "El usuario no existe en la base de datos.";
                return RedirectToAction("CaloriasMaximas");
            }

            var recordatorio = db.Recordatorios
                                 .FirstOrDefault(r => r.UsuarioId == usuarioId && r.Nombre == "Calorías Máximas");

            if (recordatorio == null)
            {
                recordatorio = new Recordatorios
                {
                    UsuarioId = usuarioId,
                    Nombre = "Calorías Máximas",
                    Frecuencia = model.Frecuencia,
                    Hora = model.Hora
                };
                db.Recordatorios.Add(recordatorio);
            }
            else
            {
                recordatorio.Frecuencia = model.Frecuencia;
                recordatorio.Hora = model.Hora;
                db.Entry(recordatorio).State = System.Data.Entity.EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
                TempData["SuccessMessage"] = "Recordatorio guardado exitosamente.";
            }
            catch (Exception ex)
            {
                ErrorLoggerService.Log(ex, "GuardarRecordatorio");
                TempData["ErrorMessage"] = "Hubo un problema al guardar el recordatorio.";
            }

            return RedirectToAction("CaloriasMaximas");
        }

        private int ObtenerUsuarioActualId()
        {
            if (Session[SessionAtributes.UsuarioId] != null)
            {
                return Convert.ToInt32(Session[SessionAtributes.UsuarioId]);
            }
            return -1;
        }

        public ActionResult EjercicioFisico()
        {
            return View();
        }

        public ActionResult RegistroComida()
        {
            return View();
        }


    }
}
