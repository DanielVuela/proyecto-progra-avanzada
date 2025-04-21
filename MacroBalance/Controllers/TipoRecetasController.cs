using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;

namespace MacroBalance.Controllers
{
    public class TipoRecetasController : Controller
    {
        private MacroBalanceEntities db = new MacroBalanceEntities();

        public ActionResult Index(int? tipoRecetaId)
        {
            var viewModel = new TipoRecetaConAlimentosViewModel();

            viewModel.TiposReceta = db.TipoReceta
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nombre,
                    Selected = (t.Id == tipoRecetaId)
                })
                .ToList();

            viewModel.TipoRecetaIdSeleccionada = tipoRecetaId;

            if (tipoRecetaId.HasValue)
            {
                viewModel.AlimentosRelacionados = db.AlimentoTipoReceta
                    .Where(x => x.TipoRecetaId == tipoRecetaId)
                    .Select(x => x.Alimento)
                    .Distinct()
                    .ToList();
            }

            return View(viewModel);
        }

    }
}