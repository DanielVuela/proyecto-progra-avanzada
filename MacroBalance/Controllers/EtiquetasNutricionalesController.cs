using System;
using System.Linq;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;

namespace MacroBalance.Controllers
{
    public class EtiquetasNutricionalesController : Controller
    {
        private MacroBalanceEntities db = new MacroBalanceEntities();

        public ActionResult Index(int? alimentoId)
        {
            var viewModel = new EtiquetaNutricionalViewModel();

            // Cargar alimentos desde la tabla EtiquetaNutricional
            viewModel.Alimentos = db.EtiquetaNutricional
                .Select(e => new SelectListItem
                {
                    Value = e.AlimentoId.ToString(),
                    Text = e.Nombre
                })
                .ToList();

            // Si hay un alimento seleccionado, cargar sus detalles
            if (alimentoId.HasValue)
            {
                var etiqueta = db.EtiquetaNutricional
                    .FirstOrDefault(e => e.AlimentoId == alimentoId.Value);

                if (etiqueta != null)
                {
                    viewModel.AlimentoIdSeleccionado = alimentoId.Value;
                    viewModel.EtiquetaNutricional = new EtiquetaNutricionalDetalleViewModel
                    {
                        Nombre = etiqueta.Nombre,
                        Imagen = etiqueta.Imagen,
                        TamanoPorcion = etiqueta.TamanoPorcion,
                        Calorias = etiqueta.Calorias,
                        PorcionesPorEnvase = etiqueta.PorcionesPorEnvase,
                        Sodio = etiqueta.Sodio,
                        Azucares = etiqueta.Azucares,
                        FibraDietetica = etiqueta.FibraDietetica,
                        VitaminaA = etiqueta.VitaminaA,
                        VitaminaC = etiqueta.VitaminaC,
                        Calcio = etiqueta.Calcio,
                        Hierro = etiqueta.Hierro
                    };
                }
            }

            return View(viewModel);
        }


        // Acción opcional si decidís usar una vista parcial o navegación separada
        public ActionResult DetalleEtiqueta(int alimentoId)
        {
            return RedirectToAction("Index", new { alimentoId = alimentoId });
        }
    }
}
