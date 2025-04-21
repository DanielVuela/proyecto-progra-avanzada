using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacroBalance.Models.ViewModels
{
    public class DietaConAlimentosViewModel
    {
        public string NombreDieta { get; set; }
        public string DescripcionDieta { get; set; }

        public List<AlimentoViewModel> Alimentos { get; set; }
    }

    public class AlimentoViewModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal? Calorias { get; set; }
        public decimal? Proteina { get; set; }
        public decimal? Carbohidratos { get; set; }
        public decimal? Grasas { get; set; }
        public decimal? CantidadDiaria { get; set; }
    }
}


