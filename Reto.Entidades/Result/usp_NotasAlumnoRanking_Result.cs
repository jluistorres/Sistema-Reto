using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Entidades.Result
{
    public class usp_NotasAlumnoRanking_Result
    {
        public string Nombres { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public Nullable<double> PromedioVirtual { get; set; }
    }
}
