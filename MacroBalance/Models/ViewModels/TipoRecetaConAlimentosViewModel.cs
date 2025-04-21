using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MacroBalance.Database;

namespace MacroBalance.Models.ViewModels
{
    public class TipoRecetaConAlimentosViewModel
    {
        public int? TipoRecetaIdSeleccionada { get; set; }

        public List<SelectListItem> TiposReceta { get; set; }

        public List<Alimento> AlimentosRelacionados { get; set; }

        public TipoRecetaConAlimentosViewModel()
        {
            TiposReceta = new List<SelectListItem>();
            AlimentosRelacionados = new List<Alimento>();
        }
    }
}
