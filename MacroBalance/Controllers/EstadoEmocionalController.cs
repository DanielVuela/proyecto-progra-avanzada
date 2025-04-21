using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MacroBalance.Database;

namespace MacroBalance.Controllers
{
    public class EstadoEmocionalController : Controller
    {
        private MacroBalanceEntities db = new MacroBalanceEntities();

        public ActionResult Index(DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Index", "Login");

            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
            var estados = db.EstadoEmocional.Where(e => e.UsuarioId == usuarioId);

            if (fechaInicio.HasValue)
                estados = estados.Where(e => e.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                estados = estados.Where(e => e.Fecha <= fechaFin.Value);

            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");

            return View(estados.ToList());
        }

        public ActionResult Create()
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Index", "Login");

            ViewBag.EstadosDisponibles = new List<string>
            {
                "Ansiedad", "Tristeza", "Felicidad", "Estrés", "Irritabilidad", "Calma", "Motivación"
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Estado,Descripcion,Fecha")] EstadoEmocional estadoEmocional)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Index", "Login");

            estadoEmocional.UsuarioId = Convert.ToInt32(Session["UsuarioId"]);

            if (ModelState.IsValid)
            {
                db.EstadoEmocional.Add(estadoEmocional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstadosDisponibles = new List<string>
            {
                "Ansiedad", "Tristeza", "Felicidad", "Estrés", "Irritabilidad", "Calma", "Motivación"
            };

            return View(estadoEmocional);
        }


        public ActionResult Detalles(int? id)
        {
            if (id == null || Session["UsuarioId"] == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

            var estado = db.EstadoEmocional.FirstOrDefault(e => e.Id == id && e.UsuarioId == usuarioId);
            if (estado == null)
                return HttpNotFound();

            string estadoNormalizado = RemoveDiacritics(estado.Estado).ToLower();
            var sugerencias = db.SugerenciaBase
                .ToList()
                .Where(s => RemoveDiacritics(s.EstadoRelacionado).ToLower() == estadoNormalizado)
                .Select(s => s.Descripcion)
                .ToList();

            ViewBag.Sugerencias = sugerencias;

            return View(estado);
        }


        public ActionResult Grafico()
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Index", "Login");

            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

            var datos = db.EstadoEmocional
                .Where(e => e.UsuarioId == usuarioId)
                .GroupBy(e => e.Estado)
                .Select(g => new
                {
                    Estado = g.Key,
                    Cantidad = g.Count()
                })
                .ToList();

            ViewBag.Estados = datos.Select(d => d.Estado).ToList();
            ViewBag.Cantidades = datos.Select(d => d.Cantidad).ToList();

            return View();
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
