using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.AccesoDatos
{
    public class dbretoEntities : DbContext
    {
        public dbretoEntities()
            : base("name=dbretoEntities")
        {
            Database.SetInitializer<dbretoEntities>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Alumno_Juego> Alumno_Juego { get; set; }
        public virtual DbSet<CriteriosEvaluacion> CriteriosEvaluacion { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Docente> Docente { get; set; }
        public virtual DbSet<DocenteCurso> DocenteCurso { get; set; }
        public virtual DbSet<Juego> Juego { get; set; }
        public virtual DbSet<Matricula> Matricula { get; set; }
        public virtual DbSet<NivelEscolar> NivelEscolar { get; set; }
        public virtual DbSet<Notas> Notas { get; set; }
        public virtual DbSet<NotasCriterio> NotasCriterio { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        /*
        public virtual ObjectResult<usp_RegistroNotasSelect_Result> usp_RegistroNotasSelect(Nullable<int> grado, string seccion, Nullable<int> idCurso)
        {
            var gradoParameter = grado.HasValue ?
                new ObjectParameter("Grado", grado) :
                new ObjectParameter("Grado", typeof(int));

            var seccionParameter = seccion != null ?
                new ObjectParameter("Seccion", seccion) :
                new ObjectParameter("Seccion", typeof(string));

            var idCursoParameter = idCurso.HasValue ?
                new ObjectParameter("IdCurso", idCurso) :
                new ObjectParameter("IdCurso", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_RegistroNotasSelect_Result>("usp_RegistroNotasSelect", gradoParameter, seccionParameter, idCursoParameter);
        }

        public virtual int usp_NotasCriterioEliminar(Nullable<int> grado, string seccion, Nullable<int> idCurso)
        {
            var gradoParameter = grado.HasValue ?
                new ObjectParameter("Grado", grado) :
                new ObjectParameter("Grado", typeof(int));

            var seccionParameter = seccion != null ?
                new ObjectParameter("Seccion", seccion) :
                new ObjectParameter("Seccion", typeof(string));

            var idCursoParameter = idCurso.HasValue ?
                new ObjectParameter("IdCurso", idCurso) :
                new ObjectParameter("IdCurso", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_NotasCriterioEliminar", gradoParameter, seccionParameter, idCursoParameter);
        }

        public virtual ObjectResult<usp_UsuarioLogin_Result> usp_UsuarioLogin(string idUsuario, string clave)
        {
            var idUsuarioParameter = idUsuario != null ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(string));

            var claveParameter = clave != null ?
                new ObjectParameter("Clave", clave) :
                new ObjectParameter("Clave", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_UsuarioLogin_Result>("usp_UsuarioLogin", idUsuarioParameter, claveParameter);
        }

        public virtual ObjectResult<usp_DocenteCursoSelect_Result> usp_DocenteCursoSelect(Nullable<int> idPersona)
        {
            var idPersonaParameter = idPersona.HasValue ?
                new ObjectParameter("IdPersona", idPersona) :
                new ObjectParameter("IdPersona", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_DocenteCursoSelect_Result>("usp_DocenteCursoSelect", idPersonaParameter);
        }

        public virtual ObjectResult<Nullable<int>> usp_CreateIdReto(string nivel)
        {
            var nivelParameter = nivel != null ?
                new ObjectParameter("Nivel", nivel) :
                new ObjectParameter("Nivel", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("usp_CreateIdReto", nivelParameter);
        }

        public virtual int usp_CuboselectByNivel(Nullable<int> idCubo, Nullable<int> idNivel)
        {
            var idCuboParameter = idCubo.HasValue ?
                new ObjectParameter("IdCubo", idCubo) :
                new ObjectParameter("IdCubo", typeof(int));

            var idNivelParameter = idNivel.HasValue ?
                new ObjectParameter("IdNivel", idNivel) :
                new ObjectParameter("IdNivel", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_CuboselectByNivel", idCuboParameter, idNivelParameter);
        }
        */
    }
}
