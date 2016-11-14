using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Entidades.Result
{
    public class usp_ScoreHistorial_Result
    {
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
        public int IdJuego { get; set; }
        public string TipoJuego { get; set; }
        public string Respuesta { get; set; }
        public Nullable<double> TiempoResolucion { get; set; }
        public Nullable<double> TiempoReaccion { get; set; }
        public Nullable<double> Puntaje { get; set; }
        public string FechaRegistro { get; set; }
        public int Bimestre { get; set; }
    }
}
