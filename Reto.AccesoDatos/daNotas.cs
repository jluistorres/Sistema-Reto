using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reto.Entidades;

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
            int registros = 0;
            cnx = new dbretoEntities();

            this.cnx.Database.ExecuteSqlCommand("usp_NotasEliminar @IdNivelEscolar = {0}, @Grado = {1}, @Seccion={2}, @Bimestre={3}", IdNivelEscolar, Grado, Seccion, Bimestre);

            if (criterios != null && criterios.Count > 0)
            {
                cnx.NotasCriterio.AddRange(criterios);
            }

            if (notas != null && notas.Count > 0)
            {
                cnx.Notas.AddRange(notas);
            }

            registros = cnx.SaveChanges();

            return registros;
        }
    }
}
