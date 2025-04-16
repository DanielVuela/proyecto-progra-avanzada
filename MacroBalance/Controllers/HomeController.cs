using System.Web.Mvc;
using Macrobalance.Constants;

namespace MacroBalance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session[SessionAtributes.UsuarioId] == null) 
            {
                RedirectToAction("Index", "Login");
            }
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
