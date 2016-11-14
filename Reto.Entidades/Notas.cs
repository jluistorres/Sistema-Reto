//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Reto.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Notas
    {
        [Key, Column(Order = 0)]
        public int IdAlumno { get; set; }
        [Key, Column(Order = 1)]
        public int IdNivelEscolar { get; set; }
        [Key, Column(Order = 2)]
        public int Grado { get; set; }
        [Key, Column(Order = 3)]
        public int Bimestre { get; set; }
        [Key, Column(Order = 4)]
        public int Anyo { get; set; }        
        public Nullable<int> IdDocente { get; set; }
        public Nullable<double> PromedioProceso { get; set; }
        public Nullable<double> PromedioVirtual { get; set; }
        public Nullable<double> TestPresencial { get; set; }
        public Nullable<double> PromedioSprint { get; set; }
        public Nullable<double> Bimestral { get; set; }
        public Nullable<double> Plataforma { get; set; }
        public Nullable<double> Clase { get; set; }
        public Nullable<double> PromedioActitud { get; set; }
        public Nullable<double> NotaFinal { get; set; }        
        public Nullable<DateTime> Fecha { get; set; }
        public string Observaciones { get; set; }
    }
}
