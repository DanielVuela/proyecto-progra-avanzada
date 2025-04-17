using System;
using System.Collections.Generic;
using MacroBalance.Database;

namespace MacroBalance.Models.ViewModels
{
    public class BitacoraHoyViewModel
    {
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }

        // Comidas (alimentos registrados)
        public List<RegistroAlimento> AlimentosConsumidos { get; set; }

        // Totales diarios (calculados)
        public decimal CaloriasConsumidas { get; set; }
        public decimal ProteinasConsumidas { get; set; }
        public decimal CarbohidratosConsumidos { get; set; }
        public decimal GrasasConsumidas { get; set; }

        // Metas del perfil
        public decimal? CaloriasMeta { get; set; }
        public decimal? ProteinasMeta { get; set; }
        public decimal? CarbohidratosMeta { get; set; }
        public decimal? GrasasMeta { get; set; }
    }
}