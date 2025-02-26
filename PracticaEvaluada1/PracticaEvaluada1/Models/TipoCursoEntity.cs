using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Models
{
    [Table("TiposCursos")]
    public class TipoCursoEntity
    {
        [Key]
        public int TipoCurso { get; set; }

        [Required]
        [StringLength(50)]
        public string DescripcionTipoCurso { get; set; }

        public virtual ICollection<RegistroModel> Estudiantes { get; set; }
    }
}
