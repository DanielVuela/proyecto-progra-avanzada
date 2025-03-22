using System;

namespace TuProyecto.Models
{
    public class MatriculaViewModel
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string DescripcionTipoCurso { get; set; }
        public string NombreEstudiante { get; set; }
    }
}
