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
    
    public partial class usp_RegistroNotasSelect_Result
    {
        public int IdMatricula { get; set; }
        public Nullable<int> IdAlumno { get; set; }
        public string Alumno { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public string Curso { get; set; }
        public Nullable<int> IdCriterio { get; set; }
        public string Criterio { get; set; }
        public Nullable<int> NroEvaluacion { get; set; }
        public Nullable<double> Nota { get; set; }
        public Nullable<int> IdDocenteCurso { get; set; }
    }
}
