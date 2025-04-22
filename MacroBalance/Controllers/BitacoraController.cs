using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Macrobalance.Constants;
using MacroBalance.Database;
using MacroBalance.Models.ViewModels;

namespace MacroBalance.Controllers
{
    public class BitacoraController : Controller
    {
        [HttpGet]
        public ActionResult Hoy()
        {
            int usuarioId = (int)Session[SessionAtributes.UsuarioId];
            DateTime inicioHoy = DateTime.Today;
            DateTime inicioManana = DateTime.Today.AddDays(1);

            using (var db = new MacroBalanceEntities())
            {
                var dietaIds = db.Dieta
                    .Where(d => d.IdUsuario == usuarioId)
                    .Select(d => d.Id)
                    .ToList();

                var alimentosHoy = db.RegistroAlimento
                    .Include("Alimento")
                    .Where(ra => dietaIds.Contains((int)ra.DietaId) && ra.Fecha >= inicioHoy && ra.Fecha < inicioManana)
                    .ToList();

                // sumar nutrientes
                decimal totalCalorias = 0, totalProteinas = 0, totalCarbohidratos = 0, totalGrasas = 0;

                foreach (var registro in alimentosHoy)
                {
                    if (registro.Alimento != null)
                    {
                        totalCalorias += (registro.Alimento.Calorias ?? 0) * registro.Cantidad ?? 0;
                        totalProteinas += (registro.Alimento.Proteina ?? 0) * registro.Cantidad ?? 0;
                        totalCarbohidratos += (registro.Alimento.Carbohidratos ?? 0) * registro.Cantidad ?? 0;
                        totalGrasas += (registro.Alimento.Grasas ?? 0) * registro.Cantidad ?? 0;
                    }
                }

                // cargar metas del perfil
                var perfil = db.PerfilDieta
                    .Where(p => p.UsuarioId == usuarioId)
                    .OrderByDescending(p => p.FechaAsignacion)
                    .FirstOrDefault();

                var model = new BitacoraHoyViewModel
                {
                    UsuarioId = usuarioId,
                    Fecha = inicioHoy,
                    AlimentosConsumidos = alimentosHoy,

                    CaloriasConsumidas = totalCalorias,
                    ProteinasConsumidas = totalProteinas,
                    CarbohidratosConsumidos = totalCarbohidratos,
                    GrasasConsumidas = totalGrasas,

                    CaloriasMeta = perfil?.CaloriasObjetivo,
                    ProteinasMeta = perfil?.Proteinas,
                    CarbohidratosMeta = perfil?.Carbohidratos,
                    GrasasMeta = perfil?.Grasas
                };

                return View(model);
            }
        }

    }
}