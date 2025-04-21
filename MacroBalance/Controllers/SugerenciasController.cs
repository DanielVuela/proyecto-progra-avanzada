
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;

namespace MacroBalance.Controllers
{
    public class SugerenciasController : Controller
    {
        private MacroBalanceEntities db = new MacroBalanceEntities();


        public ActionResult Index(DateTime? fechaInicio, DateTime? fechaFin, string estadoFiltro)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Index", "Login");

            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

            var estadosEmocionales = db.EstadoEmocional
                .Where(e => e.UsuarioId == usuarioId);

            if (fechaInicio.HasValue)
                estadosEmocionales = estadosEmocionales.Where(e => e.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                estadosEmocionales = estadosEmocionales.Where(e => e.Fecha <= fechaFin.Value);

            if (!string.IsNullOrEmpty(estadoFiltro))
                estadosEmocionales = estadosEmocionales.Where(e => e.Estado == estadoFiltro);

            var vista = new List<SugerenciaEmocionalViewModel>();

            foreach (var estado in estadosEmocionales.ToList())
            {
                string estadoNormalizado = RemoveDiacritics(estado.Estado).ToLower();

                var sugerenciasRelacionadas = db.SugerenciaBase
                    .ToList()
                    .Where(s => RemoveDiacritics(s.EstadoRelacionado).ToLower() == estadoNormalizado)
                    .Select(s => s.Descripcion)
                    .ToList();

                vista.Add(new SugerenciaEmocionalViewModel
                {
                    EstadoEmocionalId = estado.Id,
                    Estado = estado.Estado,
                    Descripcion = estado.Descripcion,
                    Fecha = estado.Fecha,
                    Sugerencias = sugerenciasRelacionadas
                });
            }

            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");
            ViewBag.EstadoFiltro = estadoFiltro;
            ViewBag.EstadosDisponibles = new List<string>
            {
                "Ansiedad", "Tristeza", "Felicidad", "Estrés", "Irritabilidad", "Calma", "Motivación"
            };

            return View(vista);
        }

        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            var normalized = text.Normalize(System.Text.NormalizationForm.FormD);
            return new string(normalized
                .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
