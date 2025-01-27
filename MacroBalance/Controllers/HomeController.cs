using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacroBalance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml"); 
        }

        public ActionResult Faq() 
        {
            return View();
        }
        public ActionResult RecuperarContrasena()
        {
            return View();
        }

        public ActionResult AjustesCuenta()
        {
            return View();
        }
    }
}
