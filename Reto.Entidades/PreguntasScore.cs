using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reto.Entidades
{
    public class PreguntasScore
    {
        [Key, Column(Order = 0)]
        public int IdAlumno { get; set; }
        [Key, Column(Order = 1)]
        public int IdPregunta { get; set; }
        public int Puntaje { get; set; }
        public Nullable<DateTime> FechaRegistro { get; set; }
    }
}
