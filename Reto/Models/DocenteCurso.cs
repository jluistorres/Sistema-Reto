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
    
    public partial class DocenteCurso
    {
        public DocenteCurso()
        {
            this.Notas = new HashSet<Notas>();
        }
    
        public int IdDocenteCurso { get; set; }
        public Nullable<int> AnyoAcademico { get; set; }
        public int IdDocente { get; set; }
        public int IdCurso { get; set; }
        public Nullable<int> IdNivelEscolar { get; set; }
        public Nullable<int> Grado { get; set; }
        public string Seccion { get; set; }
        public Nullable<int> NroEvaluaciones { get; set; }
    
        public virtual Curso Curso { get; set; }
        public virtual Docente Docente { get; set; }
        public virtual ICollection<Notas> Notas { get; set; }
    }
}
