using System;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;

namespace MacroBalance.Controllers
{
    public class ProgresoController : Controller
    {
        private readonly MacroBalanceEntities db = new MacroBalanceEntities();

        public enum FiltroTiempo
        {
            Todo,
            Semana,
            Mes,
            SeisMeses,
            Anio
        }

        public ActionResult Index(FiltroTiempo filtro = FiltroTiempo.Todo)
        {
            int? usuarioId = Session["UsuarioId"] as int?;
            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DateTime desde = DateTime.MinValue;

            switch (filtro)
            {
                case FiltroTiempo.Semana:
                    desde = DateTime.Today.AddDays(-7);
                    break;
                case FiltroTiempo.Mes:
                    desde = DateTime.Today.AddMonths(-1);
                    break;
                case FiltroTiempo.SeisMeses:
                    desde = DateTime.Today.AddMonths(-6);
                    break;
                case FiltroTiempo.Anio:
                    desde = DateTime.Today.AddYears(-1);
                    break;
                case FiltroTiempo.Todo:
                default:
                    desde = DateTime.MinValue;
                    break;
            }

            var historial = db.Progreso
                              .Where(p => p.UsuarioId == usuarioId && p.Fecha >= desde)
                              .OrderBy(p => p.Fecha)
                              .ToList();

            ViewBag.FiltroSeleccionado = filtro.ToString();
            return View(historial);
        }
    }
}
