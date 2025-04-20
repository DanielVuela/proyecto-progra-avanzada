using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MacroBalance.Database;

namespace MacroBalance.Controllers
{
    public class CicloMenstrualController : Controller
    {
        private readonly MacroBalanceEntities db = new MacroBalanceEntities();

        public ActionResult Index()
        {
            var sugerencias = db.SugerenciaCicloMenstrual.ToList();
            return View(sugerencias);
        }
    }
}

