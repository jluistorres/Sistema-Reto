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
        public virtual DbSet<Bimestre> Bimestre { get; set; }
        public virtual DbSet<JuegoScore> JuegoScore { get; set; }
        public virtual DbSet<JuegoScoreFactor> JuegoScoreFactor { get; set; }
        public virtual DbSet<CriteriosEvaluacion> CriteriosEvaluacion { get; set; }        
        public virtual DbSet<Docente> Docente { get; set; }        
        public virtual DbSet<Juego> Juego { get; set; }        
        public virtual DbSet<NivelEscolar> NivelEscolar { get; set; }
        public virtual DbSet<Notas> Notas { get; set; }
        public virtual DbSet<NotasCriterio> NotasCriterio { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
