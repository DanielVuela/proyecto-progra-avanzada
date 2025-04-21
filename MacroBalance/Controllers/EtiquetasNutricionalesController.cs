using System;
using System.Web.Mvc;

namespace MacroBalance.Controllers
{
    public class EtiquetasNutricionalesController : Controller 
    {
        public ActionResult Index()
        {
            return View("EtiquetasNutri");
        }
    }

}
