using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MacroBalance;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;

namespace MacroBalance.Controllers
{
    public class ObjetivosSaludController : Controller
    {
        private MacroBalanceEntities db = new MacroBalanceEntities();

        public ActionResult Index()
        {
            var modelo = db.Dieta
                .Where(d => d.ObjetivoId != null && d.IdUsuario == null)
                .GroupBy(d => d.ObjetivoId)
                .Select(g => g.FirstOrDefault())
                .Select(d => new DietaConObjetivoViewModel
                {
                    IdDieta = d.Id,
                    NombreObjetivo = d.Objetivo.NombreObjetivo,
                    NombreDieta = d.Nombre,
                    DescripcionDieta = d.Descripcion
                })
                .ToList();

            return View(modelo);
        }




        public ActionResult DetallesObjetivo(int dietaId)
        {
            var dieta = db.Dieta.FirstOrDefault(d => d.Id == dietaId);
            if (dieta == null)
                return HttpNotFound();

            var alimentos = db.Alimento_Dieta
                .Where(ad => ad.DietaId == dietaId)
                .Join(db.Alimento,
                      ad => ad.AlimentoId,
                      a => a.Id,
                      (ad, a) => new AlimentoViewModel
                      {
                          Nombre = a.Nombre,
                          Descripcion = a.Descripcion,
                          Calorias = a.Calorias,
                          Proteina = a.Proteina,
                          Carbohidratos = a.Carbohidratos,
                          Grasas = a.Grasas,
                          CantidadDiaria = ad.CantidadDiaria
                      })
                .ToList();

            var viewModel = new DietaConAlimentosViewModel
            {
                NombreDieta = dieta.Nombre,
                DescripcionDieta = dieta.Descripcion,
                Alimentos = alimentos
            };

            return View("DetallesObjetivo", viewModel);
        }
    }
}
