//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MacroBalance.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class PerfilDieta
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Nullable<decimal> CaloriasObjetivo { get; set; }
        public Nullable<decimal> Proteinas { get; set; }
        public Nullable<decimal> Carbohidratos { get; set; }
        public Nullable<decimal> Grasas { get; set; }
        public Nullable<decimal> Agua { get; set; }
        public Nullable<decimal> Ejercicio { get; set; }
        public Nullable<System.DateTime> FechaAsignacion { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
