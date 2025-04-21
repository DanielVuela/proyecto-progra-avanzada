using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacroBalance.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SugerenciaEmocionalViewModel
    {
        public int EstadoEmocionalId { get; set; }

        [Display(Name = "Estado Emocional")]
        public string Estado { get; set; }

        [Display(Name = "Descripción del Estado")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }

        [Display(Name = "Sugerencias Alimenticias")]
        public List<string> Sugerencias { get; set; } = new List<string>();
    }
}
