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

        public List<usp_DocenteCursoSelect_Result> DocenteCursoSelect(int IdPersona)
        {
            var result = this.cnx.Database.SqlQuery<usp_DocenteCursoSelect_Result>("usp_DocenteCursoSelect @IdPersona = {0}", IdPersona);
            return result.ToList();
            //return cnx.usp_DocenteCursoSelect(IdPersona).ToList();
        }

        public List<usp_RegistroNotasSelect_Result> RegistroNotasSelect(int Grado, string Seccion, int IdCurso)
        {
            var result = this.cnx.Database.SqlQuery<usp_RegistroNotasSelect_Result>("usp_RegistroNotasSelect @Grado = {0}, @Seccion={1}, @IdCurso={2}", Grado, Seccion, IdCurso);
            return result.ToList();
            //return cnx.usp_RegistroNotasSelect(Grado, Seccion, IdCurso).ToList();
        }

        public int GuardarNotas(DocenteCurso dc, List<NotasCriterio> notas)
        {
            int registros = 0;
            cnx = new dbretoEntities();
            int result = this.cnx.Database.ExecuteSqlCommand("usp_NotasCriterioEliminar @Grado = {0}, @Seccion={1}, @IdCurso={2}", dc.Grado, dc.Seccion, dc.IdCurso);           
            //cnx.usp_NotasCriterioEliminar(dc.Grado, dc.Seccion, dc.IdCurso);

            if (notas != null && notas.Count > 0)
            {
                cnx.NotasCriterio.AddRange(notas);
                registros = cnx.SaveChanges();
            }

            return registros;
        }
    }
}
