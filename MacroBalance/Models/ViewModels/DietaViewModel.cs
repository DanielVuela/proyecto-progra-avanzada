using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MacroBalance.Models.ViewModels
{
    public class DietaViewModel
    {
        public int UsuarioId { get; set; }

        [Display(Name = "Calorías Objetivo")]
        public decimal CaloriasObjetivo { get; set; }

        [Display(Name = "Proteínas (porciones)")]
        public decimal Proteinas { get; set; }

        [Display(Name = "Carbohidratos (porciones)")]
        public decimal Carbohidratos { get; set; }

        [Display(Name = "Grasas (porciones)")]
        public decimal Grasas { get; set; }

        [Display(Name = "Agua (porciones)")]
        public decimal Agua { get; set; }

        [Display(Name = "Ejercicio (minutos)")]
        public decimal Ejercicio { get; set; }

        public List<SelectListItem> Usuarios { get; set; }
    }

}