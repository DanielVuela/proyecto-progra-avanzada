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
        // Para cargar la lista de alimentos en el dropdown
        public List<SelectListItem> Alimentos { get; set; }

        // ID del alimento seleccionado para poder mostrar la etiqueta nutricional
        public int AlimentoIdSeleccionado { get; set; }

        // Detalles de la etiqueta nutricional, en caso de que se seleccione un alimento
        public EtiquetaNutricionalDetalleViewModel EtiquetaNutricional { get; set; }
    }

    // Este es el ViewModel para los detalles de la etiqueta nutricional, que se pasará a la vista
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
