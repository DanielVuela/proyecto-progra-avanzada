using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MacroBalance.Database;

namespace MacroBalance.Controllers
{
    public class DietaController : Controller
    {
        private MacroBalanceEntities4 db = new MacroBalanceEntities4();

        public ActionResult DetallesDieta(int id)
        {
            var dieta = db.Dieta.Find(id);
            if (dieta == null)
            {
                return HttpNotFound();
            }

            var alimentos = db.Alimento_Dieta
                .Include("Alimento")
                .Where(ad => ad.DietaId == id)
                .ToList();

            ViewBag.Alimentos = alimentos;

            return View(dieta);
        }
    }
}