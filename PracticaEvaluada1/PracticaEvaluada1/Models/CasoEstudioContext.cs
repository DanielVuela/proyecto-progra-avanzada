using System.Data.Entity;
using PracticaEvaluada1;

namespace TuProyecto.Models
{
    public class CasoEstudioContext : DbContext
    {
        public CasoEstudioContext() : base("name=CasoEstudioEntities") { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<TiposCurso> TiposCursos { get; set; }
    }
}
