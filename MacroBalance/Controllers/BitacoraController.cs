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
            DateTime hoy = DateTime.Today;

            using (var db = new MacroBalanceEntities())
            {
                var dietaIds = db.Dieta
                    .Where(d => d.IdUsuario == usuarioId)
                    .Select(d => d.Id)
                    .ToList();

                // Cargar alimentos registrados hoy
                var alimentosHoy = db.RegistroAlimento
                    .Include("Alimento")
                    .Where(ra => dietaIds.Contains((int)ra.DietaId) && ra.Fecha == hoy)
                    .ToList();

                // Sumar nutrientes
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

                // Obtener metas del perfil
                var perfil = db.PerfilDieta
                    .Where(p => p.UsuarioId == usuarioId)
                    .OrderByDescending(p => p.FechaAsignacion)
                    .FirstOrDefault();

                var model = new BitacoraHoyViewModel
                {
                    UsuarioId = usuarioId,
                    Fecha = hoy,
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