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
    
    public partial class Progreso
    {
        public int Id { get; set; }
        public Nullable<int> UsuarioId { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<decimal> Peso { get; set; }
        public Nullable<decimal> Cintura { get; set; }
        public Nullable<decimal> Cadera { get; set; }
        public Nullable<decimal> Pecho { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
