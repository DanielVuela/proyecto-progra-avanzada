using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MacroBalance.Service;

namespace MacroBalance
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Timer _timer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _timer = new Timer(60000); 
            _timer.Elapsed += (sender, e) => NotificadorService.EjecutarNotificaciones();
            _timer.Start();
        }
    }
}
