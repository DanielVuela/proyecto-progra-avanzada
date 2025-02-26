using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PracticaEvaluada1;

namespace TuProyecto.Models
{
    public class RegistroModel
    {
        public Estudiante InfoDeEstudiante { get; set; }

        public List<SelectListItem> TiposDeCurso { get; set; }

        public string MensajeExito { get; set; }
    }
}