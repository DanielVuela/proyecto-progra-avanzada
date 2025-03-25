

using System;
using System.ComponentModel.DataAnnotations;

namespace MacroBalance.Models
{
    public class RecordatorioViewModel
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }  
        public string Nombre { get; set; }
        public string Frecuencia { get; set; }
        public TimeSpan Hora { get; set; }  
    }
}
