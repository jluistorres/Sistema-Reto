using Reto.AccesoDatos;
using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.LogicaNegocio
{
    public class brNotas
    {
        private daNotas odaNotas;

        public brNotas()
        {
            odaNotas = new daNotas();
        }

        public List<usp_DocenteCursoSelect_Result> DocenteCursoSelect(int IdPersona)
        {
            return odaNotas.DocenteCursoSelect(IdPersona);
        }

        public List<usp_RegistroNotasSelect_Result> RegistroNotasSelect(int Grado, string Seccion, int IdCurso)
        {
            return odaNotas.RegistroNotasSelect(Grado, Seccion, IdCurso);
        }

        public int GuardarNotas(DocenteCurso dc, List<NotasCriterio> notas)
        {
            return odaNotas.GuardarNotas(dc, notas);
        }
    }
}
