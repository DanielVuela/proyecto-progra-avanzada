using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using TuProyecto.Models;

namespace TuProyecto.Controllers
{
    public class RegistroController : Controller
    {
        private readonly CasoEstudioContext db = new CasoEstudioContext();

        [HttpGet]
        public ActionResult Registrar()
        {
            var data = db.TiposCursos.Select(tc => new SelectListItem
            {
                Value = tc.TipoCurso.ToString(),
                Text = tc.DescripcionTipoCurso
            }).ToList();
            return View(new RegistroModel { TiposDeCurso = data });
        }

        [HttpGet]
        public ActionResult Consultar()
        {
            var matriculas = db.Estudiantes
                .Join(db.TiposCursos,
                    est => est.TipoCurso,
                    tipo => tipo.TipoCurso,
                    (est, tipo) => new MatriculaViewModel
                    {
                        Fecha = est.Fecha,
                        Monto = est.Monto,
                        DescripcionTipoCurso = tipo.DescripcionTipoCurso,
                        NombreEstudiante = est.Nombre
                    })
                .ToList();

            return View(matriculas);
        }

        [HttpPost]
        public ActionResult Registrar(RegistroModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    if (!ModelState.IsValid)
                    {
                        model.TiposDeCurso = db.TiposCursos.Select(tc => new SelectListItem
                        {
                            Value = tc.TipoCurso.ToString(),
                            Text = tc.DescripcionTipoCurso
                        }).ToList();

                        return View(model);
                    }
                }

                var cursoExistente = db.TiposCursos.Any(tc => tc.TipoCurso == model.InfoDeEstudiante.TipoCurso);
                if (!cursoExistente)
                {
                    ModelState.AddModelError("TipoCurso", "El tipo de curso seleccionado no es válido.");
                    ViewBag.TipoCursos = new SelectList(db.TiposCursos, "TipoCurso", "DescripcionTipoCurso", model.InfoDeEstudiante.TipoCurso);
                    return View(model);
                }

                var countEstudiantes = db.Estudiantes.Count(e => e.TipoCurso == model.InfoDeEstudiante.TipoCurso);
                if (countEstudiantes >= 3)
                {
                    ModelState.AddModelError("TipoCurso", "No se pueden matricular más de 3 estudiantes en este curso.");
                    ViewBag.TipoCursos = new SelectList(db.TiposCursos, "TipoCurso", "DescripcionTipoCurso", model.InfoDeEstudiante.TipoCurso);
                    return View(model);
                }

                db.Estudiantes.Add(model.InfoDeEstudiante);
                db.SaveChanges();

                ViewBag.MensajeExito = "Matricula guardada~";

                model.TiposDeCurso = db.TiposCursos.Select(tc => new SelectListItem
                {
                    Value = tc.TipoCurso.ToString(),
                    Text = tc.DescripcionTipoCurso
                }).ToList();
                ViewBag.TiposDeCurso = new SelectList(model.TiposDeCurso, "Value", "Text");
                return View(model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al registrar la matrícula: " + ex.Message);
                model.TiposDeCurso = db.TiposCursos.Select(tc => new SelectListItem
                {
                    Value = tc.TipoCurso.ToString(),
                    Text = tc.DescripcionTipoCurso
                }).ToList();
                ViewBag.TiposDeCurso = new SelectList(model.TiposDeCurso, "Value", "Text");
                return View(model);
            }
        }
    }
}
