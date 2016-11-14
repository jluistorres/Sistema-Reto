using System;
using System.ComponentModel.DataAnnotations;

namespace Reto.Entidades
{
    public class Preguntas
    {
        [Key]
        public int IdPregunta { get; set; }
        public string Enunciado { get; set; }
        public string Solucion { get; set; }
        public string TipoResultado { get; set; }
        public Nullable<int> Puntuacion { get; set; }
    }
}
