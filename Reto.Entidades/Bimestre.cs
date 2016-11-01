using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reto.Entidades
{
    public class Bimestre
    {
        [Key, Column(Order = 0)]
        public int Anyo { get; set; }
        [Key, Column(Order = 1)]
        public int NroBimestre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
