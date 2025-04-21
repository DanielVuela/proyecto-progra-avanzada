using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacroBalance.Models.ViewModels
{
    public class DietaConObjetivoViewModel
    {
        public int IdDieta { get; set; }
        public string NombreObjetivo { get; set; }
        public string NombreDieta { get; set; }
        public string DescripcionDieta { get; set; }
    }
}
