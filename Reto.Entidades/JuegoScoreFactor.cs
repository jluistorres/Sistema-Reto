using System.ComponentModel.DataAnnotations;

namespace Reto.Entidades
{
    public class JuegoScoreFactor
    {
        [Key]
        public int Nivel { get; set; }
        public int Base { get; set; }
        public int DuracionBase { get; set; }
        public int PuntajeMax { get; set; }
        public int Factor { get; set; }
    }
}
