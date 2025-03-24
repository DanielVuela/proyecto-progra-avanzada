using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MacroBalance;
using MacroBalance.Database;

namespace MacroBalance.Controllers
{
    public class ObjetivosSaludController : Controller
    {
        private MacroBalanceEntities4 db = new MacroBalanceEntities4();

        public ActionResult Index()
        {
            var objetivos = db.Objetivo.ToList();

            return View(objetivos);
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