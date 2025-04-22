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
                        Calorias = etiqueta.Calorias ?? 0, // Reemplaza null con 0
                        PorcionesPorEnvase = etiqueta.PorcionesPorEnvase ?? 0, // Reemplaza null con 0
                        Sodio = etiqueta.Sodio ?? 0, // Reemplaza null con 0
                        Azucares = etiqueta.Azucares ?? 0, // Reemplaza null con 0
                        FibraDietetica = etiqueta.FibraDietetica ?? 0, // Reemplaza null con 0
                        VitaminaA = etiqueta.VitaminaA ?? 0, // Reemplaza null con 0
                        VitaminaC = etiqueta.VitaminaC ?? 0, // Reemplaza null con 0
                        Calcio = etiqueta.Calcio ?? 0, // Reemplaza null con 0
                        Hierro = etiqueta.Hierro ?? 0 // Reemplaza null con 0
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
