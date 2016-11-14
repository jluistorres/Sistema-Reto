using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reto.Entidades;
using Reto.Entidades.Result;

namespace Reto.AccesoDatos
{
    public class daNotas
    {
        dbretoEntities cnx;

        public daNotas()
        {
            cnx = new dbretoEntities();
            cnx.Configuration.ProxyCreationEnabled = false;
        }

        public usp_DocenteSelect_Result DocenteSelect(int IdPersona)
        {
            var result = this.cnx.Database.SqlQuery<usp_DocenteSelect_Result>("usp_DocenteSelect @IdPersona = {0}", IdPersona);
            return result.FirstOrDefault();
            //return cnx.usp_DocenteCursoSelect(IdPersona).ToList();
        }

        public List<CriteriosEvaluacion> CriteriosEvaluacionSelect()
        {
            return cnx.CriteriosEvaluacion.ToList();
        }

        public List<usp_NotasSelect_Result> NotasSelect(int IdNivelEscolar, int Grado, string Seccion, int Bimestre)
        {
            var result = this.cnx.Database.SqlQuery<usp_NotasSelect_Result>("usp_NotasSelect @IdNivelEscolar = {0}, @Grado = {1}, @Seccion={2}, @Bimestre={3}", IdNivelEscolar, Grado, Seccion, Bimestre);
            return result.ToList();
            //return cnx.usp_RegistroNotasSelect(Grado, Seccion, IdCurso).ToList();
        }

        public int GuardarNotas(int IdNivelEscolar, int Grado, string Seccion, int Bimestre, List<NotasCriterio> criterios, List<Notas> notas)
        {            
            cnx = new dbretoEntities();

            this.cnx.Database.ExecuteSqlCommand("usp_NotasEliminar @IdNivelEscolar = {0}, @Grado = {1}, @Seccion={2}, @Bimestre={3}", IdNivelEscolar, Grado, Seccion, Bimestre);                        

            if (criterios != null && criterios.Count > 0)
            {
                cnx.NotasCriterio.AddRange(criterios);
            }

            if (notas != null && notas.Count > 0)
            {                
                //cnx.Notas.AddRange(notas);

                int anyo = DateTime.Now.Year;

                foreach (var n in notas)
                {
                    this.cnx.Database.ExecuteSqlCommand("usp_NotasRegistrar  @IdAlumno={0}, @IdNivelEscolar={1}, @Grado={2}, @Bimestre={3}, @Anyo={4}, @IdDocente={5}, @PromedioProceso={6}, @PromedioVirtual={7}, @TestPresencial={8}, @PromedioSprint={9}, @Bimestral={10}, @Plataforma={11}, @Clase={12}, @PromedioActitud={13}, @NotaFinal={14}",
                        n.IdAlumno, IdNivelEscolar, Grado, Bimestre, anyo, n.IdDocente, n.PromedioProceso, n.PromedioVirtual, n.TestPresencial, n.PromedioSprint, n.Bimestral, n.Plataforma, n.Clase, n.PromedioActitud, n.NotaFinal);
                }
            }

            return cnx.SaveChanges();
        }

        public List<Notas> HistorialNotas(int IdAlumno)
        {
            return cnx.Notas.Where(x => x.IdAlumno == IdAlumno).OrderBy(x => x.Fecha).ToList();
        }

        public List<usp_NotasAlumnoRanking_Result> NotasAlumnoRanking(int Bimestre, int Anyo)
        {
            var result = cnx.Database.SqlQuery<usp_NotasAlumnoRanking_Result>("usp_NotasAlumnoRanking @Bimestre={0}, @Anyo={1}", Bimestre, Anyo);
            return result.ToList();
        }

        public int RegistrarObservaciones(Notas objEN)
        {
            var nota = cnx.Notas.FirstOrDefault(x => x.IdAlumno == objEN.IdAlumno && x.IdNivelEscolar == objEN.IdNivelEscolar && x.Grado == objEN.Grado && x.Bimestre == objEN.Bimestre && x.Anyo == objEN.Anyo);
            if (nota != null)
            {
                nota.Observaciones = objEN.Observaciones;
                return cnx.SaveChanges();
            }
            return 0;
        }
    }
}
