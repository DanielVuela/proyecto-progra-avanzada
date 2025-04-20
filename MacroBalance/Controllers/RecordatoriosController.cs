using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Service;
using Macrobalance.Constants;

namespace MacroBalance.Controllers
{
    public class RecordatoriosController : Controller
    {
        private readonly MacroBalanceEntities db = new MacroBalanceEntities();

        private int ObtenerUsuarioActualId()
        {
            if (Session[SessionAtributes.UsuarioId] != null)
            {
                return Convert.ToInt32(Session[SessionAtributes.UsuarioId]);
            }
            return -1;
        }

 
        public ActionResult Index()
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

        

        public ActionResult RegistroComida()
        {
            int usuarioId = ObtenerUsuarioActualId();
            if (usuarioId == -1)
            {
                TempData["ErrorMessage"] = "Usuario no autenticado.";
                return RedirectToAction("Index");
            }

            var recordatorio = db.Recordatorios
                .FirstOrDefault(r => r.UsuarioId == usuarioId && r.Nombre == "Registro de Comidas");

            if (recordatorio == null)
            {
                recordatorio = new Recordatorios
                {
                    Frecuencia = "Diario",
                    Hora = new TimeSpan(12, 0, 0),
                    Nombre = "Registro de Comidas"
                };
            }

            return View(recordatorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistroComida(Recordatorios model)
        {
            int usuarioId = ObtenerUsuarioActualId();
            if (usuarioId == -1)
            {
                TempData["ErrorMessage"] = "No estás autenticado.";
                return RedirectToAction("RegistroComida");
            }

            var recordatorio = db.Recordatorios
                .FirstOrDefault(r => r.UsuarioId == usuarioId && r.Nombre == "Registro de Comidas");

            if (recordatorio == null)
            {
                recordatorio = new Recordatorios
                {
                    UsuarioId = usuarioId,
                    Nombre = "Registro de Comidas",
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
                ErrorLoggerService.Log(ex, "GuardarRecordatorioComida");
                TempData["ErrorMessage"] = "Error al guardar el recordatorio.";
            }

            return RedirectToAction("RegistroComida");
        }

        
    
        public ActionResult EjercicioFisico()
        {
            int usuarioId = ObtenerUsuarioActualId();
            if (usuarioId == -1)
            {
                TempData["ErrorMessage"] = "Usuario no autenticado.";
                return RedirectToAction("Index");
            }

            var recordatorio = db.Recordatorios
                .FirstOrDefault(r => r.UsuarioId == usuarioId && r.Nombre == "Ejercicio Físico");

            if (recordatorio == null)
            {
                recordatorio = new Recordatorios
                {
                    Frecuencia = "Diario",
                    Hora = new TimeSpan(18, 0, 0),
                    Nombre = "Ejercicio Físico"
                };
            }

            return View(recordatorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EjercicioFisico(Recordatorios model)
        {
            int usuarioId = ObtenerUsuarioActualId();
            if (usuarioId == -1)
            {
                TempData["ErrorMessage"] = "No estás autenticado.";
                return RedirectToAction("EjercicioFisico");
            }

            var recordatorio = db.Recordatorios
                .FirstOrDefault(r => r.UsuarioId == usuarioId && r.Nombre == "Ejercicio Físico");

            if (recordatorio == null)
            {
                recordatorio = new Recordatorios
                {
                    UsuarioId = usuarioId,
                    Nombre = "Ejercicio Físico",
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
                ErrorLoggerService.Log(ex, "GuardarRecordatorioEjercicio");
                TempData["ErrorMessage"] = "Error al guardar el recordatorio.";
            }

            return RedirectToAction("EjercicioFisico");
        }

        
        public ActionResult CaloriasMaximas()
        {
            int usuarioId = ObtenerUsuarioActualId();

            if (usuarioId == -1)
            {
                TempData["ErrorMessage"] = "Usuario no autenticado.";
                return RedirectToAction("Index");
            }

            var recordatorio = db.Recordatorios
                .FirstOrDefault(r => r.UsuarioId == usuarioId && r.Nombre == "Calorías Máximas");

            if (recordatorio == null)
            {
                recordatorio = new Recordatorios
                {
                    Frecuencia = "Diario",
                    Hora = new TimeSpan(14, 0, 0),
                    Nombre = "Calorías Máximas"
                };
            }

            return View(recordatorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult CaloriasMaximas(Recordatorios model)
        {
            int usuarioId = ObtenerUsuarioActualId();

            if (usuarioId == -1)
            {
                
                if (TempData["SuccessMessage"] == null)
                {
                    TempData["ErrorMessage"] = "No estás autenticado.";
                }

                
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
                TempData["SuccessMessage"] = "Recordatorio de calorías guardado exitosamente.";
            }
            catch (Exception ex)
            {
                ErrorLoggerService.Log(ex, "GuardarRecordatorioCalorias");
                TempData["ErrorMessage"] = "Error al guardar el recordatorio.";
            }

            return RedirectToAction("CaloriasMaximas");
        }
    }
}
