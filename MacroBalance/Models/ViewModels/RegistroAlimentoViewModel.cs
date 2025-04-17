using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace MacroBalance.Models.ViewModels
{
	public class RegistroAlimentoViewModel
	{
		public int DietaId { get; set; }

		[Display(Name = "Alimento")]
		[Required]
		public int AlimentoId { get; set; }

		[Display(Name = "Cantidad (porciones)")]
		[Required]
		[Range(0.1, 100)]
		public decimal Cantidad { get; set; }

		public List<SelectListItem> Alimentos { get; set; }
	}

}