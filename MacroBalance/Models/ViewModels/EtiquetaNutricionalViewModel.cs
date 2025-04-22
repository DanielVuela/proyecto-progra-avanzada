using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MacroBalance.Database;

namespace MacroBalance.Models.ViewModels
{
    public class EtiquetaNutricionalViewModel
    {
        public List<SelectListItem> Alimentos { get; set; }

        public int AlimentoIdSeleccionado { get; set; }

        public EtiquetaNutricionalDetalleViewModel EtiquetaNutricional { get; set; }
    }

    public class EtiquetaNutricionalDetalleViewModel
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string TamanoPorcion { get; set; }
        public decimal? Calorias { get; set; }
        public decimal? PorcionesPorEnvase { get; set; }
        public decimal? Sodio { get; set; }
        public decimal? Azucares { get; set; }
        public decimal? FibraDietetica { get; set; }
        public decimal? VitaminaA { get; set; }
        public decimal? VitaminaC { get; set; }
        public decimal? Calcio { get; set; }
        public decimal? Hierro { get; set; }
    }
}
