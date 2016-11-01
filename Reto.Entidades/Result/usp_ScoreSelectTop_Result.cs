
using System;
namespace Reto.Entidades.Result
{
    public class usp_ScoreSelectTop_Result
    {
        public int IdAlumno { get; set; }
        public int IdJuego { get; set; }
        public string Respuesta { get; set; }
        public Nullable<double> TiempoResolucion { get; set; }
        public Nullable<double> TiempoReaccion { get; set; }
        public Nullable<double> Puntaje { get; set; }
    }
}
